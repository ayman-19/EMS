namespace EMS.Api.Abstraction
{
    public interface IEndpoint
    {
        void RegisterEndpoints(IEndpointRouteBuilder endpoints);
    }
}
