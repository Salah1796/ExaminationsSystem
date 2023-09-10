using AutoMapper;
using AutoMapper.QueryableExtensions;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Features.StudentExams.Queries.GetList;
using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentExaminationsSystem.Application.Features.StudentExams.Queries.GetList
{
    public class GetStudentExamsListQueryHandler : IRequestHandler<GetStudentExamsListQuery, GetStudentExamsListResponse>
    {
        private readonly IStudentExamRepository _examRepository;
        private readonly IMapper _mapper;
        public GetStudentExamsListQueryHandler(IMapper mapper, IStudentExamRepository examRepository)
        {
            _mapper = mapper;
            _examRepository = examRepository;
        }

        public async Task<GetStudentExamsListResponse> Handle(GetStudentExamsListQuery request, CancellationToken cancellationToken)
        {
            var response = new GetStudentExamsListResponse();
            var query = _examRepository.Get();

            #region Build Query
            query = query.Where(x => x.IsDeleted == false);

            #endregion

            #region Pagination & Sorting
            request.Pagination = await _examRepository.SetPaginationCount(query, request.Pagination);
            query = _examRepository.SetPagination(query, request.Pagination);
            query = _examRepository.SetSortOrder(query, request.Sorting);
            #endregion

            var result = await query
                .ProjectTo<GetStudentExamsListRespnseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            response.Data = result;
            response.Pagination = request.Pagination;
            return response;
        }
    }
}
