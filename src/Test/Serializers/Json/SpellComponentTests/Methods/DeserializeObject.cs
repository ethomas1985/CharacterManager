﻿using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.SpellComponentTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresComponentType()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<ISpellComponent>("{}"),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(ISpellComponent.ComponentType)}"));
		}

		[Test]
		public void AllowsDescriptionToBeOptional()
		{
			const ComponentType componentType = ComponentType.DivineFocus;
			var value = $"{{ \"{nameof(ISpellComponent.ComponentType)}\": \"{componentType}\"}}";
            var result = JsonConvert.DeserializeObject<ISpellComponent>(value);
            var expected = new SpellComponent(componentType);

            Assert.That(result, Is.EqualTo(expected));
        }

		[Test]
		public void Expected()
		{
			const ComponentType componentType = ComponentType.DivineFocus;
			const string description = "Testing Spell Component Description";

			var value = $"{{" +
						$"\"{nameof(ISpellComponent.ComponentType)}\": \"{componentType}\"," +
						$"\"{nameof(ISpellComponent.Description)}\": \"{description}\"" +
						$"}}";

			var result = JsonConvert.DeserializeObject<ISpellComponent>(value);
			var expected = new SpellComponent(componentType, description);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
