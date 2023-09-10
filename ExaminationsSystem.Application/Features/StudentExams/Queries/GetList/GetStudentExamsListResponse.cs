using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Models.ViewModels.ExamQuestions;
using System.Collections.Generic;

namespace ExaminationsSystem.Application.Features.StudentExams.Queries.GetList
{
    public class GetStudentExamsListResponse : BasePaginatedResponse<List<GetStudentExamsListRespnseViewModel>>
    {
    }
}