using AutoMapper;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using ExaminationsSystem.Application.Models.ViewModels.StudentExams;
using ExaminationsSystem.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentExaminationsSystem.Application.Features.StudentExams.Commands.Update
{
    public class UpdateStudentExamCommandHandler : IRequestHandler<UpdateStudentExamCommand, UpdateStudentExamCommandResponse>
    {
        private readonly IStudentExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<UpdateStudentExamCommand> _validator;
        public UpdateStudentExamCommandHandler(IMapper mapper, IStudentExamRepository examRepository, IUnitOfWorkAsync unitOfWorkAsync,
            IValidator<UpdateStudentExamCommand> validator)
        {
            _mapper = mapper;
            _examRepository = examRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<UpdateStudentExamCommandResponse> Handle(UpdateStudentExamCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateStudentExamCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = StatusCode.ValidationError;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var exam = _mapper.Map<StudentExam>(request);
                exam = _examRepository.Update(exam);
                await _unitOfWorkAsync.CommitAsync();
                response.Data = _mapper.Map<UpdateStudentExamResponseViewModel>(exam);
            }
            return response;
        }
    }
}
