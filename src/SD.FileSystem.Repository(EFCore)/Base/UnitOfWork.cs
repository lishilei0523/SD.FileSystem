using SD.FileSystem.Domain.IRepositories;
using SD.Infrastructure.Repository.EntityFrameworkCore;

namespace SD.FileSystem.Repository.Base
{
    /// <summary>
    /// 工作单元 - 文件管理
    /// </summary>
    public sealed class UnitOfWork : EFUnitOfWorkProvider, IUnitOfWorkFile
    {

    }
}
