
using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;

namespace KeelteKoolV2.Core.ServiceInterface;

public interface IFileServices
{
    public void UploadFiles(LecturerDTO dto, Lecturer domain);
    public Task<FileToDatabase> RemoveFileFromDatabase(FileToDatabaseDTO dto);
    public Task<FileToDatabase> RemoeFilesFromDatabase(params FileToDatabaseDTO[] dtos);
}
