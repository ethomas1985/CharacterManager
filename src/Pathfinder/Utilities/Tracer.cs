﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Pathfinder.Utilities
{
	public static class Tracer
	{
		[Conditional("TRACE")]
		public static void Message([CallerMemberName] string pCallerName = null)
		{
            LogTo.Debug($"{DateTime.Now} - {pCallerName}");
		}

		[Conditional("TRACE")]
		public static void Message(string pMessage, [CallerMemberName] string pCallerName = null)
		{
			LogTo.Debug($"{DateTime.Now} - {pCallerName} :: {pMessage}");
		}
	}
}
