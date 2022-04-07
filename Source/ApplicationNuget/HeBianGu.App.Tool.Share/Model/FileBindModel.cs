using System;
using System.Drawing;
using System.IO;

namespace HeBianGu.App.Tool
{
    /// <summary> 文件绑定实体 </summary>
    [Serializable]
    public class FileBindModel
    {
        public FileBindModel()
        {

        }

        public FileBindModel(string path)
        {
            if (Path.HasExtension(path))
            {
                if (SysTemConfiger.ExceptShowFile.Exists(l => l == Path.GetExtension(path)))
                {
                    return;
                }

                FileName = Path.GetFileNameWithoutExtension(path);
                FilePath = path;
                IsFile = true;
            }
            else
            {
                FileName = Path.GetFileName(path);
                FilePath = path;
                IsFile = false;
            }

            LastTime = DateTime.Now;
        }

        public FileBindModel(System.IO.FileSystemInfo sysFile)
        {
            if (SysTemConfiger.ExceptShowFile.Exists(l => l == sysFile.Extension))
            {
                return;
            }

            if (sysFile is FileInfo)
            {
                FileName = sysFile.Name;
                FilePath = sysFile.FullName;
                IsFile = true;
            }
            else
            {
                FileName = sysFile.Name;
                FilePath = sysFile.FullName;
                IsFile = false;
            }

            LastTime = DateTime.Now;
        }

        private string _fileName;
        /// <summary> 说明 </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        private string _filePath;
        /// <summary> 说明 </summary>
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        private bool _isFile;
        /// <summary> 说明 </summary>
        public bool IsFile
        {
            get { return _isFile; }
            set { _isFile = value; }
        }

        /// <summary> 图片路径 </summary>
        public Icon ImagePath
        {
            get { return IconHelper.Instance.GetSystemInfoIcon(FilePath); }
        }

        private DateTime _lastTime;
        /// <summary> 说明 </summary>
        public DateTime LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }

    }
}
