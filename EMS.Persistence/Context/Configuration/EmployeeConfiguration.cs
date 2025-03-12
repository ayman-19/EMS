using EMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.Persistence.Context.Configuration
{
    public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee", "ems");
            builder.HasKey(x => x.Id);
            builder.HasOne(u => u.User).WithOne(e => e.Employee).HasForeignKey<Employee>(u => u.Id);
        }
    }
}
