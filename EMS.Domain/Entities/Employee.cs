using EMS.Domain.Base;

namespace EMS.Domain.Entities
{
    public sealed record Employee : Entity<Guid>
    {
        public double Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public User? User { get; set; }
        public Position? Position { get; set; }
        public Department? Department { get; set; }
    }
}
