using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.End
{
    public class EndStudentExamCommand : IRequest<EndStudentExamCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
