using SD.FileSystem.Domain.Entities;
using SD.FileSystem.IAppService.DTOs.Outputs;
using SD.Toolkits.Mapper;

namespace SD.FileSystem.AppService.Maps
{
    /// <summary>
    /// 文件映射
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

        #region # 文件映射 —— static UploadResponse ToUploadResponse(this File file)
        /// <summary>
        /// 文件映射
        /// </summary>
        public static UploadResponse ToUploadResponse(this File file)
        {
            UploadResponse response = file.Map<File, UploadResponse>();
            response.FileId = file.Id;
            response.FileName = file.Name;

            return response;
        }
        #endregion
    }
}
