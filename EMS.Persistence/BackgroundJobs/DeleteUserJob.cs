using EMS.Domain.Abstraction;
using Quartz;

namespace EMS.Persistence.BackgroundJobs
{
    public sealed class DeleteUserJob : IJob
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserJob(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap jobDate = context.JobDetail.JobDataMap;
            string email = jobDate.GetString("Email")!;
            await _userRepository.DeleteUserNotConfirmByEmailAsync(email);
        }
    }
}
