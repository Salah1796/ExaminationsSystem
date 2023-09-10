using AutoMapper;
using ExaminationsSystem.Application.AutoMapper;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.Exams.Commands.Create;
using ExaminationsSystem.Application.Features.Exams.Commands.Delete;
using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using ExaminationsSystem.Application.Validation.Exams;
using FluentValidation;
using Moq;

namespace ExaminationsSystem.Application.Test.Features.Exams.Base
{
    public class BaseClass
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<IExamRepository> _repository;
        protected readonly Mock<IUnitOfWorkAsync> _unitOfWorkAsync;
        protected readonly IValidator<CreateExamCommand> _createValidator;
        protected readonly IValidator<UpdateExamCommand> _updatValidator;
        protected readonly IValidator<DeleteExamCommand> _deleteValidator;


        public BaseClass()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _repository = new Mock<IExamRepository>();
            _unitOfWorkAsync = new Mock<IUnitOfWorkAsync>();
            _createValidator = new CreateExamCommandValidator();
            _updatValidator = new UpdateExamCommandValidator();
            _deleteValidator = new DeleteExamCommandValidator();
        }
    }
}
