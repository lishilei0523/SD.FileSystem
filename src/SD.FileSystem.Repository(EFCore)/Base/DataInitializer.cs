using SD.IdentitySystem;
using SD.Infrastructure.Repository.EntityFrameworkCore;
using SD.Infrastructure.RepositoryBase;

namespace SD.FileSystem.Repository.Base
{
    /// <summary>
    /// 数据初始化器实现
    /// </summary>
    public class DataInitializer : IDataInitializer
    {
        /// <summary>
        /// 初始化基础数据
        /// </summary>
        public void Initialize()
        {
            EFUnitOfWorkProvider.GetLoginInfo += MembershipMediator.GetLoginInfo;
        }
    }
}
