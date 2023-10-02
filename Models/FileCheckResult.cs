using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Domain;
using VartanMVCv2.ViewModels;
using System;

namespace VartanMVCv2.Models
{
    public class FileCheckResult
    {
        public string? Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        private ILogger<FileCheckResult>? _logger { get; set; }

        public const long maximumFileSize = 5 * 1024 * 1024;
        public readonly static string[] defaultExtensions = { ".jpg", ".jpeg", ".png" };

        private FileCheckResult(ILogger<FileCheckResult> logger, string inputMessage, bool success)
        {
           _logger = logger;

           Message = inputMessage;
           Success = success;
        }

        private FileCheckResult(string inputMessage, bool success)
        {
            Message = inputMessage;
            Success = success;
        }

        public FileCheckResult()
        { }


        public static FileCheckResult CheckUploadFiles(List<IFormFile> files, string[] extensions, long maxFileSize = FileCheckResult.maximumFileSize)
        {
            FileCheckResult fileResult = new FileCheckResult();

            if (files == null)
            {
                fileResult.Message ="Вы не выбрали ни одного файла";
                fileResult.Success = false;
                return fileResult;
            }

            foreach (var file in files)
            {
                if (file.Length > maxFileSize)
                {
                    fileResult.Message = $"Файл '{file.FileName}' превышает максимальный размер {maxFileSize} байт";
                    fileResult.Success = false;
                    return fileResult;
                }
                if (!extensions.Contains(Path.GetExtension(file.FileName).ToLower()))
                {
                    fileResult.Message = $"Недопустимый тип файла '{file.FileName}'";
                    fileResult.Success = false;
                    return fileResult;
                }
            }
            fileResult.Message = "Файлы успешно добавлены";
            fileResult.Success = true;
            return fileResult;
        }

        public static FileCheckResult FileRemove(string directoryName, string catalogName)
        {
            string currDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images"); // дерриктория images
            string path = Path.Combine(currDir, directoryName, catalogName);
            FileCheckResult result = new FileCheckResult();

            try
            {
                // Проверяем, существует ли указанный каталог
                if (Directory.Exists(path))
                {
                    // Удаляем каталог и все его содержимое
                    Directory.Delete(path, true);
                    result.Message = "Каталог успешно удален.";
                    result.Success = true;
                    return result;
                }
                else
                {
                    result.Message = "Каталог не существует.";
                    result.Success = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = $"Ошибка при удалении каталога: {ex.Message}";
                result.Success = false;
                return result;
            }
        }

        public static async Task<FileCheckResult> FileUpload(List<IFormFile> files, string uploadPath, Action<string> accompanyingAction) 
        {
            FileCheckResult result = new FileCheckResult();

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploadPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    accompanyingAction.Invoke(filePath);
                }
            }
            result.Message = "Файлы загружены успешно";
            result.Success = true;
            return result;
        }
    }
   
}
