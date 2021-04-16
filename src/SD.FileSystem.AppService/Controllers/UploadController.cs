using SD.Toolkits.AspNet;
using SD.Toolkits.AspNet.Configurations;
using SD.Toolkits.WebApi.Extensions;
using SD.Toolkits.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FileInfo = SD.FileSystem.AppService.Models.FileInfo;

namespace SD.FileSystem.AppService.Controllers
{
    /// <summary>
    /// 文件上传控制器
    /// </summary>
    public class UploadController : ApiController
    {
        //Public

        #region # 上传文件 —— IFormFile UploadFile(string description, IFormFile file)
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="file">Http文件</param>
        /// <returns>Http文件</returns>
        [HttpPost]
        [FileParameters]
        public FileInfo UploadFile(string description, IFormFile file)
        {
            #region # 验证

            if (!base.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            #endregion

            Trace.WriteLine(description);
            Trace.WriteLine(file);
            //this.SaveFile(file); //TODO 保存数据库

            return null;
        }
        #endregion

        #region # 批量上传文件 —— IFormFileCollection UploadFiles(IFormFileCollection files)
        /// <summary>
        /// 批量上传文件
        /// </summary>
        [HttpPost]
        [FileParameters]
        public IEnumerable<FileInfo> UploadFiles(IFormFileCollection files)
        {
            #region # 验证

            if (!base.Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            if (files == null || !files.Any())
            {
                return null;
            }

            #endregion

            Trace.WriteLine(files);
            foreach (IFormFile file in files)
            {
                //this.SaveFile(file);//TODO 保存数据库
            }

            return null;
        }
        #endregion


        //Private

        #region # 保存文件 —— void SaveFile(IFormFile formFile)
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="formFile">Http文件</param>
        /// <returns>Http文件</returns>
        private void SaveFile(IFormFile formFile)
        {
            string timestamp = DateTime.Today.ToString("yyyyMMdd");
            string directory = $@"{AspNetSection.Setting.FileServer.Path}\{timestamp}";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string filePath = $@"{directory}\{formFile.FileName}";
            File.WriteAllBytes(filePath, formFile.Datas);

            IList<Uri> uris = new List<Uri>();
            foreach (HostElement host in AspNetSection.Setting.HostElement)
            {
                string url = $"{host.Url}/{timestamp}/{formFile.FileName}";
                uris.Add(new Uri(url));
            }
        }
        #endregion
    }
}
