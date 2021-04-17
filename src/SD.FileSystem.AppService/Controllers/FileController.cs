using SD.FileSystem.AppService.Models;
using SD.FileSystem.Domain.Entities;
using SD.FileSystem.Domain.IRepositories;
using SD.Infrastructure.DTOBase;
using SD.Toolkits.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SD.FileSystem.AppService.Controllers
{
    /// <summary>
    /// 文件管理控制器
    /// </summary>
    public class FileController : ApiController
    {
        #region # 字段及构造器

        /// <summary>
        /// 文件仓储接口
        /// </summary>
        private readonly IFileRepository _fileRepository;

        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWorkFile _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public FileController(IFileRepository fileRepository, IUnitOfWorkFile unitOfWork)
        {
            this._fileRepository = fileRepository;
            this._unitOfWork = unitOfWork;
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
            File file = this._unitOfWork.Resolve<File>(fileId);
            file.UpdateInfo(fileName, extensionName, size, hashValue, relativePath, absolutePath, hostName, url, uploadedDate, use, description);

            this._unitOfWork.RegisterSave(file);
            this._unitOfWork.Commit();
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
            #region # 验证

            fileParams = fileParams ?? new Dictionary<Guid, FileParam>();
            if (!fileParams.Any())
            {
                return;
            }

            #endregion

            ICollection<File> files = this._unitOfWork.ResolveRange<File>(fileParams.Keys);
            foreach (File file in files)
            {
                FileParam fileParam = fileParams[file.Id];
                file.UpdateInfo(fileParam.FileName, fileParam.ExtensionName, fileParam.Size, fileParam.HashValue, fileParam.RelativePath, fileParam.AbsolutePath, fileParam.HostName, fileParam.Url, fileParam.UploadedDate, fileParam.Use, fileParam.Description);
            }

            this._unitOfWork.RegisterSaveRange(files);
            this._unitOfWork.Commit();
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
            File file = this._unitOfWork.Resolve<File>(fileId);

            //判断哈希
            if (this._fileRepository.CountByHash(file.HashValue) == 1)
            {
                //删除物理文件
                System.IO.File.Delete(file.AbsolutePath);
            }

            this._unitOfWork.RegisterPhysicsRemove(file);
            this._unitOfWork.Commit();
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
            #region # 验证

            fileIds = fileIds?.Distinct().ToArray() ?? new Guid[0];
            if (!fileIds.Any())
            {
                return;
            }

            #endregion

            ICollection<File> files = this._unitOfWork.ResolveRange<File>(fileIds);
            foreach (File file in files)
            {
                //判断哈希
                if (this._fileRepository.CountByHash(file.HashValue) == 1)
                {
                    //删除物理文件
                    System.IO.File.Delete(file.AbsolutePath);
                }

                this._unitOfWork.RegisterPhysicsRemove(file);
            }

            this._unitOfWork.Commit();
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
            File file = this._fileRepository.Single(fileId);
            FileInfo fileInfo = file.ToDTO();

            return fileInfo;
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
            ICollection<File> files = this._fileRepository.FindByPage(keywords, extensionName, hashValue, uploadedDate, startTime, endTime, pageIndex, pageSize, out int rowCount, out int pageCount);
            IEnumerable<FileInfo> fileInfos = files.Select(x => x.ToDTO());

            return new PageModel<FileInfo>(fileInfos, pageIndex, pageSize, pageCount, rowCount);
        }
        #endregion
    }
}