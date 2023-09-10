using AutoMapper;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Common.Extensions;
using ExaminationsSystem.Application.Contracts.Identity;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.Answer;
using ExaminationsSystem.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Answer
{
    public class AnswerStudentExamCommandHandler : IRequestHandler<AnswerStudentExamCommand, AnswerStudentExamCommandResponse>
    {
        private readonly IStudentAnswerRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<AnswerStudentExamCommand> _validator;
        private readonly ICurrentStudentService _currentStudentService;
        private readonly IStudentExamRepository _studentExamRepository;
        public AnswerStudentExamCommandHandler(IMapper mapper, IStudentAnswerRepository repository, IUnitOfWorkAsync unitOfWorkAsync,
            IValidator<AnswerStudentExamCommand> validator, ICurrentStudentService currentStudentService, IStudentExamRepository studentExamRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
            _currentStudentService = currentStudentService;
            _studentExamRepository = studentExamRepository;
        }

        public async Task<AnswerStudentExamCommandResponse> Handle(AnswerStudentExamCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return new()
                {
                    Success = false,
                    Code = StatusCode.ValidationError,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };

            var currentStudentId = _currentStudentService.GetCurrentStudentId();
            var studentExamExist = await _studentExamRepository.AnyAsync(x => x.Id == request.StudentExamId && x.StudentId == currentStudentId && !x.IsDeleted);
            if (!studentExamExist)
                return new()
                {
                    Success = false,
                    Code = StatusCode.Unauthorized,
                    Message = StatusCode.Unauthorized.GetDescription()
                };

            var answer = await _repository.Get(x => x.StudentExamId == request.StudentExamId && x.QuestionId == request.QuestionId)
              .Include(x => x.StudentExam).FirstOrDefaultAsync(cancellationToken);
            if (answer == null)
            {
                answer = _mapper.Map<StudentAnswer>(request);
                answer = await _repository.AddAsync(answer);
            }
            else
            {
                answer.AnswerText = request.AnswerText;
                answer.SelectedOptions = request.SelectedOptionsId.Select(x => new StudentSelectedOption()
                {
                    OptionId = x
                }).ToList();
                answer = _repository.Update(answer);
            }
            await _unitOfWorkAsync.CommitAsync();
            return new AnswerStudentExamCommandResponse()
            {
                Data = _mapper.Map<AnswerStudentExamResponseViewModel>(answer)
            };
        }
    }
}
