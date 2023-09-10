using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Persistence.Repositories.Base;
using System;

namespace ExaminationsSystem.Persistence.Repositories
{
    public class ExamRepository : BaseRepository<Exam, Guid>, IExamRepository
    {
        public ExamRepository(ExaminationsSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
