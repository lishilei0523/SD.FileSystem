using SD.FileSystem.AppService.Models;
using SD.FileSystem.Domain.IRepositories;
using SD.Toolkits.AspNet;
using SD.Toolkits.AspNet.Configurations;
using SD.Toolkits.WebApi.Extensions;
using SD.Toolkits.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using File = SD.FileSystem.Domain.Entities.File;
using FileInfo = SD.FileSystem.AppService.Models.FileInfo;

namespace SD.FileSystem.AppService.Controllers
{
    /// <summary>
    /// 文件上传/下载控制器
    /// </summary>
    public class LoadController : ApiController
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
        public LoadController(IFileRepository fileRepository, IUnitOfWorkFile unitOfWork)
        {
            this._fileRepository = fileRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion


        //Public

        #region # 上传文件 —— FileInfo UploadFile(string use, string description...
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="use">用途</param>
        /// <param name="description">描述</param>
        /// <param name="formFile">Http请求文件</param>
        /// <returns>文件</returns>
        [HttpPost]
        [FileParameters]
        public FileInfo UploadFile(string use, string description, IFormFile formFile)
        {
            #region # 验证

            if (!base.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile), "要上传的文件不可为空！");
            }

            #endregion

            string fileName = formFile.FileName;
            string extensionName = Path.GetExtension(formFile.FileName);
            long size = formFile.ContentLength;
            DateTime uploadedDate = DateTime.Today;
            File file = new File(fileName, extensionName, size, use, uploadedDate, description);

            string timestamp = uploadedDate.ToString("yyyyMMdd");
            string fileServerPath = AspNetSection.Setting.FileServer.Path;
            string storageDirectory = $"{fileServerPath}\\{timestamp}";
            Directory.CreateDirectory(storageDirectory);

            string relativePath = $"{timestamp}/{file.Number}";
            string absolutePath = $"{Path.GetFullPath(storageDirectory)}\\{file.Number}";
            string hostName = this.GetHostName();
            string fileUrl = $"{hostName}/{relativePath}";

            System.IO.File.WriteAllBytes(absolutePath, formFile.Datas);
            file.Save(relativePath, absolutePath, hostName, fileUrl);

            this._unitOfWork.RegisterAdd(file);
            this._unitOfWork.Commit();

            FileInfo fileInfo = file.ToDTO();

            return fileInfo;
        }
        #endregion

        #region # 批量上传文件 —— IEnumerable<FileInfo> UploadFiles(string use...
        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="use">用途</param>
        /// <param name="description">描述</param>
        /// <param name="formFiles">Http请求文件集</param>
        /// <returns>文件列表</returns>
        [HttpPost]
        [FileParameters]
        public IEnumerable<FileInfo> UploadFiles(string use, string description, IFormFileCollection formFiles)
        {
            #region # 验证

            if (!base.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            if (formFiles == null || !formFiles.Any())
            {
                throw new ArgumentNullException(nameof(formFiles), "要上传的文件集不可为空！");
            }

            #endregion

            DateTime uploadedDate = DateTime.Today;
            string timestamp = uploadedDate.ToString("yyyyMMdd");
            string fileServerPath = AspNetSection.Setting.FileServer.Path;
            string storageDirectory = $"{fileServerPath}\\{timestamp}";
            Directory.CreateDirectory(storageDirectory);
            string hostName = this.GetHostName();

            IList<File> files = new List<File>();
            foreach (IFormFile formFile in formFiles)
            {
                string fileName = formFile.FileName;
                string extensionName = Path.GetExtension(formFile.FileName);
                long size = formFile.ContentLength;
                File file = new File(fileName, extensionName, size, use, uploadedDate, description);

                string relativePath = $"{timestamp}/{file.Number}";
                string absolutePath = $"{Path.GetFullPath(storageDirectory)}\\{file.Number}";
                string fileUrl = $"{hostName}/{relativePath}";

                System.IO.File.WriteAllBytes(absolutePath, formFile.Datas);
                file.Save(relativePath, absolutePath, hostName, fileUrl);

                files.Add(file);
            }

            this._unitOfWork.RegisterAddRange(files);
            this._unitOfWork.Commit();

            IEnumerable<FileInfo> fileInfos = files.Select(x => x.ToDTO());

            return fileInfos;
        }
        #endregion


        //Private

        #region # 获取主机名 —— string GetHostName()
        /// <summary>
        /// 获取主机名
        /// </summary>
        /// <returns>主机名</returns>
        private string GetHostName()
        {
            ICollection<string> hostUrls = new HashSet<string>();
            foreach (HostElement host in AspNetSection.Setting.HostElement)
            {
                hostUrls.Add(host.Url);
            }

            string generalHostUrl = hostUrls.OrderBy(x => x).First();
            return generalHostUrl;
        }
        #endregion
    }
}
