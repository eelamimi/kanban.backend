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

    public async Task<string> SaveFileAsync(
        byte[] content,
        string fileName,
        string contentType,
        CancellationToken token = default)
    {
        // Определяем подпапку по типу файла (можно расширить логику)
        var subFolder = GetSubFolder(contentType);
        var targetFolder = Path.Combine(_storageRoot, subFolder);
        Directory.CreateDirectory(targetFolder);

        // Генерируем уникальное имя файла
        var extension = Path.GetExtension(fileName);
        var uniqueName = $"{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(targetFolder, uniqueName);

        // Сохраняем файл
        await File.WriteAllBytesAsync(filePath, content, token);

        // Возвращаем относительный путь (для хранения в БД)
        return Path.Combine(subFolder, uniqueName);
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

    private string GetSubFolder(string contentType)
    {
        if (contentType.StartsWith("image/"))
            return "images";

        if (contentType == "application/pdf")
            return "documents";

        return "attachments";
    }
}