using ExaminationsSystem.Application.Features.Base;
using MediatR;
using System;

namespace QuestioninationsSystem.Application.Features.Questions.Commands.Delete
{
    public class DeleteQuestionCommand : IRequest<DeleteQuestionCommandResponse>, IDeleteCommand
    {
        public Guid Id { get; set; }
    }
}
