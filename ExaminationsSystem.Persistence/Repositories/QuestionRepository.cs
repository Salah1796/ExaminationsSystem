using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Persistence.Repositories.Base;
using System;

namespace ExaminationsSystem.Persistence.Repositories
{
    public class QuestionRepository : BaseRepository<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(ExaminationsSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
