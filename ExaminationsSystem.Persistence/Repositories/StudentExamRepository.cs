using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Persistence.Repositories.Base;
using System;

namespace ExaminationsSystem.Persistence.Repositories
{
    public class StudentExamRepository : BaseRepository<StudentExam, Guid>, IStudentExamRepository
    {
        public StudentExamRepository(ExaminationsSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
