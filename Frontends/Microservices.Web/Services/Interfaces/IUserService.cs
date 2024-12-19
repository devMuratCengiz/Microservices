using Microservices.Web.Models;

namespace Microservices.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
