using System;
using System.Collections.Generic;
using NUnit.Framework;
using Pathfinder.Model;
using Pathfinder.Serializers.Xml;

namespace Pathfinder.Test.Serializers.Xml
{
	[TestFixture]
	public class TraitXmlSerializerTests
	{
		private const string NAME = "Trait";
		private const string TEXT = "Description";
		private const string PROPERTY = "Property";

		private readonly Trait _trait =
			new Trait(
				NAME,
				TEXT,
				new Dictionary<string, int>
				{
					[PROPERTY] = 2
				});

		private readonly string _xmlString =
			//$"<?xml version=\"1.0\" encoding=\"utf-8\" ?>{Environment.NewLine}"+
			$"<Trait>{Environment.NewLine}"+
			$"  <Name>{NAME}</Name>{Environment.NewLine}"+
			$"  <Text>{TEXT}</Text>{Environment.NewLine}"+
			$"  <PropertyModifiers>{Environment.NewLine}"+
			$"    <{PROPERTY}>2</{PROPERTY}>{Environment.NewLine}"+
			$"  </PropertyModifiers>{Environment.NewLine}"+
			 "</Trait>";

		[TestFixture]
		public class SerializeMethod : TraitXmlSerializerTests
		{
			[Test]
			public void Expected()
			{
				var serializer = new TraitXmlSerializer();
				var xml = serializer.Serialize(_trait);

				Assert.AreEqual(_xmlString, xml);
			}
		}

		[TestFixture]
		public class DeserializeMethod : TraitXmlSerializerTests
		{
			[Test]
			public void ThrowsForNullString()
			{
				var serializer = new TraitXmlSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(null));
			}

			[Test]
			public void ThrowsForEmptyString()
			{
				var serializer = new TraitXmlSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(string.Empty));
			}

			[Test]
			public void NotNull()
			{
				var serializer = new TraitXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.NotNull(result);
			}

			[Test]
			public void SetName()
			{
				var serializer = new TraitXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(NAME, result.Name);
			}

			[Test]
			public void SetText()
			{
				var serializer = new TraitXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(TEXT, result.Text);
			}

			[Test]
			public void SetPropertyModifiers()
			{
				var serializer = new TraitXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(1, result.PropertyModifiers.Count);
			}
		}
	}
}
