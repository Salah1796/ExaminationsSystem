using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Common.Extensions;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Answer;
using ExaminationsSystem.Domain.Common.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ExaminationsSystem.Application.Validation.StudentExams
{
    public class AnswerStudentExamCommandValidator : AbstractValidator<AnswerStudentExamCommand>
    {
        public AnswerStudentExamCommandValidator(IStudentExamRepository studentExamRepository)
        {
            RuleFor(i => i)
                .CustomAsync(async (command, context, cancellation) =>
                {
                    var studentExam = await studentExamRepository.Get(x => x.Id == command.StudentExamId && !x.IsDeleted)
                     .Include(x => x.Exam)
                     .ThenInclude(e => e.Questions.Where(x => x.QuestionId == command.QuestionId))
                     .ThenInclude(x => x.Question)
                     .ThenInclude(x => x.Options)
                     .FirstOrDefaultAsync(cancellation);

                    if (studentExam == null)
                        context.AddFailure(ErrorType.Item_NotExist.GetDescription(ErrorType.StudentExam.GetDescription()));
                    else
                    {
                        if (!studentExam.StartDate.HasValue)
                            context.AddFailure(ErrorType.StudentExam_Not_Started.GetDescription());

                        else if (studentExam.EndDate.HasValue || studentExam.StartDate!.Value.AddMinutes(studentExam.Exam.Duration) < DateTime.Now)
                            context.AddFailure(ErrorType.StudentExam_Ended.GetDescription());

                        if (studentExam.Exam?.Questions?.Any(x => x.QuestionId == command.QuestionId) == false)
                            context.AddFailure(ErrorType.Question_NotExistInExam.GetDescription());
                        else
                        {
                            var examQuestion = studentExam.Exam.Questions.FirstOrDefault(x => x.QuestionId == command.QuestionId)!;
                            if (examQuestion.Question.Type == QuestionType.TrueFalse
                            || examQuestion.Question.Type == QuestionType.MultipleChoice
                            || examQuestion.Question.Type == QuestionType.MultipleChoiceWithMultipleAnswer)
                            {
                                var examOptions = examQuestion.Question.Options?.Select(x => x.Id)?.ToList() ?? new();
                                if (!command.SelectedOptionsId.All(x => examOptions.Contains(x)))
                                    context.AddFailure(ErrorType.SelectedOptions_NotExistInQuestion.GetDescription());
                            }
                        }

                        if (studentExam.Exam?.ToDate < DateTime.Now)
                            context.AddFailure(ErrorType.Exam_expired.GetDescription());

                        else if (studentExam.Exam?.FromDate > DateTime.Now)
                            context.AddFailure(ErrorType.Exam_NotAvailable_Now.GetDescription());
                    }
                });
        }
    }
}
