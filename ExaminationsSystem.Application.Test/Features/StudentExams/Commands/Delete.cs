using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Delete;
using Moq;
using StudentExaminationsSystem.Application.Features.StudentStudentExams.Commands.Delete;
using StudentExaminationsSystem.Application.Test.Features.StudentExams.Base;
using Xunit;

namespace StudentExaminationsSystem.Application.Test.Features.StudentExams.Commands
{
    public class Delete : BaseClass
    {
        private readonly DeleteStudentExamCommandHandler _handler;

        public Delete()
        {
            _handler = new DeleteStudentExamCommandHandler(_repository.Object, _unitOfWorkAsync.Object, _deleteValidator);
        }

        [Fact]
        public async Task Delete_Success_Return_DeleteStudentExamCommandResponse()
        {
            DeleteStudentExamCommand request = new()
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
