using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;

namespace Pathfinder.Test.Serializers.Json.DefenseScoreTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void NotSupported()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IDefenseScore>("{}"),
				Throws.Exception
					.TypeOf<NotImplementedException>()
					.With.Message.EqualTo($"Deserialization of {nameof(IDefenseScore)} is not supported."));
		}
	}
}
