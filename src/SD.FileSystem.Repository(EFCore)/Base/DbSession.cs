using Microsoft.EntityFrameworkCore;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFrameworkCore.Base;

namespace SD.FileSystem.Repository.Base
{
    /// <summary>
    /// EF Core上下文
    /// </summary>
    internal class DbSession : DbSessionBase
    {
        /// <summary>
        /// 配置
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //※目录调整
            GlobalSetting.InitDataDirectory();

            optionsBuilder.UseFirebird(NetCoreSetting.WriteConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
