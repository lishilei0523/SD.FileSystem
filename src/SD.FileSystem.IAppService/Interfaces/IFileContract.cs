using SD.FileSystem.IAppService.DTOs.Inputs;
using SD.FileSystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.AppServiceBase;
using SD.Infrastructure.DTOBase;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SD.FileSystem.IAppService.Interfaces
{
    /// <summary>
    /// 文件管理服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.FileSystem.IAppService.Interfaces")]
    public interface IFileContract : IApplicationService
    {
        //命令部分

        #region # 修改文件 —— void UpdateFile(Guid fileId, string fileName...
        /// <summary>
        /// 修改文件
        /// </summary>
        /// <param name="fileId">文件Id</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="size">文件大小</param>
        /// <param name="hashValue">哈希值</param>
        /// <param name="relativePath">相对路径</param>
        /// <param name="absolutePath">绝对路径</param>
        /// <param name="hostName">主机名称</param>
        /// <param name="use">用途</param>
        /// <param name="url">链接地址</param>
        /// <param name="uploadedDate">上传日期</param>
        /// <param name="description">描述</param>
        [OperationContract]
        void UpdateFile(Guid fileId, string fileName, string extensionName, long size, string hashValue, string relativePath, string absolutePath, string hostName, string url, DateTime uploadedDate, string use, string description);
        #endregion

        #region # 批量修改文件 —— void UpdateFiles({Guid, FileParam} fileParams)
        /// <summary>
        /// 批量修改文件
        /// </summary>
        /// <param name="fileParams">文件参数模型字典</param>
        [OperationContract]
        void UpdateFiles(IDictionary<Guid, FileParam> fileParams);
        #endregion

        #region # 删除文件 —— void RemoveFile(Guid fileId)
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件Id</param>
        [OperationContract]
        void RemoveFile(Guid fileId);
        #endregion

        #region # 批量删除文件 —— void RemoveFiles(IEnumerable<Guid> fileIds)
        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件Id集</param>
        [OperationContract]
        void RemoveFiles(IEnumerable<Guid> fileIds);
        #endregion


        //查询部分

        #region # 获取文件 —— FileInfo GetFile(Guid fileId)
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="fileId">文件Id</param>
        /// <returns>文件</returns>
        [OperationContract]
        FileInfo GetFile(Guid fileId);
        #endregion

        #region # 分页获取文件列表 —— PageModel<FileInfo> GetFilesByPage(string keywords...
        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="hashValue">哈希值</param>
        /// <param name="uploadedDate">上传日期</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <returns>文件列表</returns>
        [OperationContract]
        PageModel<FileInfo> GetFilesByPage(string keywords, string extensionName, string hashValue, DateTime? uploadedDate, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize);
        #endregion
    }
}
