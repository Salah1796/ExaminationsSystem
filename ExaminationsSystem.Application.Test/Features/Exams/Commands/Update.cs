using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using ExaminationsSystem.Application.Test.Features.Exams.Base;
using ExaminationsSystem.Domain.Entities;
using Moq;
using Xunit;

namespace ExaminationsSystem.Application.Test.Features.Exams.Commands
{
    public class Update : BaseClass
    {
        private readonly UpdateExamCommandHandler _handler;

        public Update()
        {
            _handler = new UpdateExamCommandHandler(_mapper, _repository.Object, _unitOfWorkAsync.Object, _updatValidator);
        }

        [Fact]
        public async Task Update_Success_Return_UpdateExamCommandResponse()
        {
            UpdateExamCommand request = new()
            {
                Id = Guid.NewGuid(),
                Name = "name",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(10),
                Duration = 120,
            };

            var Exam = _mapper.Map<Exam>(request);

            _repository.Setup(x => x.Update(It.IsAny<Exam>()))
               .Returns(Exam);

            _unitOfWorkAsync.Setup(x => x.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(StatusCode.Ok, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(request.Name, result.Data.Name);
            Assert.Equal(request.FromDate, result.Data.FromDate);
            Assert.Equal(request.ToDate, result.Data.ToDate);
            Assert.Equal(request.Duration, result.Data.Duration);
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
