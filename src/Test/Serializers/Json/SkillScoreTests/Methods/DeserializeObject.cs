using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Serializers.Json.SkillScoreTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void NotSupported()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<ISkillScore>("{}"),
				Throws.Exception
						.TypeOf<NotImplementedException>()
						.With.Message.EqualTo($"Deserializing {nameof(ISkillScore)} types is not supported."));
		}
	}
}
