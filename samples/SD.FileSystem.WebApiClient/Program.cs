﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using FileInfo = SD.FileSystem.IAppService.DTOs.Outputs.FileInfo;

namespace SD.FileSystem.WebApiClient
{
    class Program
    {
        static void Main()
        {
            //上传单文件
            UploadFile();

            //上传多文件
            UploadFiles();

            //下载文件
            DownloadFile();

            Console.ReadKey();
        }

        static void UploadFile()
        {
            string filePath = CreateFile();
            string fileName = Path.GetFileName(filePath);
            byte[] buffer;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
            }

            string url = "http://localhost:49871/Api/Load/UploadFile";
            RestClient httpClient = new RestClient(url);

            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");
            request.AddFileBytes("formFile", buffer, fileName);

            IRestResponse<FileInfo> response = httpClient.Execute<FileInfo>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"文件Id：{response.Data.Id}");
                Console.WriteLine($"文件名称：{response.Data.Name}");
                Console.WriteLine($"链接地址：{response.Data.Url}");
            }
            else
            {
                Console.WriteLine("上传失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }

        static void UploadFiles()
        {
            IDictionary<string, byte[]> buffers = new Dictionary<string, byte[]>();
            for (int index = 0; index < 3; index++)
            {
                string filePath = CreateFile();
                string fileName = Path.GetFileName(filePath);
                byte[] buffer;
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                }
                buffers.Add(fileName, buffer);
            }

            string url = "http://localhost:49871/Api/Load/UploadFiles";
            RestClient httpClient = new RestClient(url);

            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");
            foreach (KeyValuePair<string, byte[]> kv in buffers)
            {
                request.AddFileBytes("formFiles", kv.Value, kv.Key);
            }

            IRestResponse<IEnumerable<FileInfo>> response = httpClient.Execute<IEnumerable<FileInfo>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！");
                Console.WriteLine("-------------------------------");
                foreach (FileInfo fileInfo in response.Data)
                {
                    Console.WriteLine($"文件Id：{fileInfo.Id}");
                    Console.WriteLine($"文件名称：{fileInfo.Name}");
                    Console.WriteLine($"链接地址：{fileInfo.Url}");
                    Console.WriteLine("-------------------------------");
                }
            }
            else
            {
                Console.WriteLine("上传失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }

        static void DownloadFile()
        {
            Guid fileId = new Guid("2D2255B1-DDB7-4A1C-95DC-E55CA0EE511C");

            string url = "http://localhost:49871/Api/Load/DownloadFile";

            RestClient httpClient = new RestClient(url);

            RestRequest request = new RestRequest(Method.GET);
            request.AddQueryParameter("fileId", fileId.ToString());

            IRestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                const string contentDispositionName = "Content-Disposition";
                Parameter contentDispositionParam = response.Headers.Single(x => x.Name == contentDispositionName);
                string contentDispositionText = contentDispositionParam.Value?.ToString();
                ContentDisposition contentDisposition = new ContentDisposition(contentDispositionText);
                string fileName = contentDisposition.FileName;
                byte[] buffer = response.RawBytes;

                Console.WriteLine("下载成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"文件名称：{fileName}");
                Console.WriteLine($"文件大小：{buffer.Length}");

                string filePath = $@"D:\{fileName}";
                File.WriteAllBytes(filePath, buffer);

                Console.WriteLine($"文件已保存至\"{filePath}\"");
            }
            else
            {
                Console.WriteLine("下载失败！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
            }
        }

        static string CreateFile()
        {
            string fileName = Guid.NewGuid().ToString();
            string filePath = $@"D:\{fileName}.txt";
            string text = "ReadMe";

            File.WriteAllText(filePath, text);

            return filePath;
        }
    }
}
