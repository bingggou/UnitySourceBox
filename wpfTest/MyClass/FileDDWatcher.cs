using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfTest.MyTool
{
    public class FileDDWatcher
    {
        #region property && fileds

        private Hashtable tempWatchers = null;
        private Queue<string> pathQueue = new Queue<string>();
        private int moveFileCount = 0;

        #endregion

        #region instance
        private static object ooo = new object();
        private static FileDDWatcher _instance;
        public static FileDDWatcher Instance
        {
            get
            {
                lock (ooo)
                {
                    if (_instance == null)
                    {
                        _instance = new FileDDWatcher();
                    }
                    return _instance;
                }
            }
        }
        private FileDDWatcher() { }
        #endregion

        #region method

        #region private

        /// <summary>
        /// 文件监控触发的事件
        /// </summary>
        /// <returns></returns>       
        private void OnCreated(object source, FileSystemEventArgs e)
        {
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TempDirectory");
            if (e.FullPath.Contains(path))
            {
                return;
            }
            pathQueue.Enqueue(e.FullPath);
        }

        #endregion

        #region public

        /// <summary>
        /// 启动文件监控，传入移动文件的个数，默认是1个
        /// </summary>
        /// <returns></returns>
        public bool StartWatcher(int fileCount = 1)
        {
            //LogHelper.WriteInfo("StartWatcher");
            try
            {
                moveFileCount = fileCount;
                int i = 1;
                if (tempWatchers == null || tempWatchers.Count <= 0)
                {
                    tempWatchers = new Hashtable();
                    foreach (string driveName in Directory.GetLogicalDrives())
                    {
                        if (Directory.Exists(driveName))
                        {
                            FileSystemWatcher watcher = new FileSystemWatcher();
                            //watcher.Filter = "*.download";
                            watcher.NotifyFilter = NotifyFilters.FileName;
                            watcher.Created -= OnCreated;
                            watcher.Created += OnCreated;
                            watcher.IncludeSubdirectories = true;// 必须要的，监控子集目录
                            watcher.Path = driveName;
                            Console.WriteLine(driveName);
                            
                            watcher.EnableRaisingEvents = true;
                            tempWatchers.Add("file_watcher" + i.ToString(), watcher);
                            i++;
                        }
                    }
                }
                return true;
            }
            catch /*(Exception ex)*/
            {
                //LogHelper.WriteError(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 停止文件监控，如果队列中路径数量和文件移动数量一致，就不重置文件传输个数
        /// </summary>
        /// <returns></returns>
        public bool StopWatcher()
        {
            try
            {
                if (tempWatchers != null && tempWatchers.Count > 0)
                {
                    //LogHelper.WriteInfo(tempWatchers.Count + "");
                    for (int i = 1; i <= tempWatchers.Count; i++)
                    {
                        ((FileSystemWatcher)tempWatchers["file_watcher" + i.ToString()]).Dispose();
                    }
                    tempWatchers.Clear();
                    tempWatchers = null;
                }
                return true;
            }
            catch/* (Exception ex)*/
            {
                return false;
            }
            finally
            {
                moveFileCount = 0;
            }
        }
        /// <summary>
        /// 返回文件保存的路径
        /// </summary>
        /// <returns></returns>
        public string[] GetTempFilePath()
        {
            try
            {
                int length = pathQueue.Count;
                if (length <= 0)
                {
                    return new string[0];
                }

                string[] ls = new string[length];
                for (int i = 0; i < length; i++)
                {
                    ls[i] = pathQueue.Dequeue();
                }
                return ls;
            }
            catch /*(Exception ex)*/
            {
                return new string[0];
            }
            finally
            {
                moveFileCount = 0;
            }
        }
        #endregion

        #endregion
    }
}
