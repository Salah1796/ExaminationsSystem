using AutoMapper;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.StudentExams;
using ExaminationsSystem.Domain.Common.Enums;
using ExaminationsSystem.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Create
{
    public class CreateStudentExamCommandHandler : IRequestHandler<CreateStudentExamCommand, CreateStudentExamCommandResponse>
    {
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<CreateStudentExamCommand> _validator;
        public CreateStudentExamCommandHandler(IMapper mapper, IStudentExamRepository studentExamRepository, IUnitOfWorkAsync unitOfWorkAsync, IValidator<CreateStudentExamCommand> validator)
        {
            _mapper = mapper;
            _studentExamRepository = studentExamRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<CreateStudentExamCommandResponse> Handle(CreateStudentExamCommand request, CancellationToken cancellationToken)
        {
            var createStudentExamCommandResponse = new CreateStudentExamCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                createStudentExamCommandResponse.Success = false;
                createStudentExamCommandResponse.Code = StatusCode.ValidationError;
                createStudentExamCommandResponse.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var studentExam = _mapper.Map<StudentExam>(request);
                studentExam.Status = StudentExamStatus.Created;
                studentExam = await _studentExamRepository.AddAsync(studentExam);
                await _unitOfWorkAsync.CommitAsync();
                createStudentExamCommandResponse.Data = _mapper.Map<CreateStudentExamResponseViewModel>(studentExam);
            }
            return createStudentExamCommandResponse;
        }
    }
}
