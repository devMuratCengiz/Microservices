namespace Microservices.Web.Services.Interfaces
{
    public interface IClientCredentialsTokenService
    {
        Task<String> GetToken();
    }
}
