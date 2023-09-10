using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Persistence.Repositories.Base;
using System;

namespace ExaminationsSystem.Persistence.Repositories
{
    public class ExamQuestionRepository : BaseRepository<ExamQuestion, Guid>, IExamQuestionRepository
    {
        public ExamQuestionRepository(ExaminationsSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
