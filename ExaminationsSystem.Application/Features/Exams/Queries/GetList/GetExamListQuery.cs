using ExaminationsSystem.Application.Common.Models;
using MediatR;

namespace ExaminationsSystem.Application.Features.Exams.Queries.GetList
{
    public class GetExamListQuery : BaseFilter, IRequest<GetExamsListResponse>
    {
        public string Name { get; set; }
    }
}
