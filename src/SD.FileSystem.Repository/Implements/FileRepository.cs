using SD.FileSystem.Domain.Entities;
using SD.FileSystem.Domain.IRepositories;
using SD.Infrastructure.Repository.EntityFramework;

namespace SD.FileSystem.Repository.Implements
{
    /// <summary>
    /// 文件仓储实现
    /// </summary>
    public class FileRepository : EFAggRootRepositoryProvider<File>, IFileRepository
    {

    }
}
