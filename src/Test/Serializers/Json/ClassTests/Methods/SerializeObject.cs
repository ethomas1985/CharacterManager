using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.ClassTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		private static IClass CreateTestClass()
		{
			return new Class(
				"Test Name",
				new HashSet<Alignment> { Alignment.Neutral },
				new Die(6),
				1,
				new HashSet<string> { "Testing Skill Name" },
				new IClassLevel[] { },
				new List<string>());
		}

		[Test]
		public void Success()
		{
			var @class = CreateTestClass();

			Assert.That(
				() => JsonConvert.SerializeObject(@class),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var @class = CreateTestClass();
			var actual = JsonConvert.SerializeObject(@class);

			var expectedAlignments = string.Join(",", @class.Alignments.Select(x => $"\"{x}\""));
			var expectedSkills = string.Join(",", @class.Skills.Select(x => $"\"{x}\""));
			var expectedFeatures = string.Join(",", @class.Features.Select(x => $"\"{x}\""));

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IClass.Name)}\":\"{@class.Name}\",")
					.Append($"\"{nameof(IClass.Alignments)}\":[{expectedAlignments}],")
					.Append($"\"{nameof(IClass.HitDie)}\":\"d{@class.HitDie.Faces}\",")
					.Append($"\"{nameof(IClass.SkillAddend)}\":{@class.SkillAddend},")
					.Append($"\"{nameof(IClass.Skills)}\":[{expectedSkills}],")
					.Append($"\"{nameof(IClass.ClassLevels)}\":[],")
					.Append($"\"{nameof(IClass.Features)}\":[{expectedFeatures}]")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
