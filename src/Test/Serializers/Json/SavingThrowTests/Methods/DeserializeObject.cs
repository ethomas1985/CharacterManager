using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;

namespace Pathfinder.Test.Serializers.Json.SavingThrowTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void NotSupported()
		{
			Assert.That(
					    () => JsonConvert.DeserializeObject<ISavingThrow>("{}"),
					    Throws.Exception
							  .TypeOf<NotImplementedException>()
							  .With.Message.EqualTo($"Deserializing {nameof(ISavingThrow)} types is not supported."));
		}
	}
}
