using log4net;
using log4net.Config;
using log4net.Core;
using System;
using System.IO;

namespace csharp.Services
{
	public class Logger
	{
		private static ILog logger;

		public void Error(Exception e)
		{
			logger.Logger.Log(typeof(Logger), Level.Error, e.Message, e);
		}

		public void Info(string info)
		{
			logger.Logger.Log(typeof(Logger), Level.Info, info, null);
		}

		public Logger(string loggerName)
		{
			var info = new FileInfo("Resources/log4net.config");
			XmlConfigurator.Configure(info);
			logger = LogManager.GetLogger(loggerName);
		}
	}
}