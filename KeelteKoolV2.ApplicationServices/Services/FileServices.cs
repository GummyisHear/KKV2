using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using KeelteKoolV2.Data;
using Microsoft.Extensions.Hosting;

namespace KeelteKoolV2.ApplicationServices.Services;

public class FileServices : IFileServices
{
    private readonly KeelteKoolV2Context _context;
    private readonly IHostEnvironment _hostEnvironment;

    public FileServices(KeelteKoolV2Context context, IHostEnvironment env)
    {
        _context = context;
        _hostEnvironment = env;
    }

    public Task<FileToDatabase> RemoeFilesFromDatabase(params FileToDatabaseDTO[] dtos)
    {
        throw new NotImplementedException();
    }

    public Task<FileToDatabase> RemoveFileFromDatabase(FileToDatabaseDTO dto)
    {
        throw new NotImplementedException();
    }

    public void UploadFiles(LecturerDTO dto, Lecturer domain)
    {
        if (dto.Files == null || dto.Files.Count == 0)
            return;
        
        foreach (var file in dto.Files)
        {
            using (var target = new MemoryStream())
            {
                var files = new FileToDatabase
                {
                    ImageID = Guid.NewGuid(),
                    ImageTitle = file.FileName,
                    LecturerId = domain.Id
                };
                file.CopyTo(target);
                files.ImageData = target.ToArray();

                _context.Files.Add(files);
            }
        }
    }
}
