using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Update
{
    public class UpdateStudentExamCommand : IRequest<UpdateStudentExamCommandResponse>
    {
        public Guid Id { get; set; }
        public decimal ManualGradeScore { get; set; }
    }
}
