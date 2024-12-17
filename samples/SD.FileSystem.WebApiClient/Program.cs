using RestSharp;
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
            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            const string url = "http://localhost:49871/Api/Load/UploadFile";

            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");
            request.AddFile("formFile", buffer, fileName);

            RestResponse<FileInfo> response = httpClient.Execute<FileInfo>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
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
                using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                buffers.Add(fileName, buffer);
            }

            const string url = "http://localhost:49871/Api/Load/UploadFiles";

            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Post);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");
            foreach (KeyValuePair<string, byte[]> kv in buffers)
            {
                request.AddFile("formFiles", kv.Value, kv.Key);
            }

            RestResponse<IEnumerable<FileInfo>> response = httpClient.Execute<IEnumerable<FileInfo>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine(response.Content);
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
            Guid fileId = new Guid("8F75F350-4619-46C2-A9C8-B7A7C5E82868");

            const string url = "http://localhost:49871/Api/Load/DownloadFile";

            using RestClient httpClient = new RestClient();
            RestRequest request = new RestRequest(url, Method.Get);
            request.AddQueryParameter("fileId", fileId.ToString());

            RestResponse response = httpClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                const string contentDispositionName = "Content-Disposition";
                Parameter contentDispositionParam = response.ContentHeaders!.Single(x => x.Name == contentDispositionName);
                string contentDispositionText = contentDispositionParam.Value?.ToString();
                ContentDisposition contentDisposition = new ContentDisposition(contentDispositionText!);
                string fileName = contentDisposition.FileName;
                byte[] buffer = response.RawBytes;

                Console.WriteLine("下载成功！");
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"文件名称：{fileName}");
                Console.WriteLine($"文件大小：{buffer!.Length}");

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

            File.WriteAllText(filePath, fileName);

            return filePath;
        }
    }
}
