using EMS.Domain.Abstraction;
using EMS.Persistence.BackgroundJobs;
using Quartz;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace EMS.Persistence.Repositories
{
    public sealed class Jobs : IJobs
    {
        private readonly ISchedulerFactory _schedulerFactory;

        public Jobs(ISchedulerFactory schedulerFactory) => _schedulerFactory = schedulerFactory;

        public async Task DeleteUserByJobAsync(string email)
        {
            var jobKey = new JobKey(nameof(DeleteUserJob));
            var scheduler = await _schedulerFactory.GetScheduler();
            if (!await scheduler.CheckExists(jobKey))
            {
                var jobData = new JobDataMap { { "Email", email } };

                var jobDetail = JobBuilder
                    .Create<DeleteUserJob>()
                    .WithIdentity(jobKey)
                    .UsingJobData(jobData)
                    .Build();

                var trigger = TriggerBuilder
                    .Create()
                    .ForJob(jobKey)
                    .StartAt(DateTimeOffset.UtcNow.AddMinutes(10))
                    .WithSimpleSchedule()
                    .Build();

                await scheduler.ScheduleJob(jobDetail, trigger);
            }
        }

        public async Task SendEmailByJobAsync(string email, string code)
        {
            var jobKey = new JobKey(nameof(SendEmailJob));
            var scheduler = await _schedulerFactory.GetScheduler();
            if (!await scheduler.CheckExists(jobKey))
            {
                var jobData = new JobDataMap { { "Email", email }, { "Code", code } };

                var jobDetail = JobBuilder
                    .Create<SendEmailJob>()
                    .WithIdentity(jobKey)
                    .UsingJobData(jobData)
                    .Build();

                var trigger = TriggerBuilder
                    .Create()
                    .ForJob(jobKey)
                    .StartNow()
                    .WithSimpleSchedule()
                    .Build();

                await scheduler.ScheduleJob(jobDetail, trigger);
            }
        }
    }
}
