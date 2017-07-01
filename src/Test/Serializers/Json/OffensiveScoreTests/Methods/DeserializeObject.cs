using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Serializers.Json.OffensiveScoreTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void NotSupported()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IOffensiveScore>("{}"), 
				Throws.Exception
					.TypeOf<NotImplementedException>()
					.With.Message.EqualTo($"Deserializing {nameof(IOffensiveScore)} types is not supported."));
		}
	}
}
