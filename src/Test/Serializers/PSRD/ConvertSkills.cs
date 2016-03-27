using System.IO;
using NUnit.Framework;
using Pathfinder.Serializers;
using Pathfinder.Serializers.PSRD;

namespace Test.Serializers.PSRD
{
	[TestFixture]
	public class ConvertSkills
	{
		[Test]
		[Ignore]
		public void ConvertDirectory()
		{
			const string sourceDir = @"C:\Users\ethomas\Documents\GitHub\PSRD-Data\core_rulebook\skill";
			const string destinationDir = @"C:\Users\ethomas\Documents\GitHub\CharacterManager\resources\Skills";

			var sourceFiles = Directory.EnumerateFiles(sourceDir);
			foreach (var file in sourceFiles)
			{
				var contents = File.ReadAllText(file);
				var jsonSerializer = new SkillJsonSerializer();
				var result = jsonSerializer.Deserialize(contents);

				var xmlSerializer = new SkillXmlSerializer();
				var xmlSkill = xmlSerializer.Serialize(result);

				var newPath = Path.Combine(destinationDir, result.Name.Replace(" ", "_"));
				newPath = Path.ChangeExtension(newPath, "xml");
				File.WriteAllText(newPath, xmlSkill);
			}
		}
	}
}
