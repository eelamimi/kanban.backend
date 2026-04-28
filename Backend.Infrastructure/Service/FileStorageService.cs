namespace Backend.Infrastructure.Service;

public class FileStorageService : IFileStorageService
{
    private readonly string _storageRoot;

    public FileStorageService(IHostEnvironment hostEnvironment)
    {
        var projectRoot = hostEnvironment.ContentRootPath;
        _storageRoot = Path.Combine(projectRoot, "Storage");

        Directory.CreateDirectory(Path.Combine(_storageRoot, "attachments"));
        Directory.CreateDirectory(Path.Combine(_storageRoot, "avatars"));
    }

    public async Task<string> SaveFileAsync(byte[] content, string fileName, string contentType, bool isAvatar = false, CancellationToken token = default)
    {
        var currentDate = DateTime.UtcNow;
        var subFolder = "attachments";
        if (isAvatar) 
            subFolder = "avatars";
        var subFolders = $"{subFolder}\\{currentDate.Year}\\{currentDate.Month}\\{currentDate.Day}";
        var targetFolder = Path.Combine(_storageRoot, subFolders);
        Directory.CreateDirectory(targetFolder);

        var extension = Path.GetExtension(fileName);
        var uniqueName = $"{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(targetFolder, uniqueName);

        await File.WriteAllBytesAsync(filePath, content, token);

        return Path.Combine(subFolders, uniqueName);
    }

    public async Task<byte[]> GetFileAsync(string filePath, CancellationToken token = default)
    {
        var fullPath = Path.Combine(_storageRoot, filePath);

        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"Файл не найден: {filePath}");

        return await File.ReadAllBytesAsync(fullPath, token);
    }

    public Task DeleteFileAsync(string filePath, CancellationToken token = default)
    {
        var fullPath = Path.Combine(_storageRoot, filePath);

        if (File.Exists(fullPath))
            File.Delete(fullPath);

        return Task.CompletedTask;
    }

    public string GetStorageRootPath() => _storageRoot;
}