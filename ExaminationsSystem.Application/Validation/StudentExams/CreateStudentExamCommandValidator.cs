using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Common.Extensions;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Create;
using FluentValidation;

namespace ExaminationsSystem.Application.Validation.StudentExams
{
    public class CreateStudentExamCommandValidator : AbstractValidator<CreateStudentExamCommand>
    {
        public CreateStudentExamCommandValidator(IStudentExamRepository studentExamRepository,
            IExamRepository examRepository, IStudentRepository studentRepository)
        {
            RuleFor(i => i)
                .CustomAsync(async (command, context, cancellation) =>
                {
                    var studentExamExist = await studentExamRepository.AnyAsync(x => x.ExamId == command.ExamId && x.StudentId == command.StudentId && !x.IsDeleted, cancellation);
                    if (studentExamExist)
                        context.AddFailure(nameof(command.ExamId), ErrorType.Item_Alreday_Exist.GetDescription(ErrorType.StudentExam.GetDescription()));

                    var studentExist = await studentRepository.AnyAsync(x => x.Id == command.StudentId && !x.IsDeleted, cancellation);
                    if (!studentExist)
                        context.AddFailure(nameof(command.ExamId), ErrorType.Item_NotExist.GetDescription(ErrorType.Student.GetDescription()));

                    var examExist = await examRepository.AnyAsync(x => x.Id == command.ExamId && !x.IsDeleted, cancellation);
                    if (!examExist)
                        context.AddFailure(nameof(command.ExamId), ErrorType.Item_NotExist.GetDescription(ErrorType.Exam.GetDescription()));

                });
        }
    }
}
