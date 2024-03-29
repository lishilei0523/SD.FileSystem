﻿using SD.FileSystem.Domain.Entities;
using SD.Infrastructure.RepositoryBase;
using System;
using System.Collections.Generic;

namespace SD.FileSystem.Domain.IRepositories
{
    /// <summary>
    /// 文件仓储接口
    /// </summary>
    public interface IFileRepository : IAggRootRepository<File>
    {
        #region # 根据哈希值获取默认文件 —— File DefaultByHash(string hashValue)
        /// <summary>
        /// 根据哈希值获取默认文件
        /// </summary>
        /// <param name="hashValue">哈希值</param>
        /// <returns>文件</returns>
        /// <remarks>如果无，则返回null</remarks>
        File DefaultByHash(string hashValue);
        #endregion

        #region # 根据哈希值获取文件列表 —— ICollection<File> FindByHash(string hashValue)
        /// <summary>
        /// 根据哈希值获取文件列表
        /// </summary>
        /// <param name="hashValue">哈希值</param>
        /// <returns>文件列表</returns>
        ICollection<File> FindByHash(string hashValue);
        #endregion

        #region # 分页获取文件列表 —— ICollection<File> FindByPage(string keywords, string extensionName...
        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="extensionName">扩展名</param>
        /// <param name="hashValue">哈希值</param>
        /// <param name="uploadedDate">上传日期</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="rowCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns>文件列表</returns>
        ICollection<File> FindByPage(string keywords, string extensionName, string hashValue, DateTime? uploadedDate, DateTime? startTime, DateTime? endTime, int pageIndex, int pageSize, out int rowCount, out int pageCount);
        #endregion

        #region # 给定哈希的文件数量 —— int CountByHash(string hashValue)
        /// <summary>
        /// 给定哈希的文件数量
        /// </summary>
        /// <param name="hashValue">哈希</param>
        /// <returns>文件数量</returns>
        int CountByHash(string hashValue);
        #endregion

        #region # 是否存在哈希 —— bool ExsitsHash(string hashValue
        /// <summary>
        /// 是否存在哈希
        /// </summary>
        /// <param name="hashValue">哈希值</param>
        /// <returns>是否存在</returns>
        bool ExsitsHash(string hashValue);
        #endregion
    }
}
