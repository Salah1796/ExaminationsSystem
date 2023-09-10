
using AutoMapper;
using ExaminationsSystem.API.Controllers.Base;
using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Answer;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Create;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Delete;
using ExaminationsSystem.Application.Features.StudentExams.Commands.End;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Grade;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Start;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using ExaminationsSystem.Application.Features.StudentExams.Queries.GetById;
using ExaminationsSystem.Application.Features.StudentExams.Queries.GetList;
using ExaminationsSystem.Application.Models.ViewModels.StudentExams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentExaminationsSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentExamController : CRUDController<CreateStudentExamCommand, UpdateStudentExamCommand, DeleteStudentExamCommand, GetStudentExamByIdQuery, GetStudentExamsListQuery
        , StudentExamSearchModel, StudentExamCreateViewModel, StudentExamEditViewModel, StudentExamViewModel, StudentExamLightViewModel>
    {
        public StudentExamController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }


        [Route("Start/{Id}")]
        [HttpPut]
        public virtual async Task<BaseResponse<StudentExamViewModel>> Start(Guid Id)
        {
            var ExamDeleteCommand = new StartStudentExamCommand { Id = Id };
            var response = await _mediator.Send(ExamDeleteCommand);
            var result = _mapper.Map<BaseResponse<StudentExamViewModel>>(response);
            return result;
        }


        [Route("End/{Id}")]
        [HttpPut]
        public virtual async Task<BaseResponse<StudentExamViewModel>> End(Guid Id)
        {
            var ExamDeleteCommand = new EndStudentExamCommand { Id = Id };
            var response = await _mediator.Send(ExamDeleteCommand);
            var result = _mapper.Map<BaseResponse<StudentExamViewModel>>(response);
            return result;
        }

        [Route("Grade/{Id}")]
        [HttpPut]
        public virtual async Task<BaseResponse<StudentExamViewModel>> Grade(Guid Id)
        {
            var ExamDeleteCommand = new GradeStudentExamCommand { Id = Id };
            var response = await _mediator.Send(ExamDeleteCommand);
            var result = _mapper.Map<BaseResponse<StudentExamViewModel>>(response);
            return result;
        }

        [Route("Answer")]
        [HttpPut]
        public virtual async Task<BaseResponse<StudentAnswerViewModel>> Answer(StudentExamAnswerViewModel studentExamAnswer)
        {
            var result = new BaseResponse<StudentAnswerViewModel>();
            var createCommand = _mapper.Map<AnswerStudentExamCommand>(studentExamAnswer);
            if (createCommand != null)
            {
                var response = await _mediator.Send(createCommand);
                result = _mapper.Map<BaseResponse<StudentAnswerViewModel>>(response);
            }
            return result;
        }

    }
}

