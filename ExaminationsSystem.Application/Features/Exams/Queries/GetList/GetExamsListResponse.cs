using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Models.ViewModels.Exams;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Features.Exams.Queries.GetList
{
    public class GetExamsListResponse : BasePaginatedResponse<List<GetExamsListRespnseViewModel>>
    {
    }
}