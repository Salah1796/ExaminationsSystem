using ExaminationsSystem.Application.Features.Base;
using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.Exams.Commands.Delete
{
    public class DeleteExamCommand : IRequest<DeleteExamCommandResponse>, IDeleteCommand
    {
        public Guid Id { get; set; }
    }
}
