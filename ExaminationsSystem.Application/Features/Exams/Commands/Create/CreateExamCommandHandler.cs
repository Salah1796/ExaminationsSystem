using AutoMapper;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.Exams;
using ExaminationsSystem.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.Exams.Commands.Create
{
    public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, CreateExamCommandResponse>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<CreateExamCommand> _validator;
        public CreateExamCommandHandler(IMapper mapper, IExamRepository examRepository, IUnitOfWorkAsync unitOfWorkAsync,
            IValidator<CreateExamCommand> validator)
        {
            _mapper = mapper;
            _examRepository = examRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<CreateExamCommandResponse> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateExamCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = Common.Enums.StatusCode.ValidationError;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var exam = _mapper.Map<Exam>(request);
                exam = await _examRepository.AddAsync(exam);
                await _unitOfWorkAsync.CommitAsync();
                response.Data = _mapper.Map<CreateExamResponseViewModel>(exam);
            }
            return response;
        }
    }
}
