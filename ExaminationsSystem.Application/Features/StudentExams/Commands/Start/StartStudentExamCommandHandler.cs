using AutoMapper;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Common.Extensions;
using ExaminationsSystem.Application.Contracts.Identity;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.StudentExams;
using ExaminationsSystem.Domain.Common.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Start
{
    public class StartStudentExamCommandHandler : IRequestHandler<StartStudentExamCommand, StartStudentExamCommandResponse>
    {
        private readonly IStudentExamRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<StartStudentExamCommand> _validator;
        private readonly ICurrentStudentService _currentStudentService;
        public StartStudentExamCommandHandler(IMapper mapper, IStudentExamRepository repository, IUnitOfWorkAsync unitOfWorkAsync,
            IValidator<StartStudentExamCommand> validator, ICurrentStudentService currentStudentService)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
            _currentStudentService = currentStudentService;
        }

        public async Task<StartStudentExamCommandResponse> Handle(StartStudentExamCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return new()
                {
                    Success = false,
                    Code = StatusCode.ValidationError,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };

            var studentExam = await _repository.GetByIdAsync(request.Id);

            var currentStudentId = _currentStudentService.GetCurrentStudentId();
            if (string.IsNullOrEmpty(currentStudentId?.ToString()) || studentExam.StudentId != currentStudentId)
                return new()
                {
                    Success = false,
                    Code = StatusCode.Unauthorized,
                    Message = StatusCode.Unauthorized.GetDescription()
                };

            studentExam.StartDate = DateTime.Now;
            studentExam.Status = StudentExamStatus.Started;
            studentExam = _repository.Update(studentExam);
            await _unitOfWorkAsync.CommitAsync();
            //TODO create Backgroud jop to end exam after its Duration
            return new()
            {
                Data = _mapper.Map<StartStudentExamResponseViewModel>(studentExam)
            };
        }
    }
}
