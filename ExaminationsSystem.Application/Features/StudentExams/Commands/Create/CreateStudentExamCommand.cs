using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Create
{
    public class CreateStudentExamCommand : IRequest<CreateStudentExamCommandResponse>
    {
        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
    }
}
