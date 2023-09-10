using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Test.Features.Questions.Base;
using Moq;
using QuestioninationsSystem.Application.Features.Questions.Commands.Delete;
using Xunit;

namespace ExaminationsSystem.Application.Test.Features.Questions.Commands
{
    public class Delete : BaseClass
    {
        private readonly DeleteQuestionCommandHandler _handler;

        public Delete()
        {
            _handler = new DeleteQuestionCommandHandler(_repository.Object, _UnitOfWorkAsync.Object, _deleteValidator);
        }

        [Fact]
        public async Task Delete_Success_Return_DeleteQuestionCommandResponse()
        {
            DeleteQuestionCommand request = new()
            {
                Id = Guid.NewGuid(),
            };

            _UnitOfWorkAsync.Setup(x => x.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(StatusCode.Ok, result.Code);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task Delete_Fail_Return_ValidationError()
        {
            var result = await _handler.Handle(new(), CancellationToken.None);
            Assert.False(result.Success);
            Assert.Equal(StatusCode.ValidationError, result.Code);
            Assert.False(result.Data);
            Assert.NotEmpty(result.ValidationErrors);
        }
    }
}
