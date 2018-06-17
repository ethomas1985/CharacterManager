using System;
using System.Configuration;
using System.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Pathfinder.Utilities
{
	public static class LogTo
	{
		private static LogEventLevel InitialMinimumLevel { get; }
			= Debugger.IsAttached ? LogEventLevel.Debug : LogEventLevel.Information;

		private static LoggingLevelSwitch LogLevelSwitch { get; }
			= new LoggingLevelSwitch(InitialMinimumLevel);

		private static Logger _logger;
		private static Logger Logger
		{
			get
			{
				if (_logger == null)
				{
					_logger = LoggerFactory();
				}

				return _logger;
			}
		}

		private static Logger LoggerFactory()
		{
			var loggerFactory = new LoggerConfiguration()
				.MinimumLevel.ControlledBy(LogLevelSwitch)
				.WriteTo.Console()
				.WriteTo.File($"./Log.{DateTime.Now:yyyy-MM-dd}.log");

			if ("Mongo".Equals(ConfigurationManager.AppSettings["LogToDatabase"]))
			{
				loggerFactory = loggerFactory.WriteTo.MongoDB("mongodb://localhost:32768/pathfinder_log");
			}

			return loggerFactory.CreateLogger();
		}

		public static void ChangeLogLevel(string pLogLevel)
		{
			if (Enum.TryParse(pLogLevel, true, out LogEventLevel outlevel))
			{
				ChangeLogLevel(outlevel);
			}
		}

		private static void ChangeLogLevel(LogEventLevel pLogLevel)
		{
			LogLevelSwitch.MinimumLevel = pLogLevel;
			Info("Log Level set to {Level}", pLogLevel);
		}

		public static void Exception(Exception pException)
		{
			Logger.Error(pException, pException.Message);
		}

		public static void Error(string pMessage, params object[] pPropertyValues)
		{
			Logger.Error($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|{pMessage}", pPropertyValues);
		}

		public static void Warning(string pMessage, params object[] pPropertyValues)
		{
			Logger.Warning($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|{pMessage}", pPropertyValues);
		}

		public static void Info(string pMessage, params object[] pPropertyValues)
		{
			Logger.Information($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|{pMessage}", pPropertyValues);
		}

		public static void Debug(string pMessage, params object[] pPropertyValues)
		{
			Logger.Debug($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|{pMessage}", pPropertyValues);
		}

		public static void Verbose(string pMessage, params object[] pPropertyValues)
		{
			Logger.Verbose($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}|{pMessage}", pPropertyValues);
		}
	}
}
