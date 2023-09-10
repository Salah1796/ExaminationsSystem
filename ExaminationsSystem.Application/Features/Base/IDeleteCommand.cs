using System;

namespace ExaminationsSystem.Application.Features.Base
{
    public interface IDeleteCommand
    {
        Guid Id { get; set; }
    }
}