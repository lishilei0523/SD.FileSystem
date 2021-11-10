using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SD.FileSystem.Domain.Entities;

namespace SD.FileSystem.Repository.EntityConfigurations
{
    /// <summary>
    /// 文件实体映射配置
    /// </summary>
    public class FileConfig : IEntityTypeConfiguration<File>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<File> builder)
        {
            //配置属性
            builder.HasKey(file => file.Id).IsClustered(false);
            builder.Property(file => file.Keywords).IsRequired().HasMaxLength(256);
            builder.Property(file => file.Name).IsRequired().HasMaxLength(64);
            builder.Property(file => file.ExtensionName).IsRequired().HasMaxLength(16);
            builder.Property(file => file.HashValue).IsRequired().HasMaxLength(32);

            //配置索引
            builder.HasIndex(file => file.AddedTime).HasDatabaseName("IX_AddedTime").IsUnique(false).IsClustered(true);
            builder.HasIndex(file => file.HashValue).HasDatabaseName("IX_HashValue").IsUnique(false);
        }
    }
}
