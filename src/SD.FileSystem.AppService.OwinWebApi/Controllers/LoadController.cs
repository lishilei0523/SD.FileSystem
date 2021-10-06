using SD.Common;
using SD.FileSystem.AppService.Models;
using SD.FileSystem.Domain.IRepositories;
using SD.Toolkits.AspNet;
using SD.Toolkits.WebApi.Attributes;
using SD.Toolkits.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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


        //命令部分

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

            File file = this.ProcessFile(use, description, formFile);

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

            IList<File> files = new List<File>();
            foreach (IFormFile formFile in formFiles)
            {
                File file = this.ProcessFile(use, description, formFile);
                files.Add(file);
            }

            this._unitOfWork.RegisterAddRange(files);
            this._unitOfWork.Commit();

            IEnumerable<FileInfo> fileInfos = files.Select(x => x.ToDTO());

            return fileInfos;
        }
        #endregion


        //查询部分

        #region # 下载文件 —— IHttpActionResult DownloadFile(string hashValue)
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="hashValue">哈希值</param>
        /// <returns>文件数据</returns>
        [HttpGet]
        public IHttpActionResult DownloadFile(string hashValue)
        {
            File file = this._fileRepository.DefaultByHash(hashValue);
            byte[] buffer = System.IO.File.ReadAllBytes(file.AbsolutePath);

            const string contentType = "application/octet-stream";
            const string dispositionType = "attachment";
            HttpContent httpContent = new ByteArrayContent(buffer);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            httpContent.Headers.ContentDisposition = new ContentDispositionHeaderValue(dispositionType)
            {
                FileName = file.Name,
                Size = file.Size
            };
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = httpContent
            };

            return base.ResponseMessage(httpResponse);
        }
        #endregion


        //Private

        #region # 处理文件 —— File ProcessFile(string use, string description...
        /// <summary>
        /// 处理文件
        /// </summary>
        /// <param name="use">用途</param>
        /// <param name="description">描述</param>
        /// <param name="formFile">Http请求文件</param>
        /// <returns>文件</returns>
        private File ProcessFile(string use, string description, IFormFile formFile)
        {
            const string timestampFormat = "yyyyMMdd";
            string fileName = formFile.FileName;
            string extensionName = Path.GetExtension(formFile.FileName);
            long size = formFile.ContentLength;
            string hashValue = formFile.Datas.ToMD5();
            DateTime uploadedDate = DateTime.Today;
            File file = new File(fileName, extensionName, size, hashValue, uploadedDate, use, description);

            //哈希值比对
            File existedFile = this._fileRepository.DefaultByHash(hashValue);
            if (existedFile != null)
            {
                file.Save(existedFile.RelativePath, existedFile.AbsolutePath, existedFile.HostName, existedFile.Url);
            }
            else
            {
                string timestamp = uploadedDate.ToString(timestampFormat);
                string fileServerPath = AspNetSection.Setting.FileServer.Value;
                string storageDirectory = $"{fileServerPath}\\{timestamp}";
                Directory.CreateDirectory(storageDirectory);

                string relativePath = $"{timestamp}/{file.Number}";
                string absolutePath = $"{Path.GetFullPath(storageDirectory)}\\{file.Number}";
                string hostName = $"http://{this.Request.RequestUri.Host}:{this.Request.RequestUri.Port}";
                string fileUrl = $"{hostName}/{relativePath}";

                System.IO.File.WriteAllBytes(absolutePath, formFile.Datas);
                file.Save(relativePath, absolutePath, hostName, fileUrl);
            }

            return file;
        }
        #endregion
    }
}
