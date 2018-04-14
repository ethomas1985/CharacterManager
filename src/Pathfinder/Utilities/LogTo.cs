using System;
using Serilog;
using Serilog.Core;

namespace Pathfinder.Utilities
{
	public static class LogTo
	{
		private static Logger _logger;

		private static Logger Logger
		{
			get
			{
				if (_logger == null)
				{
					_logger = new LoggerConfiguration()
						.WriteTo.Console()
						.WriteTo.MongoDB("mongodb://localhost:32768/pathfinder_log")
						.WriteTo.File($"./Log.{DateTime.Now:yyyy-MM-dd}.log")
						.CreateLogger();
				}

				return _logger;
			}
		}

		public static void Info(string pMessage, params object[] pPropertyValues)
		{
			Logger.Information($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|Pathfinder|{pMessage}", pPropertyValues);
		}

		public static void Warning(string pMessage, params object[] pPropertyValues)
		{
			Logger.Warning($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|Pathfinder|{pMessage}", pPropertyValues);
		}

		public static void Debug(string pMessage)
		{
			Logger.Debug($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|Pathfinder|{pMessage}");
		}
	}
}
