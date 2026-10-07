namespace WebAppInHollandMTF.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
