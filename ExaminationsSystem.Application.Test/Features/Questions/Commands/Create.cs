using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Test.Features.Questions.Base;
using ExaminationsSystem.Domain.Common.Enums;
using ExaminationsSystem.Domain.Entities;
using Moq;
using QuestioninationsSystem.Application.Features.Questions.Commands.Create;
using QuestioninationsSystem.Application.Features.Questions.Questions.Create;
using Xunit;

namespace ExaminationsSystem.Application.Test.Features.Questions.Commands
{
    public class Create : BaseClass
    {
        private readonly CreateQuestionCommandHandler _handler;

        public Create()
        {
            _handler = new CreateQuestionCommandHandler(_mapper, _repository.Object, _UnitOfWorkAsync.Object, _createValidator);
        }

        [Fact]
        public async Task Create_Success_Return_CreateQuestionCommandResponse()
        {
            CreateQuestionCommand request = new CreateQuestionCommand()
            {
                QuestionText = "q1",
                DifficultyLevel = DifficultyLevel.Easy,
                Type = QuestionType.MultipleChoice,
                Options = new()
                {
                    new ()
                    {
                        OptionText = "Option1",
                        IsCorrect = true
                    },
                    new ()
                    {
                        OptionText = "Option1",
                        IsCorrect = false
                    }
                },
            };

            var question = _mapper.Map<Question>(request);

            _repository.Setup(x => x.AddAsync(It.IsAny<Question>()))
               .ReturnsAsync(question);

            _UnitOfWorkAsync.Setup(x => x.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(StatusCode.Ok, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(request.QuestionText, result.Data.QuestionText);
            Assert.Equal(request.Type, result.Data.Type);
            Assert.Equal(request.DifficultyLevel, result.Data.DifficultyLevel);
            Assert.Equal(request.Options.Count, result.Data.Options.Count);
        }

        [Fact]
        public async Task Create_Fail_Return_ValidationError()
        {
            var result = await _handler.Handle(new(), CancellationToken.None);
            Assert.False(result.Success);
            Assert.Equal(StatusCode.ValidationError, result.Code);
            Assert.Null(result.Data);
            Assert.NotEmpty(result.ValidationErrors);
        }
    }
}
