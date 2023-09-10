using AutoMapper;
using ExaminationsSystem.Application.AutoMapper;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.Questions.Commands.Update;
using FluentValidation;
using Moq;
using QuestioninationsSystem.Application.Features.Questions.Commands.Create;
using QuestioninationsSystem.Application.Features.Questions.Commands.Delete;
using QuestioninationsSystem.Application.Validation.Questions;

namespace ExaminationsSystem.Application.Test.Features.Questions.Base
{
    public class BaseClass
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<IQuestionRepository> _repository;
        protected readonly Mock<IUnitOfWorkAsync> _UnitOfWorkAsync;
        protected readonly IValidator<CreateQuestionCommand> _createValidator;
        protected readonly IValidator<UpdateQuestionCommand> _updatValidator;
        protected readonly IValidator<DeleteQuestionCommand> _deleteValidator;


        public BaseClass()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
            _repository = new Mock<IQuestionRepository>();
            _UnitOfWorkAsync = new Mock<IUnitOfWorkAsync>();
            _createValidator = new CreateQuestionCommandValidator();
            _updatValidator = new UpdateQuestionCommandValidator();
            _deleteValidator = new DeleteQuestionCommandValidator();
        }
    }
}
