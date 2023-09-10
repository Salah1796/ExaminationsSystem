using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using ExaminationsSystem.Domain.Common.Enums;
using ExaminationsSystem.Domain.Entities;
using Moq;
using StudentExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using StudentExaminationsSystem.Application.Test.Features.StudentExams.Base;
using Xunit;

namespace StudentExaminationsSystem.Application.Test.Features.StudentExams.Commands
{
    public class Update : BaseClass
    {
        private readonly UpdateStudentExamCommandHandler _handler;

        public Update()
        {
            _handler = new UpdateStudentExamCommandHandler(_mapper, _repository.Object, _unitOfWorkAsync.Object, _updatValidator);
        }

        [Fact]
        public async Task Update_Success_Return_UpdateStudentExamCommandResponse()
        {
            UpdateStudentExamCommand request = new()
            {
                Id = Guid.NewGuid(),
                ManualGradeScore = 10,
            };

            var StudentExam = _mapper.Map<StudentExam>(request);
            StudentExam.Status = StudentExamStatus.Created;

            _repository.Setup(x => x.Update(It.IsAny<StudentExam>()))
               .Returns(StudentExam);

            _unitOfWorkAsync.Setup(x => x.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(StatusCode.Ok, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(10, result.Data.ManualGradeScore);

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
