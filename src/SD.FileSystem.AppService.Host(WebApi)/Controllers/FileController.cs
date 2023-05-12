using Microsoft.AspNetCore.Mvc;
using SD.FileSystem.IAppService.DTOs.Inputs;
using SD.FileSystem.IAppService.DTOs.Outputs;
using SD.FileSystem.IAppService.Interfaces;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.AspNetCore.Attributes;
using System;
using System.Collections.Generic;

namespace SD.FileSystem.AppService.Host.Controllers
{
    /// <summary>
    /// 文件管理WebApi接口
    /// </summary>
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class FileController : ControllerBase
    {
        #region # 字段及构造器

        /// <summary>
        /// 文件管理服务契约接口
        /// </summary>
        private readonly IFileContract _fileContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public FileController(IFileContract fileContract)
        {
            this._fileContract = fileContract;
        }

        #endregion


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
        [HttpPost]
        [WrapPostParameters]
        public void UpdateFile(Guid fileId, string fileName, string extensionName, long size, string hashValue, string relativePath, string absolutePath, string hostName, string url, DateTime uploadedDate, string use, string description)
        {
            this._fileContract.UpdateFile(fileId, fileName, extensionName, size, hashValue, relativePath, absolutePath, hostName, url, uploadedDate, use, description);
        }
        #endregion

        #region # 批量修改文件 —— void UpdateFiles({Guid, FileParam} fileParams)
        /// <summary>
        /// 批量修改文件
        /// </summary>
        /// <param name="fileParams">文件参数模型字典</param>
        [HttpPost]
        [WrapPostParameters]
        public void UpdateFiles(IDictionary<Guid, FileParam> fileParams)
        {
            this._fileContract.UpdateFiles(fileParams);
        }
        #endregion

        #region # 删除文件 —— void RemoveFile(Guid fileId)
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件Id</param>
        [HttpPost]
        [WrapPostParameters]
        public void RemoveFile(Guid fileId)
        {
            this._fileContract.RemoveFile(fileId);
        }
        #endregion

        #region # 批量删除文件 —— void RemoveFiles(IEnumerable<Guid> fileIds)
        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件Id集</param>
        [HttpPost]
        [WrapPostParameters]
        public void RemoveFiles(IEnumerable<Guid> fileIds)
        {
            this._fileContract.RemoveFiles(fileIds);
        }
        #endregion


        //查询部分

        #region # 获取文件 —— FileInfo GetFile(Guid fileId)
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="fileId">文件Id</param>
        /// <returns>文件</returns>
        [HttpGet]
        public FileInfo GetFile(Guid fileId)
        {
            return this._fileContract.GetFile(fileId);
        }
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
        [HttpGet]
        public PageModel<FileInfo> GetFilesByPage(string keywords, string extensionName, string hashValue, DateTime? uploadedDate, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize)
        {
            return this._fileContract.GetFilesByPage(keywords, extensionName, hashValue, uploadedDate, startTime, endTime, pageIndex, pageSize);
        }
        #endregion
    }
}
