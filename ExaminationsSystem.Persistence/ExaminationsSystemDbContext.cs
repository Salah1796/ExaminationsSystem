using ExaminationsSystem.Domain.Entities;
using ExaminationsSystem.Domain.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ExaminationsSystem.Persistence
{
    public class ExaminationsSystemDbContext : DbContext
    {
        public ExaminationsSystemDbContext(DbContextOptions<ExaminationsSystemDbContext> options)
           : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentSelectedOption> StudentSelectedOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentMap());
        }

    }
}
