using Microsoft.AspNetCore.Identity.UI.Services;
using Quartz;

namespace EMS.Persistence.BackgroundJobs
{
    public sealed class SendEmailJob : IJob
    {
        private readonly IEmailSender _emailSender;

        public SendEmailJob(IEmailSender emailSender) => _emailSender = emailSender;

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap jobDate = context.JobDetail.JobDataMap;
            string email = jobDate.GetString("Email")!;
            string code = jobDate.GetString("Code")!;
            await _emailSender.SendEmailAsync(
                email,
                "Confirm Email.",
                $"To Confirm Email Code: <h3>{code}</h3>"
            );
        }
    }
}
