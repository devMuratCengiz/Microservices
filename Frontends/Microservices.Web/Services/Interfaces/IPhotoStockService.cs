using Microservices.Web.Models.PhotoStock;

namespace Microservices.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoStockViewModel> UploadPhoto(IFormFile photo);
        Task<bool> DeletePhoto(string photoUrl);
    }
}
