using SD.FileSystem.Domain.Entities;
using SD.Toolkits.EntityFramework.Extensions;
using System.Data.Entity.ModelConfiguration;

namespace SD.FileSystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 文件实体映射配置
    /// </summary>
    public class FileConfig : EntityTypeConfiguration<File>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public FileConfig()
        {
            //配置属性
            this.HasKey(file => file.Id, index => index.IsClustered(false));
            this.Property(file => file.Keywords).IsRequired();
            this.Property(file => file.Number).IsRequired().HasMaxLength(64);
            this.Property(file => file.Name).IsRequired().HasMaxLength(256);
            this.Property(file => file.ExtensionName).IsRequired().HasMaxLength(16);
            this.Property(file => file.HashValue).IsRequired().HasMaxLength(32);

            //配置索引
            this.HasIndex("IX_AddedTime", IndexType.Clustered, table => table.Property(file => file.AddedTime));
            this.HasIndex("IX_HashValue", IndexType.Nonclustered, table => table.Property(file => file.HashValue));
        }
    }
}
