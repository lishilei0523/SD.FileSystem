using Microsoft.Extensions.DependencyInjection;
using SD.FileSystem.IAppService.DTOs.Inputs;
using SD.FileSystem.IAppService.DTOs.Outputs;
using SD.FileSystem.IAppService.Interfaces;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetCore.ServiceModel;
using System;
using System.IO;

namespace SD.FileSystem.WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化依赖注入
            InitContainer();

            //上传文件
            //UploadFile();

            //下载文件
            DownloadFile();

            Console.ReadKey();
        }

        static void InitContainer()
        {
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection serviceCollection = ResolveMediator.GetServiceCollection();
                serviceCollection.RegisterServiceModels();

                ResolveMediator.Build();
            }

            Console.WriteLine("依赖注入已初始化！");
            Console.WriteLine("-------------------------------------");
        }

        static void UploadFile()
        {
            string filePath = CreateFile();
            string fileName = Path.GetFileName(filePath);
            using FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            ILoadContract loadContract = ResolveMediator.Resolve<ILoadContract>();
            UploadRequest request = new UploadRequest(fileName, stream, "用途", "描述");
            UploadResponse response = loadContract.UploadFile(request);

            Console.WriteLine("上传成功！");
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"文件Id：{response.FileId}");
            Console.WriteLine($"文件名称：{response.FileName}");
            Console.WriteLine($"链接地址：{response.Url}");
        }

        static void DownloadFile()
        {
            ILoadContract loadContract = ResolveMediator.Resolve<ILoadContract>();
            DownloadRequest request = new DownloadRequest(new Guid("CD05E6C4-B195-4221-8CC2-BEEC3A79D2F5"));
            DownloadResponse response = loadContract.DownloadFile(request);

            Console.WriteLine("下载成功！");
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"文件名称：{response.FileName}");
            Console.WriteLine($"文件大小：{response.Size}");

            string filePath = $@"D:\{response.FileName}";
            byte[] buffer = new byte[response.Size];
            response.Datas.Read(buffer);
            File.WriteAllBytes(filePath, buffer);

            Console.WriteLine($"文件已保存至\"{filePath}\"");
        }

        static string CreateFile()
        {
            string fileName = Guid.NewGuid().ToString();
            string filePath = $@"D:\{fileName}.txt";
            string text = "Hello World !";

            File.WriteAllText(filePath, text);

            return filePath;
        }
    }
}
