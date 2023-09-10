#region Using ...
using ExaminationsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
#endregion
namespace ExaminationsSystem.Domain.Mappings
{
    public class StudentMap : IEntityTypeConfiguration<Student>
    {
        #region Methods
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            #region Configure Fields
            builder.Property(prop => prop.UserName).IsRequired();
            builder.Property(prop => prop.Password).IsRequired();
            builder.Property(prop => prop.Email).HasMaxLength(320);
            builder.Property(prop => prop.Name).IsRequired().HasMaxLength(200);
            builder.HasIndex(p => p.UserName).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();
            #endregion
        }
        #endregion
    }
}
