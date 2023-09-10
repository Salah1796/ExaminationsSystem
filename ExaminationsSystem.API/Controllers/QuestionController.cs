
using AutoMapper;
using ExaminationsSystem.API.Controllers.Base;
using ExaminationsSystem.Application.Features.Exams.Queries.GetById;
using ExaminationsSystem.Application.Features.Exams.Queries.GetList;
using ExaminationsSystem.Application.Features.Questions.Commands.Update;
using ExaminationsSystem.Application.Models.ViewModels.Exams;
using ExaminationsSystem.Application.Models.ViewModels.Questions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestioninationsSystem.Application.Features.Questions.Commands.Create;
using QuestioninationsSystem.Application.Features.Questions.Commands.Delete;

namespace QuestioninationsSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : CRUDController<CreateQuestionCommand, UpdateQuestionCommand, DeleteQuestionCommand, GetExamByIdQuery, GetExamListQuery
        , QuestionSearchModel, QuestionCreateViewModel, QuestionEditViewModel, QuestionViewModel, QuestionLightViewModel>
    {
        public QuestionController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }
    }
}

