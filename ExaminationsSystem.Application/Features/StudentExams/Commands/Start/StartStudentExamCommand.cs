using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Start
{
    public class StartStudentExamCommand : IRequest<StartStudentExamCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
