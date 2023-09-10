using ExaminationsSystem.Application.Common.Models;
using MediatR;

namespace ExaminationsSystem.Application.Features.StudentExams.Queries.GetList
{
    public class GetStudentExamsListQuery : BaseFilter, IRequest<GetStudentExamsListResponse>
    {
    }
}
