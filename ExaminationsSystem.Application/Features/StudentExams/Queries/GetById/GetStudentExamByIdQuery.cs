using ExaminationsSystem.Application.Features.Base;
using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.StudentExams.Queries.GetById
{
    public class GetStudentExamByIdQuery : IRequest<GetStudentExamByIdResponse>, IGetByIdQuery
    {
        public Guid Id { get; set; }
    }
}
