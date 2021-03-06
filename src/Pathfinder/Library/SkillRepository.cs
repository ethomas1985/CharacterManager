﻿using System;
using Pathfinder.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
	internal class SkillRepository : AbstractFilesystemRepository<ISkill>
	{
		internal SkillRepository(ISerializer<ISkill, string> pSerializer, string pLibraryDirectory)
			: base(pSerializer, pLibraryDirectory)
		{
		}

		public override ISkill this[string pKey]
		{
			get
			{
				ISkill value;
				if (Library.TryGetValue(pKey, out value))
				{
					return value;
				}

				// Special Logic for Craft, Knowledge, Profession, and Perform skills.
				var regex = new Regex(@"([\w ]+) \(([\w ]+)\)");
				var match = regex.Match(pKey);
				if (match.Success)
				{
					var baseName = match.Groups[1].Value;
					var specialization = match.Groups[2].Value;
					if (Library.TryGetValue(baseName, out value))
					{
						return CreateSpecialization(value, specialization);
					}
				}

				throw new KeyNotFoundException($"Key := \"{pKey}\"");
			}
		}

		private Skill CreateSpecialization(ISkill value, string specialization)
		{
			var skill = 
				new Skill(
					$"{value.Name} ({specialization})",
					value.AbilityType,
					value.TrainedOnly,
					value.ArmorCheckPenalty,
					value.Description,
					value.Check,
					value.Action,
					value.TryAgain,
					value.Special,
					value.Restriction,
					value.Untrained);

			Library.TryAdd(skill.Name, skill);

			return skill;
		}
	}
}
