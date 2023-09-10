using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Grade
{
    public class GradeStudentExamCommand : IRequest<GradeStudentExamCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
