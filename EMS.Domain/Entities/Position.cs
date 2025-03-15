using EMS.Domain.Base;

namespace EMS.Domain.Entities
{
    public sealed record Position : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public static Position Create(string name, string description) =>
            new Position(name, description);

        public void UpdatePosition(string name, string description)
        {
            Name = name;
            Description = description;
        }

        private Position(string name, string description)
        {
            Name = name;
            Description = description;
            Employees = new List<Employee>();
        }
    }
}
