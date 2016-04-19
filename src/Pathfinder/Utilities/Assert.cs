using System;

namespace Pathfinder.Utilities
{
	internal static class Assert
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pCondition"></param>
		/// <param name="pMessage"></param>
		/// <param name="pParams"></param>
		/// <exception cref="Exception" />
		public static void IsTrue(bool pCondition, string pMessage, params object[] pParams)
		{
			if (!pCondition)
			{
				throw new Exception(string.Format(pMessage, pParams));
			}
		}

		/// <summary>
		/// Throws an ArgumentNullException if the given value is null.
		/// </summary>
		/// <param name="pParameter"></param>
		/// <param name="pParameterName"></param>
		/// <exception cref="ArgumentNullException" />
		public static void ArgumentNotNull(object pParameter, string pParameterName)
		{
			if (pParameter == null)
			{
				throw new ArgumentNullException(pParameterName);
			}
		}

		/// <summary>
		/// Throws an ArgumentNullException if the given string is null or Empty.
		/// </summary>
		/// <param name="pParameter"></param>
		/// <param name="pParameterName"></param>
		/// <exception cref="ArgumentNullException" />
		public static void ArgumentIsNotEmpty(string pParameter, string pParameterName)
		{
			if (string.IsNullOrEmpty(pParameter))
			{
				throw new ArgumentNullException(pParameterName);
			}
		}

		public static void AreEqual<T>(T pExpected, T pValue)
		{
			if (!pExpected.Equals(pValue))
			{
				throw new Exception(
					$"Constraint Violation!{Environment.NewLine}" +
					$"Expected Value: {pExpected}{Environment.NewLine}" +
					$"Given Value: {pValue}{Environment.NewLine}");
			}
		}
	}
}