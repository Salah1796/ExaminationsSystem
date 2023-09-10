using AutoMapper;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.Questions;
using ExaminationsSystem.Domain.Entities;
using FluentValidation;
using MediatR;
using QuestioninationsSystem.Application.Features.Questions.Commands.Create;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuestioninationsSystem.Application.Features.Questions.Questions.Create
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IQuestionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<CreateQuestionCommand> _validator;
        public CreateQuestionCommandHandler(IMapper mapper, IQuestionRepository examRepository, IUnitOfWorkAsync unitOfWorkAsync,
            IValidator<CreateQuestionCommand> validator)
        {
            _mapper = mapper;
            _repository = examRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateQuestionCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = StatusCode.ValidationError;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var question = _mapper.Map<Question>(request);
                question = await _repository.AddAsync(question);
                await _unitOfWorkAsync.CommitAsync();
                response.Data = _mapper.Map<CreateQuestionResponseViewModel>(question);
            }
            return response;
        }
    }
}
