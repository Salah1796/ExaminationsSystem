using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Persistence.Repositories.Base;
using System;

namespace ExaminationsSystem.Persistence.Repositories
{
    public class StudentAnswerRepository : BaseRepository<StudentAnswer, Guid>, IStudentAnswerRepository
    {
        public StudentAnswerRepository(ExaminationsSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
