using System;
using System.Collections.Generic;
using System.IO;
using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class SpellLibrary : AbstractLibrary<ISpell>
	{
		private readonly Lazy<IDictionary<string, ISpell>> _library =
			new Lazy<IDictionary<string, ISpell>>(
				() => new Dictionary<string, ISpell>());

		private readonly Lazy<IDictionary<string, IDictionary<string, ISpell>>> _classIndex =
			new Lazy<IDictionary<string, IDictionary<string, ISpell>>>(
				() => new Dictionary<string, IDictionary<string, ISpell>>());

		private readonly Lazy<IDictionary<string, IDictionary<int, IDictionary<string, ISpell>>>> _classLevelIndex =
			new Lazy<IDictionary<string, IDictionary<int, IDictionary<string, ISpell>>>>(
				() => new Dictionary<string, IDictionary<int, IDictionary<string, ISpell>>>());

		internal SpellLibrary(ISerializer<ISpell, string> pSerializer, string pLibraryDirectory)
			: base(pSerializer, pLibraryDirectory)
		{
		}

		private IDictionary<string, IDictionary<string, ISpell>> ClassIndex => _classIndex.Value;
		private IDictionary<string, IDictionary<int, IDictionary<string, ISpell>>> ClassLevelIndex => _classLevelIndex.Value;

		/// <summary>
		/// Returns the Level Index for the given Class's Spell Library.
		/// </summary>
		/// <param name="pClass"></param>
		/// <returns></returns>
		public IDictionary<int, IDictionary<string, ISpell>> this[IClass pClass]
		{
			get
			{
				IDictionary<int, IDictionary<string, ISpell>> value;
				if (ClassLevelIndex.TryGetValue(pClass.Name, out value))
				{
					return value;
				}

				throw new KeyNotFoundException($"Key := \"{pClass.Name}\"");
			}
		}

		protected override void LoadFile(ISerializer<ISpell, string> pSerializer, string pFile)
		{
			var xml = File.ReadAllText(pFile);
			var spell = pSerializer.Deserialize(xml);

			Library[spell.Name] = spell;

			foreach( var keyValue in spell.LevelRequirements)
			{
				var classRequirement = keyValue.Key;
				var levelRequirement = keyValue.Value;

				AddToClassIndex(classRequirement, spell);
				AddToClassLevelIndex(classRequirement, levelRequirement, spell);
			}
		}

		private void AddToClassIndex(string pClassRequirement, ISpell pSpell)
		{
			IDictionary<string, ISpell> outClassIndex;
			if (!ClassIndex.TryGetValue(pClassRequirement, out outClassIndex))
			{
				ClassIndex[pClassRequirement] = new Dictionary<string, ISpell>();
			}
			ClassIndex[pClassRequirement][pSpell.Name] = pSpell;
		}

		private void AddToClassLevelIndex(string pClassRequirement, int pLevelRequirement, ISpell pSpell)
		{
			IDictionary<int, IDictionary<string, ISpell>> outClassIndex;
			if (!ClassLevelIndex.TryGetValue(pClassRequirement, out outClassIndex))
			{
				ClassLevelIndex[pClassRequirement] = new Dictionary<int, IDictionary<string, ISpell>>();
			}

			IDictionary<string, ISpell> outLevelIndex;
			if (!ClassLevelIndex[pClassRequirement].TryGetValue(pLevelRequirement, out outLevelIndex))
			{
				ClassLevelIndex[pClassRequirement][pLevelRequirement] = new Dictionary<string, ISpell>();
			}

			ClassLevelIndex[pClassRequirement][pLevelRequirement][pSpell.Name] = pSpell;
		}

	}
}
