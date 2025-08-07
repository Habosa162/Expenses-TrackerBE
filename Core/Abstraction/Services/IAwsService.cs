using Microsoft.AspNetCore.Http;

namespace ExpensesTracker.Application.Interfaces
{
    public interface IAwsService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string fileKey);
    }
}
