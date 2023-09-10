using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuestioninationsSystem.Application.Features.Questions.Commands.Delete
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, DeleteQuestionCommandResponse>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<DeleteQuestionCommand> _validator;
        public DeleteQuestionCommandHandler(IQuestionRepository questionRepository, IUnitOfWorkAsync unitOfWorkAsync, IValidator<DeleteQuestionCommand> validator)
        {
            _questionRepository = questionRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<DeleteQuestionCommandResponse> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteQuestionCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = StatusCode.ValidationError;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                await _questionRepository.Delete(request.Id);
                await _unitOfWorkAsync.CommitAsync();
                response.Data = true;
            }
            return response;
        }
    }
}
