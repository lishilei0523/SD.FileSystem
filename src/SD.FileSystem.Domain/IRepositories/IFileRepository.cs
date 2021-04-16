using SD.FileSystem.Domain.Entities;
using SD.Infrastructure.RepositoryBase;

namespace SD.FileSystem.Domain.IRepositories
{
    /// <summary>
    /// 文件仓储接口
    /// </summary>
    public interface IFileRepository : IAggRootRepository<File>
    {

    }
}
