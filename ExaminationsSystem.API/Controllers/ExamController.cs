
using AutoMapper;
using ExaminationsSystem.API.Controllers.Base;
using ExaminationsSystem.Application.Features.Exams.Commands.Create;
using ExaminationsSystem.Application.Features.Exams.Commands.Delete;
using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using ExaminationsSystem.Application.Features.Exams.Queries.GetById;
using ExaminationsSystem.Application.Features.Exams.Queries.GetList;
using ExaminationsSystem.Application.Models.ViewModels.Exams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationsSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamController : CRUDController<CreateExamCommand, UpdateExamCommand, DeleteExamCommand, GetExamByIdQuery, GetExamListQuery
        , ExamSearchModel, ExamCreateViewModel, ExamEditViewModel, ExamViewModel, ExamLightViewModel>
    {
        public ExamController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }
    }
}

