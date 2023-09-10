using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Create;
using ExaminationsSystem.Domain.Common.Enums;
using ExaminationsSystem.Domain.Entities;
using Moq;
using StudentExaminationsSystem.Application.Test.Features.StudentExams.Base;
using System.Linq.Expressions;
using Xunit;

namespace StudentExaminationsSystem.Application.Test.Features.StudentExams.Commands
{
    public class Create : BaseClass
    {
        private readonly CreateStudentExamCommandHandler _handler;

        public Create()
        {
            _handler = new CreateStudentExamCommandHandler(_mapper, _repository.Object, _unitOfWorkAsync.Object, _createValidator);
        }

        [Fact]
        public async Task Create_Success_Return_CreateStudentExamCommandResponse()
        {
            CreateStudentExamCommand request = new()
            {
                ExamId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
            };

            var StudentExam = _mapper.Map<StudentExam>(request);
            StudentExam.Status = StudentExamStatus.Created;

            _repository.Setup(x => x.AddAsync(It.IsAny<StudentExam>()))
               .ReturnsAsync(StudentExam);

            _repository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<StudentExam, bool>>>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(false);

            _studentRepository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Student, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

            _examRepository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Exam, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

            _unitOfWorkAsync.Setup(x => x.CommitAsync())
                .ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);
            Assert.True(result.Success);
            Assert.Equal(StatusCode.Ok, result.Code);
            Assert.NotNull(result.Data);
            Assert.Equal(request.ExamId, result.Data.ExamId);
            Assert.Equal(request.StudentId, result.Data.StudentId);
            Assert.Equal(StudentExamStatus.Created, result.Data.Status);
            Assert.Null(result.Data.StartDate);
            Assert.Null(result.Data.EndDate);
            Assert.Null(result.Data.AutomaticGradeDate);
            Assert.Null(result.Data.ManualGradeDate);
            Assert.False(result.Data.IsManualGraded);
            Assert.False(result.Data.IsAutomaticGraded);
            Assert.Equal(0, result.Data.TotalScore);
            Assert.Equal(0, result.Data.ManualGradeScore);
            Assert.Equal(0, result.Data.AutomaticGradeScore);
        }

        [Fact]
        public async Task Create_Fail_Return_ValidationError()
        {
            _repository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<StudentExam, bool>>>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(false);
            var result = await _handler.Handle(new(), CancellationToken.None);
            Assert.False(result.Success);
            Assert.Equal(StatusCode.ValidationError, result.Code);
            Assert.Null(result.Data);
            Assert.NotEmpty(result.ValidationErrors);
        }
    }
}
