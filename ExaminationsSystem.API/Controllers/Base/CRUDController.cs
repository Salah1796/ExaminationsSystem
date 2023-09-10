
using AutoMapper;
using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Features.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationsSystem.API.Controllers.Base
{
    public abstract class CRUDController<TCreateCommand, TUpdateCommand, TDeleteCommand, TGetByIdQuery, TGetListQuery, TSearchModel, TCreat, TUpdate, TView, TLight> : ControllerBase
        where TDeleteCommand : IDeleteCommand, new() where TGetByIdQuery : IGetByIdQuery, new() where TSearchModel : new()
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;
        public CRUDController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("Search")]
        [HttpPost]

        public virtual async Task<BasePaginatedResponse<List<TLight>>> Search(TSearchModel searchModel)
        {
            var result = new BasePaginatedResponse<List<TLight>>();
            var examesListQuery = _mapper.Map<TGetListQuery>(searchModel);
            if (examesListQuery != null)
            {
                var response = await _mediator.Send(examesListQuery);
                result = _mapper.Map<BasePaginatedResponse<List<TLight>>>(response);
            }
            return result;
        }

        [Route("Get/{Id}")]
        [HttpGet]
        public virtual async Task<BaseResponse<TView>> GetById(Guid Id)
        {
            var result = new BaseResponse<TView>();
            var examByIdQuery = new TGetByIdQuery() { Id = Id };
            if (examByIdQuery != null)
            {
                var response = await _mediator.Send(examByIdQuery);
                result = _mapper.Map<BaseResponse<TView>>(response);
            }
            return result;
        }


        [Route("Add")]
        [HttpPost]
        public virtual async Task<BaseResponse<TView>> Add(TCreat createViewModel)
        {
            var result = new BaseResponse<TView>();
            var createCommand = _mapper.Map<TCreateCommand>(createViewModel);
            if (createCommand != null)
            {
                var response = await _mediator.Send(createCommand);
                result = _mapper.Map<BaseResponse<TView>>(response);
            }
            return result;
        }

        [Route("Update")]
        [HttpPut]
        public virtual async Task<BaseResponse<TView>> Update(TUpdate examEditViewModel)
        {
            var result = new BaseResponse<TView>();
            var updateCommand = _mapper.Map<TUpdateCommand>(examEditViewModel);
            if (updateCommand != null)
            {
                var response = await _mediator.Send(updateCommand);
                result = _mapper.Map<BaseResponse<TView>>(response);
            }
            return result;
        }


        [Route("Delete/{Id}")]
        [HttpDelete]
        public virtual async Task<BaseResponse<bool>> Delete(Guid Id)
        {
            var ExamDeleteCommand = new TDeleteCommand { Id = Id };
            var response = await _mediator.Send(ExamDeleteCommand);
            var result = _mapper.Map<BaseResponse<bool>>(response);
            return result;
        }
    }
}

