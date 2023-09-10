using AutoMapper;
using ExaminationsSystem.Application.AutoMapper;
using ExaminationsSystem.Application.Contracts.Identity;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Create;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Delete;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Start;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using ExaminationsSystem.Application.Validation.StudentExams;
using FluentValidation;
using Moq;

namespace StudentExaminationsSystem.Application.Test.Features.StudentExams.Base
{
    public class BaseClass
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<IStudentExamRepository> _repository;
        protected readonly Mock<IStudentRepository> _studentRepository;
        protected readonly Mock<IExamRepository> _examRepository;
        protected readonly Mock<ICurrentStudentService> _currentStudentService;

        protected readonly Mock<IUnitOfWorkAsync> _unitOfWorkAsync;
        protected readonly IValidator<CreateStudentExamCommand> _createValidator;
        protected readonly IValidator<UpdateStudentExamCommand> _updatValidator;
        protected readonly IValidator<DeleteStudentExamCommand> _deleteValidator;
        protected readonly IValidator<StartStudentExamCommand> _startValidator;
        public BaseClass()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _repository = new Mock<IStudentExamRepository>();
            _unitOfWorkAsync = new Mock<IUnitOfWorkAsync>();
            _studentRepository = new();
            _examRepository = new();
            _currentStudentService = new();
            _createValidator = new CreateStudentExamCommandValidator(_repository.Object, _examRepository.Object, _studentRepository.Object);
            _updatValidator = new UpdateStudentExamCommandValidator();
            _deleteValidator = new DeleteStudentExamCommandValidator();
            _startValidator = new StartStudentExamCommandValidator(_repository.Object);
        }
    }
}
