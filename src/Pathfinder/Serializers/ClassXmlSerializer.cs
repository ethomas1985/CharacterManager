using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers
{
	internal class ClassXmlSerializer : ISerializer<IClass, string>
	{
		public IClass Deserialize(string pValue)
		{
			throw new System.NotImplementedException();
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
								pObject.Skills.Select(x => new XElement(nameof(Skill), x.Name))),
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
														x => new XElement(nameof(Feature), x.Name))),
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
																x => new XElement(nameof(Spell), x.Name))))),
										}
										))),
							new XElement(
								nameof(Class.Features),
								pObject.Features.Select(x => new XElement(nameof(Feature), x.Name)))
						}.ToArray<object>()));

			return xDocument.ToString();
		}
	}
}
