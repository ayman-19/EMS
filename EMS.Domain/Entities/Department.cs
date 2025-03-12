using EMS.Domain.Base;

namespace EMS.Domain.Entities
{
    public sealed record Department : Entity<Guid>
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public Department() => Employees = new List<Employee>();
    }
}
