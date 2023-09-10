using AutoMapper;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.Grade;
using ExaminationsSystem.Domain.Common.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.StudentExams.Commands.Grade
{
    public class GradeStudentExamCommandHandler : IRequestHandler<GradeStudentExamCommand, GradeStudentExamCommandResponse>
    {
        private readonly IStudentExamRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IValidator<GradeStudentExamCommand> _validator;
        public GradeStudentExamCommandHandler(IMapper mapper, IStudentExamRepository repository, IUnitOfWorkAsync unitOfWorkAsync, IValidator<GradeStudentExamCommand> validator
           )
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWorkAsync = unitOfWorkAsync;
            _validator = validator;
        }

        public async Task<GradeStudentExamCommandResponse> Handle(GradeStudentExamCommand request, CancellationToken cancellationToken)
        {
            var response = new GradeStudentExamCommandResponse();

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Code = StatusCode.ValidationError;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var studentExam = await _repository.Get(x => x.Id == request.Id && !x.IsDeleted)
                    .Include(x => x.Answers)
                    .ThenInclude(x => x.SelectedOptions)
                    .Include(x => x.Exam)
                    .ThenInclude(x => x.Questions)
                    .ThenInclude(x => x.Question)
                    .ThenInclude(x => x.Options)
                    .FirstOrDefaultAsync(cancellationToken);

                decimal score = 0;
                var studentAnswers = studentExam.Answers;
                var examQuestions = studentExam.Exam.Questions;

                foreach (var answer in studentAnswers)
                {
                    var examQuestion = examQuestions.FirstOrDefault(x => x.QuestionId == answer.QuestionId);
                    if (examQuestion != null)
                    {
                        if (examQuestion.Question.Type == QuestionType.TrueFalse ||
                            examQuestion.Question.Type == QuestionType.MultipleChoice ||
                            examQuestion.Question.Type == QuestionType.MultipleChoiceWithMultipleAnswer)
                        {
                            var correctOptions = examQuestion.Question.Options.Where(x => x.IsCorrect).ToList();
                            var selectedOptions = answer.SelectedOptions.Select(o => o.OptionId).ToList();
                            bool answerAll = correctOptions.Select(x => x.Id).All(selectedOptions.Contains);
                            if (answerAll) score += examQuestion.Points;
                            answer.IsGraded = true;
                        }
                        else if (examQuestion.Question.Type == QuestionType.FillInTheBlank && answer.AnswerText == examQuestion.Question.CorrectAnswer)
                        {
                            score += examQuestion.Points;
                            answer.IsGraded = true;
                        }
                    }
                }
                studentExam.IsAutomaticGraded = true;
                studentExam.AutomaticGradeDate = DateTime.Now;
                studentExam.AutomaticGradeScore = score;
                studentExam = _repository.Update(studentExam);
                await _unitOfWorkAsync.CommitAsync();
                response.Data = new GradeStudentExamResponseViewModel()
                {
                    Id = request.Id,
                    Score = score,
                };
            }
            return response;
        }
    }
}
