using System;

namespace ExaminationsSystem.Application.Contracts.Identity
{
    public interface ICurrentStudentService
    {
        Guid? GetCurrentStudentId();
    }
}