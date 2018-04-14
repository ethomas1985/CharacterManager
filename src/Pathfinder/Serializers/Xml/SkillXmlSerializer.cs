using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Xml
{
	internal class SkillXmlSerializer : ISerializer<ISkill, string>
	{
		public ISkill Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);
			return 
				new Skill(
					GetValue(xDocument, nameof(Skill.Name)),
					GetAbilityType(xDocument),
					GetBooleanValue(xDocument, nameof(Skill.TrainedOnly)),
					GetBooleanValue(xDocument, nameof(Skill.ArmorCheckPenalty)),
					GetValue(xDocument, nameof(Skill.Description)),
					GetValue(xDocument, nameof(Skill.Check)),
					GetValue(xDocument, nameof(Skill.Action)),
					GetValue(xDocument, nameof(Skill.TryAgain)),
					GetValue(xDocument, nameof(Skill.Special)),
					GetValue(xDocument, nameof(Skill.Restriction)),
					GetValue(xDocument, nameof(Skill.Untrained)));
		}

		private static string GetValue(XDocument xDocument, string pNodeName)
		{
			return
				xDocument
					.Descendants(pNodeName)
					.Select(x => x.Value)
					.FirstOrDefault();
		}

		private static bool GetBooleanValue(XDocument xDocument, string pNodeName)
		{
			var str = xDocument
				.Descendants(pNodeName)
				.Select(x => x.Value)
				.FirstOrDefault();
			bool value;
			return bool.TryParse(str, out value) && value;
		}

		private static AbilityType GetAbilityType(XDocument xDocument)
		{
			var nodeValue = 
				xDocument
					.Descendants(nameof(Skill.AbilityType))
					.Select(x => x.Value)
					.FirstOrDefault();

			AbilityType value;
			if (AbilityType.TryParse(nodeValue, out value))
			{
				return value;
			}
			throw new XmlException($"Invalid value for AbilityType; was {nodeValue}");
		}

		public string Serialize(ISkill pObject)
		{
			var xDocument =
				new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(nameof(Skill),
						new List<XElement>
						{
							new XElement(nameof(Skill.Name), pObject.Name),
							new XElement(nameof(Skill.AbilityType), pObject.AbilityType),
							new XElement(nameof(Skill.TrainedOnly), pObject.TrainedOnly),
							new XElement(nameof(Skill.ArmorCheckPenalty), pObject.ArmorCheckPenalty),
							new XElement(nameof(Skill.Description), pObject.Description),
							new XElement(nameof(Skill.Check), pObject.Check),
							new XElement(nameof(Skill.Action), pObject.Action),
							new XElement(nameof(Skill.TryAgain), pObject.TryAgain),
							new XElement(nameof(Skill.Special), pObject.Special),
							new XElement(nameof(Skill.Restriction), pObject.Restriction),
							new XElement(nameof(Skill.Untrained), pObject.Untrained),
						}.ToArray<object>()));

			return xDocument.ToString();
		}
	}
}
