using FirebirdSql.Data.FirebirdClient;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Repository.EntityFramework.Base;
using System.Data.Common;

namespace SD.FileSystem.Repository.Base
{
    /// <summary>
    /// EF上下文
    /// </summary>
    internal class DbSession : DbSessionBase
    {
        /// <summary>
        /// Firebird数据库连接
        /// </summary>
        public static DbConnection DbConnection
        {
            get
            {
                GlobalSetting.InitDataDirectory();
                return new FbConnection(GlobalSetting.WriteConnectionString);
            }
        }

        /// <summary>
        /// 基础构造器
        /// </summary>
        public DbSession()
            : base(DbConnection, true)
        {

        }
    }
}
