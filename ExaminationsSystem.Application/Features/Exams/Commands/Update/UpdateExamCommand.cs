using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using MediatR;
using System;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Features.Exams.Commands.Update
{
    public class UpdateExamCommand : IRequest<UpdateExamCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<ExamQuestionCreateViewModel> Questions { get; set; }
    }
}
