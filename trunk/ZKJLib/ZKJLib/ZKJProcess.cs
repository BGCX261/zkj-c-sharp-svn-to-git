using System;
using System.Diagnostics;
namespace ZKJLib
{ 

    /// <summary>
    /// 封装进程类，提供启动，查找和关闭进程的方法
    /// </summary>
    public class ZKJProcess
    {
        Process process;
        public string ProcessName = null;
        

        /// <summary>
        /// Processname表示要处理的程序路径名
        /// </summary>
        /// <param name="Processname"></param>
        public ZKJProcess(string Processname)
        {
            process = new Process();
            process.StartInfo.FileName = Processname;
        }

        /// <summary>
        /// Processname表示要处理的程序路径名
        /// Arguments表示参数
        /// </summary>
        /// <param name="Processname"></param>
        /// <param name="Arguments"></param>
        public ZKJProcess(string Processname, string Arguments): this(Processname)
        {
            process.StartInfo.Arguments = Arguments;
        }

        /// <summary>
        /// Processname表示要处理的程序路径名
        /// WindowStyle表示程序的显示方式
        /// </summary>
        /// <param name="Processname"></param>
        /// <param name="WindowStyle"></param>
        public ZKJProcess(string Processname, ProcessWindowStyle WindowStyle): this(Processname)
        {
            process.StartInfo.WindowStyle = WindowStyle;
        }

        /// <summary>
        /// Processname表示要处理的程序路径名
        /// Arguments表示参数
        /// WindowStyle表示程序的显示方式
        /// </summary>
        /// <param name="Processname"></param>
        /// <param name="Arguments"></param>
        /// <param name="WindowStyle"></param>
        public ZKJProcess(string Processname, string Arguments, ProcessWindowStyle WindowStyle)
            : this(Processname, Arguments)
        {
            process.StartInfo.WindowStyle = WindowStyle;
        }

        /// <summary>
        /// 启动指定的程序
        /// </summary>
        /// <returns>
        /// 成功返回进程名，失败返回失败信息
        /// </returns>
        public string Start()
        {
            try
            {
                process.Start();
            }
            catch(Exception e)
            {
                return e.Message;
            }
            ProcessName = process.ProcessName;
            return ProcessName;
        }

        /// <summary>
        /// 查找进程
        /// </summary>
        /// <returns>true表示找到，false表示未找到</returns>
        public bool Search()
        {
            //foreach (Process p in Process.GetProcesses())
            //{
            //    if (p.ProcessName == ProcessName)
            //    {
            //        return true;
            //    }
            //}
            //return false;
            return ZKJProcess.Search(ProcessName);
        }

        /// <summary>
        /// 查找进程
        /// </summary>
        /// <param name="ProcessName"></param>
        /// <returns>true表示找到，false表示未找到</returns>
        public static bool  Search(string ProcessName)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == ProcessName)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 关闭进程
        /// </summary>
        public void Close()
        {
            ZKJProcess.Close(ProcessName);
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="ProcessName"></param>
        public static void Close(string ProcessName)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == ProcessName)
                {
                    p.Kill();
                    p.Dispose();
                }
            }
        }
    }
}
