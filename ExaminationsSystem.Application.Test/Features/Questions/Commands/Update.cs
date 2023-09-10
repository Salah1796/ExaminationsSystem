using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Features.Questions.Commands.Update;
using ExaminationsSystem.Application.Test.Features.Questions.Base;
using ExaminationsSystem.Domain.Common.Enums;
using ExaminationsSystem.Domain.Entities;
using Moq;
using QuestioninationsSystem.Application.Features.Questions.Commands.Update;
using Xunit;

namespace ExaminationsSystem.Application.Test.Features.Questions.Commands
{
    public class Update : BaseClass
    {
        private readonly UpdateQuestionCommandHandler _handler;

        public Update()
        {
            _handler = new UpdateQuestionCommandHandler(_mapper, _repository.Object, _UnitOfWorkAsync.Object, _updatValidator);
        }

        [Fact]
        public async Task Update_Success_Return_UpdateQuestionCommandResponse()
        {
            UpdateQuestionCommand request = new()
            {
                Id = Guid.NewGuid(),
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

            _repository.Setup(x => x.Update(It.IsAny<Question>()))
               .Returns(question);

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
            Assert.Equal(request.Id, result.Data.Id);

        }

        [Fact]
        public async Task Update_Fail_Return_ValidationError()
        {
            var result = await _handler.Handle(new(), CancellationToken.None);
            Assert.False(result.Success);
            Assert.Equal(StatusCode.ValidationError, result.Code);
            Assert.Null(result.Data);
            Assert.NotEmpty(result.ValidationErrors);
        }
    }
}
