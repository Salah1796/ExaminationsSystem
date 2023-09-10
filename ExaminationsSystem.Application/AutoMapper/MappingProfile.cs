using AutoMapper;
using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Features.Exams.Commands.Create;
using ExaminationsSystem.Application.Features.Exams.Commands.Delete;
using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using ExaminationsSystem.Application.Features.Exams.Queries.GetById;
using ExaminationsSystem.Application.Features.Exams.Queries.GetList;
using ExaminationsSystem.Application.Features.Questions.Commands.Update;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Answer;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Create;
using ExaminationsSystem.Application.Features.StudentExams.Commands.End;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Grade;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Start;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using ExaminationsSystem.Application.Features.StudentExams.Queries.GetById;
using ExaminationsSystem.Application.Features.StudentExams.Queries.GetList;
using ExaminationsSystem.Application.Models.ViewModels.Answer;
using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using ExaminationsSystem.Application.Models.ViewModels.Exams;
using ExaminationsSystem.Application.Models.ViewModels.Grade;
using ExaminationsSystem.Application.Models.ViewModels.Identity;
using ExaminationsSystem.Application.Models.ViewModels.Options;
using ExaminationsSystem.Application.Models.ViewModels.Questions;
using ExaminationsSystem.Application.Models.ViewModels.StudentExams;
using ExaminationsSystem.Domain.Entities;
using QuestioninationsSystem.Application.Features.Questions.Commands.Create;
using QuestioninationsSystem.Application.Features.Questions.Commands.Delete;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationsSystem.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Exams

            #region Create
            CreateMap<ExamCreateViewModel, CreateExamCommand>();
            CreateMap<ExamQuestionCreateViewModel, ExamQuestion>();
            CreateMap<CreateExamCommand, Exam>();
            CreateMap<Exam, CreateExamResponseViewModel>();
            CreateMap<CreateExamResponseViewModel, ExamViewModel>();
            CreateMap<CreateExamCommandResponse, BaseResponse<ExamViewModel>>();

            #endregion

            #region Update
            CreateMap<ExamEditViewModel, UpdateExamCommand>();
            CreateMap<UpdateExamCommand, Exam>();
            CreateMap<Exam, UpdateExamResponseViewModel>();
            CreateMap<UpdateExamResponseViewModel, ExamViewModel>();
            CreateMap<UpdateExamCommandResponse, BaseResponse<ExamViewModel>>();
            #endregion

            #region Search
            CreateMap<ExamSearchModel, GetExamListQuery>();
            CreateMap<Exam, GetExamsListRespnseViewModel>();
            CreateMap<GetExamsListRespnseViewModel, ExamLightViewModel>();
            CreateMap<GetExamsListResponse, BasePaginatedResponse<List<ExamLightViewModel>>>();
            #endregion

            #region GetById
            CreateMap<Exam, GetExamByIdRespnseViewModel>();
            CreateMap<GetExamByIdRespnseViewModel, ExamViewModel>();
            CreateMap<GetExamByIdResponse, BaseResponse<ExamViewModel>>();
            #endregion

            CreateMap<ExamViewModel, ExamEditViewModel>();

            #region Delete
            CreateMap<DeleteExamCommandResponse, BaseResponse<bool>>();
            #endregion

            #endregion

            #region Question

            #region Create
            CreateMap<QuestionCreateViewModel, CreateQuestionCommand>();
            CreateMap<CreateQuestionCommand, Question>();
            CreateMap<Question, CreateQuestionResponseViewModel>();
            CreateMap<CreateQuestionResponseViewModel, QuestionViewModel>();
            CreateMap<CreateQuestionCommandResponse, BaseResponse<QuestionViewModel>>();

            #endregion

            #region Update
            CreateMap<QuestionEditViewModel, UpdateQuestionCommand>();
            CreateMap<UpdateQuestionCommand, Question>();
            CreateMap<Question, UpdateQuestionResponseViewModel>();
            CreateMap<UpdateQuestionResponseViewModel, QuestionViewModel>();
            CreateMap<UpdateQuestionCommandResponse, BaseResponse<QuestionViewModel>>();
            #endregion

            #region Delete
            CreateMap<DeleteQuestionCommandResponse, BaseResponse<bool>>();
            #endregion

            #endregion

            #region Option
            CreateMap<QuestionOption, QuestionOptionViewModel>().ReverseMap();
            #endregion

            #region StudentExam

            #region Create
            CreateMap<StudentExamCreateViewModel, CreateStudentExamCommand>();
            CreateMap<CreateStudentExamCommand, StudentExam>();
            CreateMap<StudentExam, CreateStudentExamResponseViewModel>();
            CreateMap<CreateStudentExamResponseViewModel, StudentExamViewModel>();
            CreateMap<CreateStudentExamCommandResponse, BaseResponse<StudentExamViewModel>>();

            #endregion

            #region Update
            CreateMap<StudentExamEditViewModel, UpdateStudentExamCommand>();
            CreateMap<UpdateStudentExamCommand, StudentExam>();
            CreateMap<StudentExam, UpdateStudentExamResponseViewModel>();
            CreateMap<UpdateStudentExamResponseViewModel, StudentExamViewModel>();
            CreateMap<UpdateStudentExamCommandResponse, BaseResponse<StudentExamViewModel>>();
            #endregion

            #region Start
            CreateMap<StudentExam, StartStudentExamResponseViewModel>();
            CreateMap<StartStudentExamResponseViewModel, StudentExamViewModel>();
            CreateMap<StartStudentExamCommandResponse, BaseResponse<StudentExamViewModel>>();
            #endregion

            #region End
            CreateMap<StudentExam, EndStudentExamResponseViewModel>();
            CreateMap<EndStudentExamResponseViewModel, StudentExamViewModel>();
            CreateMap<EndStudentExamCommandResponse, BaseResponse<StudentExamViewModel>>();
            #endregion

            #region Grade
            CreateMap<StudentExam, GradeStudentExamResponseViewModel>();
            CreateMap<GradeStudentExamResponseViewModel, StudentExamViewModel>();
            CreateMap<GradeStudentExamCommandResponse, BaseResponse<StudentExamViewModel>>();
            #endregion

            #region Answer
            CreateMap<StudentExamAnswerViewModel, AnswerStudentExamCommand>();
            CreateMap<AnswerStudentExamCommand, StudentAnswer>()
                .ForMember(x => x.SelectedOptions, op => op.MapFrom(src => src.SelectedOptionsId.Select(x => new StudentSelectedOption()
                {
                    OptionId = x
                }).ToList()));
            CreateMap<StudentAnswer, AnswerStudentExamResponseViewModel>()
                .ForMember(x => x.SelectedOptionsId, op => op.MapFrom(src => src.SelectedOptions.Select(x => x.OptionId).ToList()));
            CreateMap<AnswerStudentExamResponseViewModel, StudentAnswerViewModel>();
            CreateMap<AnswerStudentExamCommandResponse, BaseResponse<StudentAnswerViewModel>>();
            #endregion

            #region Search
            CreateMap<StudentExamSearchModel, GetStudentExamsListQuery>();
            CreateMap<StudentExam, GetStudentExamsListRespnseViewModel>();
            CreateMap<GetStudentExamsListRespnseViewModel, StudentExamLightViewModel>();
            CreateMap<GetStudentExamsListResponse, BasePaginatedResponse<List<StudentExamLightViewModel>>>();
            #endregion

            #region GetById
            CreateMap<StudentExam, GetStudentExamByIdRespnseViewModel>()
                 .ForMember(x => x.Questions, op => op.MapFrom(src => src.Exam.Questions.Select(examQuestion => new StudentExamQuestionViewModel()
                 {
                     QuestionId = examQuestion.QuestionId,
                     QuestionText = examQuestion.Question.QuestionText,
                     DifficultyLevel = examQuestion.Question.DifficultyLevel,
                     Points = examQuestion.Points,
                     Type = examQuestion.Question.Type,
                     Options = examQuestion.Question.Options.Select(op => new StudentExamQuestionOptionViewModel()
                     {
                         Id = op.Id,
                         OptionText = op.OptionText,
                     }).ToList(),
                     StudentAnswer = src.Answers.Any(answer => answer.QuestionId == examQuestion.Question.Id) ?
                     new()
                     {
                         Id = src.Answers.FirstOrDefault(a => a.QuestionId == examQuestion.Question.Id)!.Id,
                         AnswerText = src.Answers.FirstOrDefault(a => a.QuestionId == examQuestion.Question.Id)!.AnswerText,
                         SelectedOptionsId = src.Answers.FirstOrDefault(a => a.QuestionId == examQuestion.Question.Id)!.SelectedOptions.Select(op => op.OptionId).ToList(),
                     } : null
                 }).ToList()));

            CreateMap<GetStudentExamByIdRespnseViewModel, StudentExamViewModel>();
            CreateMap<GetStudentExamByIdResponse, BaseResponse<StudentExamViewModel>>();
            #endregion
            #endregion

            #region Auth
            CreateMap<RegistrationRequest, Student>();
            #endregion

        }
    }
}
