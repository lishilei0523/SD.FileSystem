using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SD.FileSystem.WindowsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //上传单文件
            //UploadFile();

            //上传多文件
            UploadFiles();

            Console.ReadKey();
        }

        static void UploadFile()
        {
            string url = "http://localhost:4987/Api/Load/UploadFile";

            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");

            string filePath = CreateTestFile();
            string fileName = Path.GetFileName(filePath);
            byte[] fileBytes;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fileBytes = new byte[stream.Length];
                stream.Read(fileBytes, 0, fileBytes.Length);
            }
            request.AddFileBytes("formFile", fileBytes, fileName);

            RestClient restClient = new RestClient(url);
            IRestResponse response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！\n" + response.Content);
            }
            else
            {
                Console.WriteLine("上传失败！\n" + response.Content);
            }

            File.Delete(filePath);
        }

        static void UploadFiles()
        {
            //string url = "http://192.168.51.100:4987/Api/Load/UploadFiles";
            string url = "http://localhost:4987/Api/Load/UploadFiles";

            RestRequest request = new RestRequest(Method.POST);
            request.AddParameter("use", "用途");
            request.AddParameter("description", "描述");

            IList<string> filePaths = new List<string>();
            for (int index = 0; index < 3; index++)
            {
                string filePath = CreateTestFile();
                string fileName = Path.GetFileName(filePath);

                byte[] fileBytes;
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileBytes = new byte[stream.Length];
                    stream.Read(fileBytes, 0, fileBytes.Length);
                }

                request.AddFileBytes("formFiles", fileBytes, fileName);
                filePaths.Add(filePath);
            }

            RestClient restClient = new RestClient(url);
            IRestResponse response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("上传成功！\n" + response.Content);
            }
            else
            {
                Console.WriteLine("上传失败！\n" + response.Content);
            }

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        static string CreateTestFile()
        {
            string fileName = Guid.NewGuid().ToString();
            string text = "ReadMe";
            string path = $@"D:\{fileName}.txt";

            File.WriteAllText(path, text);

            return path;
        }
    }
}
