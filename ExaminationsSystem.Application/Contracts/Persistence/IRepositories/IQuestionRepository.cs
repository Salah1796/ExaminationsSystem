using ExaminationsSystem.Application.Contracts.Persistence.IRepositories.Base;
using ExaminationsSystem.Domain.Entities;
using System;

namespace ExaminationsSystem.Application.Contracts.Persistence.IRepositories
{
    public interface IQuestionRepository : IBaseRepositoryAsync<Question, Guid>
    {
    }
}
