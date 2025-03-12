using EMS.Domain.Base;

namespace EMS.Domain.Entities
{
    public sealed record Position : Entity<Guid>
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public Position() => Employees = new List<Employee>();
    }
}
