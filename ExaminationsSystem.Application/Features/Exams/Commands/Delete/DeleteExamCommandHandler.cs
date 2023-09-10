using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.Exams.Commands.Delete
{
    public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, DeleteExamCommandResponse>
    {
        private readonly IExamRepository _examRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<DeleteExamCommand> _validator;
        public DeleteExamCommandHandler(IExamRepository examRepository, IUnitOfWorkAsync unitOfWorkAsync, IValidator<DeleteExamCommand> validator)
        {
            _examRepository = examRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<DeleteExamCommandResponse> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteExamCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = Common.Enums.StatusCode.ValidationError;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                await _examRepository.Delete(request.Id);
                await _unitOfWorkAsync.CommitAsync();
                response.Data = true;
            }
            return response;
        }
    }
}
