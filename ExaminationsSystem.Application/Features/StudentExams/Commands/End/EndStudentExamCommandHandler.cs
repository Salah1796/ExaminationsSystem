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
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.End
{
    public class EndStudentExamCommandHandler : IRequestHandler<EndStudentExamCommand, EndStudentExamCommandResponse>
    {
        private readonly IStudentExamRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<EndStudentExamCommand> _validator;
        private readonly ICurrentStudentService _currentStudentService;
        public EndStudentExamCommandHandler(IMapper mapper, IStudentExamRepository repository, IUnitOfWorkAsync unitOfWorkAsync, IValidator<EndStudentExamCommand> validator, ICurrentStudentService currentStudentService)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
            _currentStudentService = currentStudentService;
        }

        public async Task<EndStudentExamCommandResponse> Handle(EndStudentExamCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return new()
                {
                    Success = false,
                    Code = StatusCode.ValidationError,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };

            var studentExam = await _repository.Get(x => x.Id == request.Id && !x.IsDeleted).Include(x => x.Exam)
              .FirstOrDefaultAsync(cancellationToken);
            var currentStudentId = _currentStudentService.GetCurrentStudentId();
            if (string.IsNullOrEmpty(currentStudentId?.ToString()) || studentExam.StudentId != currentStudentId)
                return new()
                {
                    Success = false,
                    Code = StatusCode.Unauthorized,
                    Message = StatusCode.Unauthorized.GetDescription()
                };
            studentExam.EndDate = DateTime.Now;
            studentExam.Status = StudentExamStatus.Ended;
            studentExam = _repository.Update(studentExam);
            await _unitOfWorkAsync.CommitAsync();
            return new()
            {
                Data = _mapper.Map<EndStudentExamResponseViewModel>(studentExam)
            };
        }
    }
}
