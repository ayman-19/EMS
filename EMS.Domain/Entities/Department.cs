using EMS.Domain.Base;

namespace EMS.Domain.Entities
{
    public sealed record Department : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static Department Create(string Name, string Description) => new(Name, Description);

        public void UpdateDepartment(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public ICollection<Employee> Employees { get; set; }

        private Department(string name, string description)
        {
            Name = name;
            Description = description;
            Employees = new List<Employee>();
        }
    }
}
