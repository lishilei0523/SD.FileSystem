using SD.FileSystem.Domain.Entities;
using SD.FileSystem.Domain.IRepositories;
using SD.Infrastructure.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SD.FileSystem.Repository.Implements
{
    /// <summary>
    /// 文件仓储实现
    /// </summary>
    public class FileRepository : EFAggRootRepositoryProvider<File>, IFileRepository
    {
        #region # 分页获取文件列表 —— ICollection<File> FindByPage(string keywords, string extensionName...
        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="uploadedDate">上传日期</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>文件列表</returns>
        public ICollection<File> FindByPage(string keywords, string extensionName, DateTime? uploadedDate, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, out int rowCount, out int pageCount)
        {
            uploadedDate = uploadedDate?.Date;

            Expression<Func<File, bool>> condition =
                x =>
                    (string.IsNullOrEmpty(keywords) || x.Keywords.Contains(keywords)) &&
                    (string.IsNullOrEmpty(extensionName) || x.ExtensionName == extensionName) &&
                    (uploadedDate == null || x.UploadedDate == uploadedDate) &&
                    (startTime == null || x.AddedTime >= startTime) &&
                    (endTime == null || x.AddedTime <= endTime);

            IQueryable<File> files = base.FindByPage(condition, pageIndex, pageSize, out rowCount, out pageCount);

            return files.ToList();
        }
        #endregion
    }
}
