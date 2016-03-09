using System;
using System.Collections.Generic;
using Mono.Unix;
using Mono.Unix.Native;
using Microsoft.Owin.Hosting;
using Mono.Options;
using System.Reflection;

namespace Owin.SelfHost.Runner
{
	public static class ProgramRunner<TStartup>
	{
		public static void Run(IEnumerable<string> args)
		{
			var hasToShowHelp = false;
			var domain = "localhost";
			var port = 80;

			var optionSet = new OptionSet()
				.Add("?|help|h", "Prints out the options.", option => hasToShowHelp = option != null)
				.Add("d=|domain=", $"Domain - Specify the host to use for the service. Defaults to {domain}", option => domain = option)
				.Add("p=|port=", $"Port - Specify the port to use with the uri. Defaults to {port}", option => port = ParsePortOption(option));

			try
			{
				optionSet.Parse(args);
			}
			catch (OptionException)
			{
				ShowHelp("Error - usage is:", optionSet);
			}

			if (hasToShowHelp)
			{
				var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
				var usageMessage = $"{assemblyName}.exe [ /d[omain] VALUE ] [ /p[ort] VALUE ]";
				ShowHelp(usageMessage, optionSet);
			}

			System.Console.WriteLine("Starting web Server...");
			var serverUri = new UriBuilder("http", domain, port).Uri;
			StartServer(serverUri);
			System.Console.WriteLine("- Server running at {0}", serverUri);
			System.Console.WriteLine();

			WaitEndOfProcess();
		}

		private static int ParsePortOption(string option)
		{
			if (!IsPortValid(option))
			{
				throw new OptionException($"Port value [{option}] is invalid", "Port");
			}

			return Convert.ToInt32(option);
		}

		private static bool IsPortValid(string port)
		{
			int portResult;
			return Int32.TryParse(port, out portResult);
		}

		private static void ShowHelp(string message, OptionSet optionSet)
		{
			System.Console.Error.WriteLine(message);
			optionSet.WriteOptionDescriptions(System.Console.Error);
			Environment.Exit(-1);
		}

		private static void StartServer(Uri uri)
		{
			WebApp.Start<TStartup>(uri.ToString());
		}

		private static void WaitEndOfProcess()
		{
			var platformId = Environment.OSVersion.Platform;

			if (platformId == PlatformID.Unix || platformId == PlatformID.MacOSX)
			{
				// on mono, processes will usually run as daemons - this allows you to listen
				// for termination signals (ctrl+c, shutdown, etc) and finalize correctly
				UnixSignal.WaitAny(new[]
					{
						new UnixSignal(Signum.SIGINT),
						new UnixSignal(Signum.SIGTERM),
						new UnixSignal(Signum.SIGQUIT),
						new UnixSignal(Signum.SIGHUP)
					});
			}
			else
			{
				System.Console.WriteLine("Press Enter to quit");
				System.Console.ReadLine();
			}
		}
	}
}

