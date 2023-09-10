using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Features.Exams.Commands.Delete;
using ExaminationsSystem.Application.Test.Features.Exams.Base;
using Moq;
using Xunit;

namespace ExaminationsSystem.Application.Test.Features.Exams.Commands
{
    public class Delete : BaseClass
    {
        private readonly DeleteExamCommandHandler _handler;

        public Delete()
        {
            _handler = new DeleteExamCommandHandler(_repository.Object, _unitOfWorkAsync.Object, _deleteValidator);
        }

        [Fact]
        public async Task Delete_Success_Return_DeleteExamCommandResponse()
        {
            DeleteExamCommand request = new()
            {
                Id = Guid.NewGuid(),
            };

            _unitOfWorkAsync.Setup(x => x.CommitAsync())
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
