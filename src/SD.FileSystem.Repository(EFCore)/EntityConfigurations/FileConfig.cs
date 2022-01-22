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
            builder.Property(file => file.Keywords).IsRequired();
            builder.Property(file => file.Number).IsRequired().HasMaxLength(64);
            builder.Property(file => file.Name).IsRequired().HasMaxLength(256);
            builder.Property(file => file.ExtensionName).IsRequired().HasMaxLength(16);
            builder.Property(file => file.HashValue).IsRequired().HasMaxLength(32);
            builder.Property(file => file.UploadedDate).HasColumnType("DATE");

            //配置索引
            builder.HasIndex(file => file.AddedTime).IsUnique(false).IsClustered();
            builder.HasIndex(file => file.HashValue).IsUnique(false);

            //忽略映射
            builder.Ignore(file => file.Deleted);
            builder.Ignore(file => file.DeletedTime);
        }
    }
}
