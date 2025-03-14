namespace EMS.Domain.Abstraction
{
    public interface IJobs
    {
        Task SendEmailByJobAsync(string email, string code);
    }
}
