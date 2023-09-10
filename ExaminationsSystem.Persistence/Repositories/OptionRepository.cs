using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Persistence.Repositories.Base;
using System;

namespace ExaminationsSystem.Persistence.Repositories
{
    public class OptionRepository : BaseRepository<QuestionOption, Guid>, IOptionRepository
    {
        public OptionRepository(ExaminationsSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
