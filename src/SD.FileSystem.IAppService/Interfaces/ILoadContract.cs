using SD.FileSystem.IAppService.DTOs.Inputs;
using SD.FileSystem.IAppService.DTOs.Outputs;
using SD.Infrastructure.AppServiceBase;
using System.ServiceModel;

namespace SD.FileSystem.IAppService.Interfaces
{
    /// <summary>
    /// 文件上传/下载服务契约接口
    /// </summary>
    [ServiceContract(Namespace = "http://SD.FileSystem.IAppService.Interfaces")]
    public interface ILoadContract : IApplicationService
    {
        //命令部分

        #region # 上传文件 —— UploadResponse UploadFile(UploadRequest request)
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="request">上传请求</param>
        /// <returns>上传响应</returns>
        [OperationContract]
        UploadResponse UploadFile(UploadRequest request);
        #endregion


        //查询部分

        #region # 下载文件 —— DownloadResponse DownloadFile(DownloadRequest request)
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="request">下载请求</param>
        /// <returns>下载响应</returns>
        [OperationContract]
        DownloadResponse DownloadFile(DownloadRequest request);
        #endregion
    }
}
