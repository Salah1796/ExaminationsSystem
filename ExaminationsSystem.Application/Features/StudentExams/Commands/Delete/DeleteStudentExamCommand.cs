using ExaminationsSystem.Application.Features.Base;
using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Delete
{
    public class DeleteStudentExamCommand : IRequest<DeleteStudentExamCommandResponse>, IDeleteCommand
    {
        public Guid Id { get; set; }
    }
}
