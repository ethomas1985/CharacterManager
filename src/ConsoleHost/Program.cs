using System;
using System.Web.Http.SelfHost;
using Pathfinder.Api;
using Pathfinder.Utilities;

namespace ConsoleHost
{
	class Program
	{
		static readonly Uri _baseAddress = new Uri("http://localhost:8888/");
		static void Main(string[] args)
		{
			LogTo.Info($"Launching WebApi Service host.");

			AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

			LogTo.Debug($"Configuring WebApi Services.");
			// Set up server configuration
			var config = new HttpSelfHostConfiguration(_baseAddress);
			WebApiConfig.Register(config);

			LogTo.Debug($"Bringing server online.");
			var server = new HttpSelfHostServer(config);
			// Start listening
			server.OpenAsync().Wait();


			LogTo.Info($"Web API Self hosted on {_baseAddress}");
			PrintBanner();
			var stop = false;
			do
			{
				ConsoleKeyInfo input = Console.ReadKey();
				switch (input.KeyChar)
				{
					case 's':
					case 'S':
						stop = true;
						break;
					case 'c':
					case 'C':
						Console.Clear();
						break;
					case 'l':
					case 'L':
						Console.WriteLine($"Enter new log level [Error, Warning, Information, Debug, Verbose]:");
						var level = Console.ReadLine();
						if (!string.IsNullOrEmpty(level))
						{
							LogTo.ChangeLogLevel(level);
						}
						break;
					case '?':
						PrintBanner();
						break;
					default:
						break;
				}

			} while (!stop);

			server.CloseAsync().Wait();
		}

		private static void UnhandledExceptionTrapper(object pSender, UnhandledExceptionEventArgs pUnhandledExceptionEventArgs)
		{
			LogTo.Exception((Exception)pUnhandledExceptionEventArgs.ExceptionObject); ;

			Console.WriteLine("Press Enter to continue");
			Console.ReadLine();
			Environment.Exit(1);
		}

		private static void PrintBanner()
		{
			Console.WriteLine(new string('=', 60));
			Console.WriteLine($"Enter command:");
			Console.WriteLine($"\t[sS]: Stop service");
			Console.WriteLine($"\t[cC]: Clear console");
			Console.WriteLine($"\t[lL]: Change log level");
			Console.WriteLine(new string('=', 60));
		}
	}
}
