using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using MediatR;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Features.Exams.Commands.Create
{
    public class CreateExamCommand : IRequest<CreateExamCommandResponse>
    {
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<ExamQuestionCreateViewModel> Questions { get; set; }
    }
}
