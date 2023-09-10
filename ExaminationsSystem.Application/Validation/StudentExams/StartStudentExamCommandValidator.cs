using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Common.Extensions;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Start;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExaminationsSystem.Application.Validation.StudentExams
{
    public class StartStudentExamCommandValidator : AbstractValidator<StartStudentExamCommand>
    {
        public StartStudentExamCommandValidator(IStudentExamRepository studentExamRepository)
        {
            RuleFor(i => i)
                .CustomAsync(async (command, context, cancellation) =>
                {
                    var studentExam = await studentExamRepository.Get(x => x.Id == command.Id && !x.IsDeleted)
                    .Include(x => x.Exam).FirstOrDefaultAsync(cancellation);

                    if (studentExam == null)
                        context.AddFailure(ErrorType.Item_NotExist.GetDescription(ErrorType.StudentExam.GetDescription()));
                    else
                    {
                        if (studentExam.StartDate.HasValue)
                            context.AddFailure(ErrorType.StudentExam_Alreday_Started.GetDescription());

                        if (studentExam.EndDate.HasValue)
                            context.AddFailure(ErrorType.StudentExam_Ended.GetDescription());

                        if (studentExam.Exam.ToDate < DateTime.Now)
                            context.AddFailure(ErrorType.Exam_expired.GetDescription());

                        else if (studentExam.Exam.FromDate > DateTime.Now)
                            context.AddFailure(ErrorType.Exam_NotAvailable_Now.GetDescription());
                    }
                });
        }
    }
}
