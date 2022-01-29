using System;
using System.IO;
using System.Linq;
using SD.Common;
using SD.FileSystem.Domain.IRepositories;
using SD.FileSystem.IAppService.DTOs.Inputs;
using SD.FileSystem.IAppService.DTOs.Outputs;
using SD.FileSystem.IAppService.Interfaces;
using SD.Toolkits.AspNet;
using File = SD.FileSystem.Domain.Entities.File;
#if NET40_OR_GREATER
using System.ServiceModel;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
#endif

namespace SD.FileSystem.AppService.Implements
{
    /// <summary>
    /// 文件上传/下载服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class LoadContract : ILoadContract
    {
        #region # 字段及依赖注入构造器

        /// <summary>
        /// 文件仓储接口
        /// </summary>
        private readonly IFileRepository _fileRepository;

        /// <summary>
        /// 单元事务
        /// </summary>
        private readonly IUnitOfWorkFile _unitOfWork;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public LoadContract(IFileRepository fileRepository, IUnitOfWorkFile unitOfWork)
        {
            this._fileRepository = fileRepository;
            this._unitOfWork = unitOfWork;
        }

        #endregion


        //命令部分

        #region # 上传文件 —— UploadResponse UploadFile(UploadRequest request)
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="request">上传请求</param>
        /// <returns>上传响应</returns>
        public UploadResponse UploadFile(UploadRequest request)
        {
            #region # 验证

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "上传请求不可为空！");
            }
            if (request.Datas == null)
            {
                throw new InvalidOperationException("要上传的文件不可为空！");
            }

            #endregion

            File file = this.ProcessFile(request);

            this._unitOfWork.RegisterAdd(file);
            this._unitOfWork.Commit();

            UploadResponse response = new UploadResponse
            {
                FileId = file.Id,
                FileName = file.Name,
                HashValue = file.HashValue,
                Url = file.Url
            };

            return response;
        }
        #endregion


        //查询部分

        #region # 下载文件 —— DownloadResponse DownloadFile(DownloadRequest request)
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="request">下载请求</param>
        /// <returns>下载响应</returns>
        public DownloadResponse DownloadFile(DownloadRequest request)
        {
            #region # 验证

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "下载请求不可为空！");
            }

            #endregion

            File file = this._fileRepository.Single(request.FileId);
            byte[] buffer = System.IO.File.ReadAllBytes(file.AbsolutePath);
            DownloadResponse response = new DownloadResponse
            {
                FileName = file.Name,
                Size = file.Size,
                Datas = new MemoryStream(buffer)
            };

            return response;
        }
        #endregion


        //Private

        #region # 处理文件 —— File ProcessFile(UploadRequest request)
        /// <summary>
        /// 处理文件
        /// </summary>
        /// <param name="request">上传请求</param>
        /// <returns>文件</returns>
        private File ProcessFile(UploadRequest request)
        {
            const string timestampFormat = "yyyyMMdd";
            string fileName = request.FileName;
            string extensionName = Path.GetExtension(request.FileName);
            byte[] fileBuffer;
            using (MemoryStream stream = new MemoryStream())
            {
                request.Datas.CopyTo(stream);
                fileBuffer = stream.ToArray();
            }

            string hashValue = fileBuffer.ToMD5();
            DateTime uploadedDate = DateTime.Today;
            File file = new File(fileName, extensionName, fileBuffer.Length, hashValue, uploadedDate, request.Use, request.Description);

            //哈希值比对
            File existedFile = this._fileRepository.DefaultByHash(hashValue);
            if (existedFile != null)
            {
                file.Save(existedFile.RelativePath, existedFile.AbsolutePath, existedFile.HostName, existedFile.Url);
            }
            else
            {
                string timestamp = uploadedDate.ToString(timestampFormat);
                string fileServerPath = Path.IsPathRooted(AspNetSetting.FileServerPath)
                    ? AspNetSetting.FileServerPath
                    : Path.Combine(AppContext.BaseDirectory, AspNetSetting.FileServerPath);
                string storageDirectory = $"{fileServerPath}\\{timestamp}";
                Directory.CreateDirectory(storageDirectory);

                string host = OperationContext.Current.RequestContext.RequestMessage.Headers.To.Host;
                int port = AspNetSetting.HttpPorts.First();
                string relativePath = $"{timestamp}/{file.Number}";
                string absolutePath = $"{Path.GetFullPath(storageDirectory)}\\{file.Number}";
                string hostName = $"http://{host}:{port}";
                string fileUrl = $"{hostName}/{relativePath}";

                System.IO.File.WriteAllBytes(absolutePath, fileBuffer);
                file.Save(relativePath, absolutePath, hostName, fileUrl);
            }

            return file;
        }
        #endregion
    }
}
