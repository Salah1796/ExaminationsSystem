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

namespace ExaminationsSystem.Application.Features.Exams.Queries.GetList
{
    public class GetExamsListQueryHandler : IRequestHandler<GetExamListQuery, GetExamsListResponse>
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        public GetExamsListQueryHandler(IMapper mapper, IExamRepository examRepository)
        {
            _mapper = mapper;
            _examRepository = examRepository;
        }

        public async Task<GetExamsListResponse> Handle(GetExamListQuery request, CancellationToken cancellationToken)
        {
            var response = new GetExamsListResponse();
            var query = _examRepository.Get();

            #region Build Query
            query = query.Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }
            #endregion

            #region Pagination & Sorting
            request.Pagination = await _examRepository.SetPaginationCount(query, request.Pagination);
            query = _examRepository.SetPagination(query, request.Pagination);
            query = _examRepository.SetSortOrder(query, request.Sorting);
            #endregion

            var result = await query
                .ProjectTo<GetExamsListRespnseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            response.Data = result;
            response.Pagination = request.Pagination;
            return response;
        }
    }
}
