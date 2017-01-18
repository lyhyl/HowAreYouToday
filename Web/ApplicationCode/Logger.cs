using System;

namespace HRUTWeb
{
    public static class Logger
    {
        public static void SetConfig(string root)
        {
            log4net.Appender.FileAppender fAppender = new log4net.Appender.FileAppender();
            log4net.Layout.PatternLayout layout = new log4net.Layout.PatternLayout
            {
                ConversionPattern = "[%date] %thread -- %-5level -- %logger [%M] -- %message%newline"
            };

            layout.ActivateOptions();

            fAppender.File = System.IO.Path.Combine(root, @"Log\HRUT.log");
            fAppender.Layout = layout;
            fAppender.AppendToFile = true;
            fAppender.ActivateOptions();
            log4net.Config.BasicConfigurator.Configure(fAppender);
        }
        
        public static void WriteLog(string info)
        {
            if (LogInfo.IsInfoEnabled)
            {
                try
                {
                    LogInfo.Info(info);
                    System.Diagnostics.Debug.WriteLine(info);
                }
                catch
                {
                }
            }
        }

        public static void WriteLog(string info, Exception se)
        {
            if (LogError.IsErrorEnabled)
            {
                try
                {
                    LogError.Error(info, se);
                    System.Diagnostics.Debug.WriteLine(info);
                }
                catch
                {
                }
            }
        }
        
        private static readonly log4net.ILog LogInfo = log4net.LogManager.GetLogger("loginfo");
        private static readonly log4net.ILog LogError = log4net.LogManager.GetLogger("logerror");
    }
}