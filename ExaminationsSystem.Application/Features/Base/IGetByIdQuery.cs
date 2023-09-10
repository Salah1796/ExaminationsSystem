using System;

namespace ExaminationsSystem.Application.Features.Base
{
    public interface IGetByIdQuery
    {
        Guid Id { get; set; }
    }
}