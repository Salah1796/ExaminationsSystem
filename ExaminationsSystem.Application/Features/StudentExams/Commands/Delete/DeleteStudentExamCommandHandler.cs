using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Delete;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentExaminationsSystem.Application.Features.StudentStudentExams.Commands.Delete
{
    public class DeleteStudentExamCommandHandler : IRequestHandler<DeleteStudentExamCommand, DeleteStudentExamCommandResponse>
    {
        private readonly IStudentExamRepository _examRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<DeleteStudentExamCommand> _validator;
        public DeleteStudentExamCommandHandler(IStudentExamRepository examRepository, IUnitOfWorkAsync unitOfWorkAsync, IValidator<DeleteStudentExamCommand> validator)
        {
            _examRepository = examRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<DeleteStudentExamCommandResponse> Handle(DeleteStudentExamCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteStudentExamCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = StatusCode.ValidationError;
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
