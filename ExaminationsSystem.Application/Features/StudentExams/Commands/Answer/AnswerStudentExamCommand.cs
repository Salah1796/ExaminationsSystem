using MediatR;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Answer
{
    public class AnswerStudentExamCommand : IRequest<AnswerStudentExamCommandResponse>
    {
        public Guid StudentExamId { get; set; }
        public Guid QuestionId { get; set; }
        public List<Guid> SelectedOptionsId { get; set; }
        public string? AnswerText { get; set; }
    }
}
