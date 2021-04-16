using SD.FileSystem.AppService.Models;
using SD.FileSystem.Domain.Entities;
using SD.Toolkits.Mapper;

namespace SD.FileSystem.AppService.Maps
{
    /// <summary>
    /// 文件映射工具类
    /// </summary>
    public static class FileMap
    {
        #region # 文件映射 —— static FileInfo ToDTO(this File file)
        /// <summary>
        /// 文件映射
        /// </summary>
        public static FileInfo ToDTO(this File file)
        {
            FileInfo fileInfo = file.Map<File, FileInfo>();
            return fileInfo;
        }
        #endregion
    }
}
