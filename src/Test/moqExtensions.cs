﻿using System.Reflection;
using Moq.Language;
using Moq.Language.Flow;

namespace Pathfinder.Test
{
	/// <summary>
	/// From Stackoverflow: https://stackoverflow.com/questions/1068095/assigning-out-ref-parameters-in-moq
	/// </summary>
	/// <remarks>
	/// From https://stackoverflow.com/a/19598345
	/// </remarks>
	public static class MoqExtensions
	{
		public delegate void OutAction<TOut>(out TOut outVal);
		public delegate void OutAction<in T1, TOut>(T1 arg1, out TOut outVal);

		public static IReturnsThrows<TMock, TReturn> OutCallback<TMock, TReturn, TOut>(this ICallback<TMock, TReturn> mock, OutAction<TOut> action)
			where TMock : class
		{
			return OutCallbackInternal(mock, action);
		}

		public static IReturnsThrows<TMock, TReturn> OutCallback<TMock, TReturn, T1, TOut>(this ICallback<TMock, TReturn> mock, OutAction<T1, TOut> action)
			where TMock : class
		{
			return OutCallbackInternal(mock, action);
		}

		private static IReturnsThrows<TMock, TReturn> OutCallbackInternal<TMock, TReturn>(ICallback<TMock, TReturn> mock, object action)
			where TMock : class
		{
			mock.GetType()
				.Assembly.GetType("Moq.MethodCall")
				.InvokeMember("SetCallbackWithArguments", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, mock,
							  new[] { action });
			return mock as IReturnsThrows<TMock, TReturn>;
		}
	}
}
