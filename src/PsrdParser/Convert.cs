using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Library;
using Pathfinder.Properties;
using Pathfinder.Serializers;
using Pathfinder.Serializers.Xml;
using PsrdParser.Serializers.PSRD;

namespace PsrdParser
{
	[TestFixture]
	public class Convert
	{
		private static readonly string MyDocuments =
			Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		private static readonly string PsrdData =
			Path.Combine(MyDocuments, "GitHub", "PSRD-Data");
		private static readonly string PsrdDataCore =
			Path.Combine(PsrdData, "core_rulebook");
		private static readonly string MyData =
			Path.Combine(MyDocuments, "GitHub", "CharacterManager", "resources");


		[Test]
		[Ignore]
		public void ConvertSkillsDirectory()
		{

			var sourceDir = Path.Combine(PsrdDataCore, "skill");
			var destinationDir = Path.Combine(MyData, "Skills");
			CreateDestinationDirectory(destinationDir);

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

		[Test]
		[Ignore]
		public void ConvertRacialTraitsDirectory()
		{
			var sourceDir = Path.Combine(PsrdDataCore, "racial_trait");
			var destinationDir = Path.Combine(MyData, "Traits");
			CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			WriteDuplicatesToConsole(sourceFiles);

			foreach (var file in sourceFiles)
			{
				var contents = File.ReadAllText(file);
				var jsonSerializer = new TraitJsonSerializer();
				var result = jsonSerializer.Deserialize(contents);

				var xmlSerializer = new TraitXmlSerializer();
				var xmlSkill = xmlSerializer.Serialize(result);

				var newPath = Path.Combine(destinationDir, result.Name.Replace(" ", "_"));
				newPath = Path.ChangeExtension(newPath, "xml");
				File.WriteAllText(newPath, xmlSkill);
			}
		}

		private static bool IsValidTraitFile(string pFilename)
		{
			var textInfo = new CultureInfo("en-US", false).TextInfo;
			Size outSize;
			var isSize = Size.TryParse(textInfo.ToTitleCase(pFilename), out outSize);

			return
				!pFilename.Contains("speed")
				&& !pFilename.Contains("attributes")
				&& !pFilename.Contains("weapon_familiarity")
				&& !pFilename.Contains("languages")
				&& !isSize;
		}

		[Test]
		[Ignore]
		public void ConvertClassDirectory()
		{
			var sourceDir = Path.Combine(PsrdDataCore, "class", "core");
			var destinationDir = Path.Combine(MyData, "Classes");
			CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			WriteDuplicatesToConsole(sourceFiles);

			foreach (var file in sourceFiles)
			{
				var contents = File.ReadAllText(file);
				var jsonSerializer = new ClassJsonSerializer();
				var result = jsonSerializer.Deserialize(contents);

				var xmlSerializer = new ClassXmlSerializer();
				var xmlSkill = xmlSerializer.Serialize(result);

				var newPath = Path.Combine(destinationDir, result.Name.Replace(" ", "_"));
				newPath = Path.ChangeExtension(newPath, "xml");
				File.WriteAllText(newPath, xmlSkill);
			}
		}

		[Test]
		[Ignore]
		public void ConvertFeaturesDirectory()
		{
			var sourceDir = Path.Combine(PsrdDataCore, "class", "core");
			var destinationDir = Path.Combine(MyData, "ClassFeatures");
			CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			WriteDuplicatesToConsole(sourceFiles);

			foreach (var file in sourceFiles)
			{
				var contents = File.ReadAllText(file);
				var jsonSerializer = new FeatureJsonSerializer();
				var results = jsonSerializer.Deserialize(contents);

				foreach (var result in results)
				{
					var xmlSerializer = new FeatureXmlSerializer();
					var xmlSkill = xmlSerializer.Serialize(result);

					var newPath = Path.Combine(destinationDir, result.Name.Replace(" ", "_").Replace(":", ""));
					newPath = Path.ChangeExtension(newPath, "xml");
					File.WriteAllText(newPath, xmlSkill);
				}
			}
		}

		[Test]
		[Ignore]
		public void ConvertFeatsDirectory()
		{
			var sourceDir = Path.Combine(PsrdDataCore, "feat");
			var destinationDir = Path.Combine(MyData, "Feats");
			CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			WriteDuplicatesToConsole(sourceFiles);
			foreach (var file in sourceFiles)
			{
				var contents = File.ReadAllText(file);
				var jsonSerializer = new FeatJsonSerializer();
				var result = jsonSerializer.Deserialize(contents);

				var xmlSerializer = new FeatXmlSerializer();
				var xmlSkill = xmlSerializer.Serialize(result);

				var newPath = Path.Combine(destinationDir, result.Name.Replace(" ", "_"));
				newPath = Path.ChangeExtension(newPath, "xml");
				File.WriteAllText(newPath, xmlSkill);
			}
		}

		[Test]
		[Ignore]
		public void ConvertSpellsDirectory()
		{
			var sourceDir = Path.Combine(PsrdDataCore, "spell");
			var destinationDir = Path.Combine(MyData, "Spells");
			CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			WriteDuplicatesToConsole(sourceFiles);


			foreach (var file in sourceFiles)
			{
				Console.WriteLine($"Spell: {Path.GetFileNameWithoutExtension(file)}");
				var contents = File.ReadAllText(file);
				var jsonSerializer = new SpellJsonSerializer();
				var result = jsonSerializer.Deserialize(contents);

				var xmlSerializer = new SpellXmlSerializer();
				var xmlSkill = xmlSerializer.Serialize(result);

				var newPath = 
					Path.Combine(
						destinationDir, 
						result.Name.Replace(" ", "_").Replace("\\", "_").Replace("/", "_"));
				newPath = Path.ChangeExtension(newPath, "xml");
				File.WriteAllText(newPath, xmlSkill);
			}
		}

		private static void CreateDestinationDirectory(string pDestinationDir)
		{
			if (!Directory.Exists(pDestinationDir))
			{
				Directory.CreateDirectory(pDestinationDir);
			}
		}

		private static void WriteDuplicatesToConsole(IEnumerable<string> pSourceFiles)
		{
			var dups =
				pSourceFiles
					.GroupBy(Path.GetFileNameWithoutExtension)
					.Select(x => new { x.Key, Count = x.Count() })
					.Where(x => x.Count > 1)
					.OrderByDescending(x => x.Count).ToList();
			Console.WriteLine($"Total Duplicates: {dups.Count}");
			foreach (var file in dups)
			{
				Console.WriteLine($"{file.Key}\t\t{file.Count}");
			}
		}
	}
}
