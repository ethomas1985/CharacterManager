using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers
{
	internal class ClassXmlSerializer : ISerializer<IClass, string>
	{
		private static readonly Regex Pattern = new Regex(@"d(\d+)");

		public IClass Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);

			return new Class(
				GetName(xDocument),
				GetAlignments(xDocument),
				GetHitDie(xDocument),
				GetSkillSet(xDocument),
				GetClassLevels(xDocument),
				GetFeatures(xDocument.Descendants(nameof(IClass.Features)).FirstOrDefault())
				);
		}

		private IEnumerable<IClassLevel> GetClassLevels(XContainer pXDocument)
		{
			var classLevels =
				new HashSet<IClassLevel>(
					pXDocument
						.Descendants(nameof(ClassLevel))
						.Select(
							    x => new ClassLevel(
									GetLevel(x),
									GetBaseAttackBonuses(x),
									GetFortitude(x),
									GetReflex(x),
									GetWill(x),
									GetFeatures(x),
									GetSpellsPerDay(x),
									null, //spellsKnown
									GetSpellsByLevel(x)
									)));
			return classLevels;
		}

		private static string GetName(XContainer pXDocument)
		{
			return pXDocument
				.Descendants(nameof(IClass.Name))
				.Select(x => x.Value)
				.FirstOrDefault();
		}
		private HashSet<string> GetSkillSet(XContainer pXDocument)
		{
			var skills =
				new HashSet<string>(
					pXDocument
						.Descendants(nameof(Skill))
						.Select(x => x.Value));
			return skills;
		}
		private static int GetLevel(XContainer pElement)
		{
			return pElement.Descendants(nameof(ClassLevel.Level)).Select(y => AsInt(y.Value)).FirstOrDefault();
		}
		private static IEnumerable<int> GetBaseAttackBonuses(XContainer pElement)
		{
			return pElement.Descendants(nameof(ClassLevel.BaseAttackBonus)).Descendants().Select(y => AsInt(y.Value));
		}
		private static int GetFortitude(XContainer pElement)
		{
			return pElement.Descendants(nameof(ClassLevel.Fortitude)).Select(y => AsInt(y.Value)).FirstOrDefault();
		}
		private static int GetReflex(XContainer pElement)
		{
			return pElement.Descendants(nameof(ClassLevel.Reflex)).Select(y => AsInt(y.Value)).FirstOrDefault();
		}
		private static int GetWill(XContainer pElement)
		{
			return pElement.Descendants(nameof(ClassLevel.Will)).Select(y => AsInt(y.Value)).FirstOrDefault();
		}
		private IEnumerable<string> GetFeatures(XContainer pElement)
		{
			return pElement.Descendants(nameof(Feature)).Select(y => y.Value);
		}
		private static Dictionary<int, int> GetSpellsPerDay(XElement pElement)
		{
			return pElement.Descendants(nameof(ClassLevel.SpellsPerDay)).ToDictionary(k => k.Name.LocalName.WrittenToInteger(), v => AsInt(pElement.Value));
		}
		private Dictionary<int, IEnumerable<string>> GetSpellsByLevel(XContainer pElement)
		{
			return pElement.Descendants(nameof(Spell)).GroupBy(k => k.Name.LocalName.WrittenToInteger()).ToDictionary(k => k.Key, v => v.Select(y => y.Value));
		}

		private static Die GetHitDie(XContainer pDocument)
		{
			var hitDie =
				pDocument
					.Descendants(nameof(Class.HitDie))
					.Select(x =>
							{
								var match = Pattern.Match(x.Value);
								return match.Success ? match.Groups[1].Value : null;
							})
					.Where(x => x != null)
					.Select(AsInt)
					.Select(x => new Die(x))
					.FirstOrDefault();
			return hitDie;
		}

		private static int AsInt(string pValue)
		{
			int value;
			return int.TryParse(pValue, out value) ? value : 0;
		}

		private static HashSet<Alignment> GetAlignments(XContainer pDocument)
		{
			var alignmentsStrings =
				pDocument
					.Descendants(nameof(Alignment))
					.Select(x => x.Value);
			var alignments = new HashSet<Alignment>();
			foreach (var alignmentString in alignmentsStrings)
			{
				Alignment value;
				if (!System.Enum.TryParse(alignmentString, out value))
				{
					throw new InvalidCastException($"Invalid Alignment Value: {alignmentString}");
				}

				if (!alignments.Contains(value))
				{
					alignments.Add(value);
				}
			}
			return alignments;
		}

		public string Serialize(IClass pObject)
		{
			var xDocument =
				new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(nameof(Class),
						new List<XElement>
						{
							new XElement(nameof(Class.Name), pObject.Name),
							new XElement(
								nameof(Class.Alignments),
								pObject.Alignments.Select(
									x => new XElement(nameof(Alignment), x))),
							new XElement(nameof(Class.HitDie), $"d{pObject.HitDie.Faces}"),
							new XElement(
								nameof(Class.Skills),
								pObject.Skills.Select(x => new XElement(nameof(Skill), x))),
							new XElement(
								nameof(Class.ClassLevels),
								pObject.ClassLevels.Select(
									classLevel => new XElement(
										nameof(ClassLevel),
										new List<XElement>
										{
											new XElement(nameof(ClassLevel.Level), classLevel.Level),
											new XElement(
												nameof(ClassLevel.BaseAttackBonus),
												classLevel.BaseAttackBonus.Select(
														(x, i) => new XElement((i+1).IntegerToWritten(), x))),
											new XElement(nameof(ClassLevel.Fortitude), classLevel.Fortitude),
											new XElement(nameof(ClassLevel.Reflex), classLevel.Reflex),
											new XElement(nameof(ClassLevel.Will), classLevel.Will),
											new XElement(
												nameof(ClassLevel.Specials),
												classLevel.Specials.Select(
														x => new XElement(nameof(Feature), x))),
											new XElement(
												nameof(ClassLevel.SpellsPerDay),
												classLevel.SpellsPerDay?.Select(
													x => new XElement(x.Key.IntegerToWritten(), x.Value))),
											new XElement(
												nameof(ClassLevel.SpellsKnown),
												classLevel.SpellsKnown?.Select(
													x => new XElement(x.Key.IntegerToWritten(), x.Value))),
											new XElement(
												nameof(ClassLevel.Spells),
												classLevel.Spells?.Select(
														pair => new XElement(
															pair.Key.IntegerToWritten(),
															pair.Value.Select(
																x => new XElement(nameof(Spell), x))))),
										}
										))),
							new XElement(
								nameof(Class.Features),
								pObject.Features.Select(x => new XElement(nameof(Feature), x)))
						}.ToArray<object>()));

			return xDocument.ToString();
		}
	}
}
