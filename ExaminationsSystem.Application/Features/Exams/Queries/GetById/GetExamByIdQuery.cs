using ExaminationsSystem.Application.Features.Base;
using MediatR;
using System;

namespace ExaminationsSystem.Application.Features.Exams.Queries.GetById
{
    public class GetExamByIdQuery : IRequest<GetExamByIdResponse>, IGetByIdQuery
    {
        public Guid Id { get; set; }
    }
}
