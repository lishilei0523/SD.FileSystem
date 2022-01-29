using Microsoft.Extensions.DependencyInjection;
using SD.FileSystem.IAppService.DTOs.Inputs;
using SD.FileSystem.IAppService.DTOs.Outputs;
using SD.FileSystem.IAppService.Interfaces;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetCore;
using SD.IOC.Extension.NetCore.ServiceModel;
using System;
using System.IO;

namespace Sample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //初始化依赖注入
            InitContainer();

            //上传
            UploadFile();

            //下载
            //DownloadFile();

            Console.ReadKey();
        }

        static void InitContainer()
        {
            IServiceCollection serviceCollection = ResolveMediator.GetServiceCollection();
            serviceCollection.RegisterConfigs();
            serviceCollection.RegisterServiceModels();
            ResolveMediator.Build();

            Console.WriteLine("依赖注入已初始化！");
            Console.WriteLine("-------------------------------------");
        }

        static void UploadFile()
        {
            string filePath = CreateFile();
            string fileName = Path.GetFileName(filePath);

            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            UploadRequest file = new UploadRequest
            {
                FileName = fileName,
                Use = "用途",
                Description = "描述",
                Datas = stream
            };

            ILoadContract loadContract = ResolveMediator.Resolve<ILoadContract>();
            UploadResponse response = loadContract.UploadFile(file);
            Console.WriteLine(response);

            stream.Flush();
            File.Delete(filePath);
        }

        static void DownloadFile()
        {
            ILoadContract loadContract = ResolveMediator.Resolve<ILoadContract>();

            DownloadRequest request = new DownloadRequest(new Guid("2E21E5E5-4739-421F-8692-D3AF2B32A03F"));
            DownloadResponse response = loadContract.DownloadFile(request);

            byte[] buffer = new byte[response.ContentLength];
            response.Datas.Read(buffer);
            File.WriteAllBytes(@"D:\Download.txt", buffer);

            Console.WriteLine(response);
        }

        static string CreateFile()
        {
            string text = "ReadMe";
            string path = @"D:\ReadMe.txt";

            File.WriteAllText(path, text);

            return path;
        }
    }
}
