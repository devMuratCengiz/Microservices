using IdentityModel.Client;
using Microservice.Shared.Dtos;
using Microservices.Web.Models;

namespace Microservices.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
