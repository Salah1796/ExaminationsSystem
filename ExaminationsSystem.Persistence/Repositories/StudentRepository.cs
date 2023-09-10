using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Persistence.Repositories.Base;
using System;

namespace ExaminationsSystem.Persistence.Repositories
{
    public class StudentRepository : BaseRepository<Student, Guid>, IStudentRepository
    {
        public StudentRepository(ExaminationsSystemDbContext dbContext) : base(dbContext)
        {
        }
    }
}
