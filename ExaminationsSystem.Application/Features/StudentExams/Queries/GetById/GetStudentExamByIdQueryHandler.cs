using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExaminationsSystem.Application.Common.Enums;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Queries.GetById;
using ExaminationsSystem.Application.Models.ViewModels.StudentExams;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentExaminationsSystem.Application.Features.StudentExams.Queries.GetById
{
    public class GetStudentExamByIdQueryHandler : IRequestHandler<GetStudentExamByIdQuery, GetStudentExamByIdResponse>
    {
        private readonly IStudentExamRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetStudentExamByIdQuery> _validator;
        public GetStudentExamByIdQueryHandler(IMapper mapper, IStudentExamRepository repository, IValidator<GetStudentExamByIdQuery> validator)
        {
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetStudentExamByIdResponse> Handle(GetStudentExamByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GetStudentExamByIdResponse();
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                #region Build Query
                var query = _repository.Get();
                query = query.Where(x => x.Id == request.Id);
                query = query.Where(x => x.IsDeleted == false);
                #endregion

                var exam = await query
                    .Include(x => x.Exam)
                    .ThenInclude(x => x.Questions)
                    .ThenInclude(x => x.Question)
                    .ThenInclude(x => x.Options)
                    .Include(x => x.Answers)
                    .ThenInclude(x => x.SelectedOptions)
                    .ProjectTo<GetStudentExamByIdRespnseViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

                if (exam != null)
                    response.Data = exam;
                else
                {
                    response.Success = false;
                    response.Code = StatusCode.NotFound;
                    //todo read from enum error types
                    response.Message = $" credential with id  =  {request.Id} not exist ";
                }
            }
            return response;
        }
    }
}
