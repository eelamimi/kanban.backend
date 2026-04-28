namespace Backend.Domain.Service;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(byte[] content, string fileName, string contentType, bool isAvatar = false, CancellationToken token = default);

    Task<byte[]> GetFileAsync(string filePath, CancellationToken token = default);

    Task DeleteFileAsync(string filePath, CancellationToken token = default);

    string GetStorageRootPath();
}