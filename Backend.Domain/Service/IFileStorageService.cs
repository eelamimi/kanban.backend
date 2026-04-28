namespace Backend.Domain.Service;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(byte[] content, string fileName, string contentType, CancellationToken ct = default);

    Task<byte[]> GetFileAsync(string filePath, CancellationToken ct = default);

    Task DeleteFileAsync(string filePath, CancellationToken ct = default);

    string GetStorageRootPath();
}