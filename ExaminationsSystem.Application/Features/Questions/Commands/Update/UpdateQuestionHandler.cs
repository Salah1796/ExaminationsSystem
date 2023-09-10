using AutoMapper;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.Questions.Commands.Update;
using ExaminationsSystem.Application.Models.ViewModels.Questions;
using ExaminationsSystem.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuestioninationsSystem.Application.Features.Questions.Commands.Update
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, UpdateQuestionCommandResponse>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<UpdateQuestionCommand> _validator;
        public UpdateQuestionCommandHandler(IMapper mapper, IQuestionRepository questionRepository, IUnitOfWorkAsync unitOfWorkAsync, IValidator<UpdateQuestionCommand> validator)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<UpdateQuestionCommandResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateQuestionCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = StatusCode.ValidationError;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var exam = _mapper.Map<Question>(request);
                exam = _questionRepository.Update(exam);
                await _unitOfWorkAsync.CommitAsync();
                response.Data = _mapper.Map<UpdateQuestionResponseViewModel>(exam);
            }
            return response;
        }
    }
}
