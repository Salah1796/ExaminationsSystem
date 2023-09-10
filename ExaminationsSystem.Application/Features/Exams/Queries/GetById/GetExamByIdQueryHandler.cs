using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.Exams;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Features.Exams.Queries.GetById
{
    public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, GetExamByIdResponse>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetExamByIdQuery> _validator;
        public GetExamByIdQueryHandler(IMapper mapper, IExamRepository examRepository, IValidator<GetExamByIdQuery> validator)
        {
            _mapper = mapper;
            _examRepository = examRepository;
            _validator = validator;
        }

        public async Task<GetExamByIdResponse> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetExamByIdResponse();
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                #region Build Query
                var query = _examRepository.Get();
                query = query.Where(x => x.Id == request.Id);
                query = query.Where(x => x.IsDeleted == false);
                #endregion

                var exam = await query.ProjectTo<GetExamByIdRespnseViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                if (exam != null)
                    response.Data = exam;
                else
                {
                    response.Success = false;
                    response.Code = Common.Enums.StatusCode.NotFound;
                    //todo read from enum error types
                    response.Message = $" credential with id  =  {request.Id} not exist ";
                }

            }
            return response;
        }
    }
}
