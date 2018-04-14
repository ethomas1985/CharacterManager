using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Serializers.Xml;
using PsrdParser.Serializers.PSRD;

namespace PsrdParser
{
	[TestFixture, Explicit]
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
			_CreateDestinationDirectory(destinationDir);

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
			_CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => _IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			_WriteDuplicatesToConsole(sourceFiles);

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

		private static bool _IsValidTraitFile(string pFilename)
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
			_CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => _IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			_WriteDuplicatesToConsole(sourceFiles);

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
			_CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => _IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			_WriteDuplicatesToConsole(sourceFiles);

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
			_CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => _IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			_WriteDuplicatesToConsole(sourceFiles);
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
			_CreateDestinationDirectory(destinationDir);

			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => _IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			_WriteDuplicatesToConsole(sourceFiles);


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

		[Test]
		[Ignore]
		public void ConvertItemsDirectory()
		{
			var sourceDir = Path.Combine(PsrdDataCore, "item");
			var sourceFiles =
				Directory
					.EnumerateFiles(sourceDir, "*.json", SearchOption.AllDirectories)
					.Where(x => _IsValidTraitFile(Path.GetFileNameWithoutExtension(x)))
					.OrderBy(x => x);

			var destinationDir = Path.Combine(MyData, "Items");
			_CreateDestinationDirectory(destinationDir);

			_WriteDuplicatesToConsole(sourceFiles);

			var itemJsonSerializer = new ItemJsonSerializer();
			var itemXmlSerializer = new ItemXmlSerializer();

			foreach (var file in sourceFiles)
			{
				string filename = Path.GetFileNameWithoutExtension(file);
				Console.WriteLine($"Item: {filename}");
				if ("bag_of_holding".Equals(filename))
				{
					/*
					 * The Bag of Holding is a special case as far as items go, as it
					 * has several tiers that change it's properties.
					 * 
					 * TODO: Parse bag_of_holding.json specially to generate a separate IItem for each set of properties.
					 */
					continue;
				}
				ConvertItem(itemJsonSerializer, itemXmlSerializer, file, destinationDir);
			}
		}

		private static void ConvertItem(ItemJsonSerializer pJsonSerializer, ItemXmlSerializer pXmlSerializer, string pFile, string pDestinationDir)
		{
			var contents = File.ReadAllText(pFile);
			var result = pJsonSerializer.Deserialize(contents);

			var xmlSkill = pXmlSerializer.Serialize(result);

			var newPath =
				Path.Combine(
							 pDestinationDir,
							 result.Name.Replace(" ", "_").Replace("\\", "_").Replace("/", "_"));
			newPath = Path.ChangeExtension(newPath, "xml");
			File.WriteAllText(newPath, xmlSkill);
		}

		private static void _CreateDestinationDirectory(string pDestinationDir)
		{
			if (!Directory.Exists(pDestinationDir))
			{
				Directory.CreateDirectory(pDestinationDir);
			}
		}

		private static void _WriteDuplicatesToConsole(IEnumerable<string> pSourceFiles)
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
