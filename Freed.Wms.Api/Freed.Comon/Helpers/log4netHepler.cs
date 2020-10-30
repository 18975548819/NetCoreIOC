using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Freed.Common.Helpers
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class log4netHepler
    {
        public static ILoggerRepository repository { get; set; }

        public static string message { get; set; }

        private static log4netHepler _InitLog;


        public static log4netHepler InitLog
        {
            get
            {
                if (_InitLog == null)
                {
                    _InitLog = new log4netHepler();

                }
                return _InitLog;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public log4netHepler()
        {
            repository = LogManager.CreateRepository("log4net");
            XmlConfigurator.Configure(repository, new System.IO.FileInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(2);
            MethodBase methodBase = stackFrame.GetMethod();
            message = "ClassName:" + methodBase.ReflectedType.Name + " FunctionName:" + methodBase.Name + " MessageInfo:";
        }


        public static ILog DoLogInit<T>()
        {
            ILog log = LogManager.GetLogger(repository.Name, typeof(T));
            return log;
        }

        /// <summary>
        /// 写信息日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteInfo<T>(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, typeof(T));
            log.Info(message + msg);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteError<T>(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, typeof(T));
            log.Error(message + msg);
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteWarn<T>(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, typeof(T));
            log.Warn(message + msg); ;
        }

        /// <summary>
        /// 写运行日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteDebug<T>(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, typeof(T));
            log.Debug(message + msg);
        }




        #region 22
        public static ILog DoLogInit()
        {
            ILog log = LogManager.GetLogger(repository.Name,"初始化");
            return log;
        }

        /// <summary>
        /// 写信息日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteInfo(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, "普通日志");
            log.Info(message + msg);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteError(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, "写错误日志");
            log.Error(message + msg);
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteWarn(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, "警告日");
            log.Warn(message + msg); ;
        }

        /// <summary>
        /// 写运行日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void WriteDebug(string msg)
        {
            ILog log = LogManager.GetLogger(repository.Name, "运行日志");
            log.Debug(message + msg);
        }
        #endregion
    }
}
