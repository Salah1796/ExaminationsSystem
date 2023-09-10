using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Features.Exams.Commands.Create;
using ExaminationsSystem.Application.Test.Features.Exams.Base;
using ExaminationsSystem.Domain.Entities;
using Moq;
using Xunit;

namespace ExaminationsSystem.Application.Test.Features.Exams.Commands
{
    public class Create : BaseClass
    {
        private readonly CreateExamCommandHandler _handler;

        public Create()
        {
            _handler = new CreateExamCommandHandler(_mapper, _repository.Object, _unitOfWorkAsync.Object, _createValidator);
        }

        [Fact]
        public async Task Create_Success_Return_CreateExamCommandResponse()
        {
            CreateExamCommand request = new CreateExamCommand()
            {
                Name = "name",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now.AddDays(10),
                Duration = 120,
            };

            var Exam = _mapper.Map<Exam>(request);

            _repository.Setup(x => x.AddAsync(It.IsAny<Exam>()))
               .ReturnsAsync(Exam);

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
