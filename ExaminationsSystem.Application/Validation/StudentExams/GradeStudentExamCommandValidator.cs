using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Common.Extensions;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Grade;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ExaminationsSystem.Application.Validation.StudentExams
{
    public class GradeStudentExamCommandValidator : AbstractValidator<GradeStudentExamCommand>
    {
        public GradeStudentExamCommandValidator(IStudentExamRepository studentExamRepository)
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
                        if (!studentExam.StartDate.HasValue)
                            context.AddFailure(ErrorType.StudentExam_Not_Started.GetDescription());

                        if (!studentExam.EndDate.HasValue)
                            context.AddFailure(ErrorType.StudentExam_Not_Ended.GetDescription());

                        if (studentExam.IsAutomaticGraded)
                            context.AddFailure(ErrorType.StudentExam_Alreday_Graded.GetDescription());
                    }

                });
        }
    }
}
