#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 四川******有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[河边骨]   时间：2017/11/29 16:18:22 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;

namespace HeBianGu.App.Tool
{
    /// <summary> 文件夹有关的操作类 </summary>
    public static class DirectoryHelper
    {
        /// <summary> 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.指定目录的绝对路径 </summary>
        public static string[] GetDirs(this string directoryPath)
        {
            return Directory.GetDirectories(directoryPath);
        }

        /// <summary> 创建文件夹 指定目录的绝对路径 </summary>
        public static void CreateDir(this string destDirectory)
        {
            if (!string.IsNullOrEmpty(destDirectory) && !Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
        }

        /// <summary> 复制文件夹 要复制的文件夹 复制到的文件夹 p2 = 是否清空文件夹内容 </summary>
        public static bool CopyDir(this string strFromDirectory, string strToDirectory, bool recursive = true)
        {
            //  是否清空文件夹内容
            if (recursive)
            {
                if (Directory.Exists(strToDirectory))
                {
                    Directory.Delete(strToDirectory, true);
                }
            }


            Directory.CreateDirectory(strToDirectory);

            if (!Directory.Exists(strFromDirectory)) return false;

            string[] directories = Directory.GetDirectories(strFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyDir(d, strToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(strFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, strToDirectory + s.Substring(s.LastIndexOf("\\")));
                }
            }
            return true;
        }

        /// <summary> 删除文件夹 </summary>
        public static bool DeleteDir(this string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }
            else //文件夹不存在
            {
                return false;
            }
            return true;
        }

