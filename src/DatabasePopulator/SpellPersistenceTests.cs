using System.Linq;
using NUnit.Framework;
using Pathfinder;
using Pathfinder.Library;
using Pathfinder.Repository;
using Pathfinder.Serializers.Xml;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace DatabasePopulator
{
	[TestFixture, Explicit]
	public class SpellPersistenceTests
	{
		private const string RESOURCES_DIRECTORY =
			@"C:\Users\thean\Documents\GitHub\CharacterManager\resources\Spells\";

		[SetUp]
		public void TestSetUp()
		{
			PathfinderConfiguration.InitializePersistenceLayer();
		}

		[Test]
		public void TestInsert()
		{
			var spellSerializer = new SpellXmlSerializer();
			var spellRepository = new SpellFileSystemRepository(spellSerializer, RESOURCES_DIRECTORY);
			var spell = spellRepository.Values.First();

			var spellStore = new SpellMongoRepository();

			LogTo.Info("{Class}.{Method}|Loaded Spell \"{Name}\" from file system.",
					   nameof(SpellPersistenceTests), nameof(TestInsert), spell.Name);

			LogTo.Info("{Class}.{Method}|Looking at \"{spell.Name}\"",
					   nameof(SpellPersistenceTests), nameof(TestInsert), spell.Name);
			var storedSpell = spellStore.Get(spell.Name);
			if (storedSpell == null)
			{
				LogTo.Info("{Class}.{Method}|Saving spell \"{spell.Name}\"",
						   nameof(SpellPersistenceTests), nameof(TestInsert), spell.Name);
				spellStore.Insert(spell);
			}

			Assert.That(spellStore.Get(spell.Name), Is.Not.Null);
		}

		[Test, Explicit]
		public void UpdateMongoDatabase()
		{
			var spellSerializer = new SpellXmlSerializer();
			var spellRepository = new SpellFileSystemRepository(spellSerializer, RESOURCES_DIRECTORY);
			var spells = spellRepository.Values.ToList();

			var spellStore = new SpellMongoRepository();
			
			LogTo.Info("{Class}.{Method}|Loaded {Count} Spells from file system.",
					   nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spells.Count);

			foreach (var spell in spells)
			{
				LogTo.Info("{Class}.{Method}|Looking at \"{Name}\"",
						   nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spell.Name);
				var storedSpell = spellStore.Get(spell.Name);
				if (storedSpell == null)
				{
					LogTo.Info("{Class}.{Method}|Saving spell \"{Name}\"",
							   nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spell.Name);
					spellStore.Insert(spell);
				} else
				{
					LogTo.Info("{Class}.{Method}|Updating spell \"{Name}\"",
							   nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spell.Name);
					spellStore.Update(spell);
				}
			}

			Assert.That(spellStore.GetAll().Count(), Is.EqualTo(622));
		}
	}
}