        /// <summary> 得到当前文件夹中所有文件列表string[] </summary>
        public static string[] GetDirFiles(this string dirFullPath)
        {
            string[] fileList;

            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, "*.*", SearchOption.TopDirectoryOnly);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary> 得到当前文件夹及下级文件夹中所有文件列表string[] 查找文件的选项，是否包含子级文件夹 </summary>
        public static string[] GetDirFiles(this string dirFullPath, SearchOption so)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, "*.*", so);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary> 得到当前文件夹中指定文件类型［扩展名］文件列表string[] 查找文件的扩展名如“*.*代表所有文件；*.doc代表所有doc文件” </summary>
        public static string[] GetDirFiles(this string dirFullPath, string searchPattern)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, searchPattern);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary> 得到当前文件夹及下级文件夹中指定文件类型［扩展名］文件列表string[] 查找文件的扩展名如“*.*代表所有文件；*.doc代表所有doc文件” </summary>
        public static string[] GetDirFiles(this string dirFullPath, string searchPattern, SearchOption so)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, searchPattern, so);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary> 确保文件夹被创建 </summary>
        public static void AssertDirExist(this string filePath)
        {
            DirectoryInfo dir = new DirectoryInfo(filePath);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }

        /// <summary> 检测指定目录是否存在 </summary>
        public static bool IsExistDirectory(this string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary> 检测指定目录是否为空 </summary>
        public static bool IsEmptyDirectory(this string directoryPath)
        {
            //判断是否存在文件
            string[] fileNames = GetFileNames(directoryPath);

            if (fileNames.Length > 0)
            {
                return false;
            }

            //判断是否存在文件夹
            string[] directoryNames = GetDirs(directoryPath);

            if (directoryNames.Length > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary> 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法. </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <returns>bool 是否包含文件</returns>
        public static bool ContainFile(this string directoryPath, string searchPattern)
        {
            //获取指定的文件列表
            string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

            //判断指定文件是否存在
            if (fileNames.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary> 检测指定目录中是否存在指定的文件 </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        /// <returns>bool 是否包含文件</returns>
        public static bool ContainFile(this string directoryPath, string searchPattern, bool isSearchChild)
        {
            //获取指定的文件列表
            string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

            //判断指定文件是否存在
            if (fileNames.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary> 取当前目录 </summary>
        /// <returns>当前目录名</returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary> 设当前目录 </summary>
        /// <param name="path">目录绝对路径</param>
        public static void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }

        /// <summary> 取路径中不充许存在的字符 </summary>
        /// <returns>不充许存在的字符</returns>
        public static char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        /// <summary> 取系统所有的逻辑驱动器 </summary>
        /// <returns>所有的逻辑驱动器</returns>
        public static DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }

        /// <summary> 获取指定目录中所有文件列表 </summary>
        public static string[] GetFileNames(this string directoryPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            //获取文件列表
            return Directory.GetFiles(directoryPath);
        }

        /// <summary> 获取指定目录及子目录中所有文件列表 </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        /// <returns>指定目录及子目录中所有文件列表</returns>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            if (isSearchChild)
            {
                return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
            }
            return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
        }

        /// <summary> 删除当前文件夹下所有文件 </summary>
        public static bool DeleteAllFile(this DirectoryInfo dir)
        {

            dir.DeleteAllFile(l => true);

            return true;
        }

        /// <summary> 删除当前文件夹下所有匹配的文件 </summary>
        public static bool DeleteAllFile(this DirectoryInfo dir, Predicate<FileInfo> match)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                //  递归删除文件
                d.DeleteAllFile(match);

                //  文件夹是空则删除
                if (d.GetFileSystemInfos().Length == 0)
                {
                    d.Delete();
                }
            }


            foreach (FileInfo f in dir.GetFiles())
            {
                //  删除匹配文件
                if (match(f))
                {
                    f.Delete();
                }
            }

            return true;
        }

        /// <summary> 删除当前文件夹下匹配的文件夹 </summary>
        public static void DeleteCurrentDir(this DirectoryInfo dir, Predicate<DirectoryInfo> match)
        {
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                if (match(d))
                {
                    d.Delete(true);
                }
            }
        }

        /// <summary> 获取当前文件夹下所有匹配的文件 </summary>
        public static List<string> GetAllFile(this DirectoryInfo dir, Predicate<FileInfo> match = null)
        {
            List<string> ss = new List<string>();

            foreach (FileSystemInfo d in dir.GetFileSystemInfos())
            {
                if (d is DirectoryInfo)
                {
                    DirectoryInfo dd = d as DirectoryInfo;
                    ss.AddRange(dd.GetAllFile(match));
                }
                else if (d is FileInfo)
                {
                    FileInfo dd = d as FileInfo;

                    if (match == null || match(dd))
                    {

                        ss.Add(d.FullName);
                    }
                }
            }

            return ss;
        }

        /// <summary> 获取当前文件夹下所有匹配的文件 </summary>
        public static List<string> GetAllFile(this string dirPath, Predicate<FileInfo> match = null)
        {
            List<string> ss = new List<string>();

            if (!Directory.Exists(dirPath)) return ss;

            DirectoryInfo dir = new DirectoryInfo(dirPath);

            foreach (FileSystemInfo d in dir.GetFileSystemInfos())
            {
                if (d is DirectoryInfo)
                {
                    DirectoryInfo dd = d as DirectoryInfo;
                    ss.AddRange(dd.GetAllFile(match));
                }
                else if (d is FileInfo)
                {
                    FileInfo dd = d as FileInfo;

                    if (match == null || match(dd))
                    {
                        ss.Add(d.FullName);
                    }
                }
            }

            return ss;
        }



        /// <summary> 递归创建文件夹 </summary>
        public static string GetIndexFolderName(this string folderPath, bool isAutoCreate = false)
        {
            // Todo ：递归创建文件夹 
            int index = 0;

            string folder = Path.GetDirectoryName(folderPath);

            string folderName = Path.GetFileNameWithoutExtension(folderPath);

            Func<string, string> action = null;

            action = l =>
            {
                if (!Directory.Exists(l))
                {
                    if (isAutoCreate)
                    {
                        Directory.CreateDirectory(l);
                    }

                    return l;
                }
                else
                {
                    index++;

                    string k = Path.Combine(folder, folderName + "（" + index + "）");

                    return action(k);

                }
            };

            return action.Invoke(folderPath);
        }

        /// <summary> Bytes到KB,MB,GB,TB单位智能转换 </summary>
        public static string ConvertBytes(long len)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }
            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        /// <summary> 文件大小到KB,MB,GB,TB单位智能转换 </summary>
        public static string GetSizeString(this string filePath)
        {
            if (!File.Exists(filePath)) return "0";

            FileInfo file = new FileInfo(filePath);

            return ConvertBytes(file.Length);

        }

    }
}
