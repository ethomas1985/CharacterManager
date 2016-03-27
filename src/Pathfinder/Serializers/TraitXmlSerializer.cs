using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers
{
	internal class TraitXmlSerializer : ISerializer<ITrait, string>
	{
		public ITrait Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);

			var name =
				xDocument
					.Descendants(nameof(ITrait.Name))
					.Select(x => x.Value)
					.FirstOrDefault();
			var text =
				xDocument
					.Descendants(nameof(ITrait.Text))
					.Select(x => x.Value)
					.FirstOrDefault();
			var propertyModifiers =
				xDocument
					.Descendants(nameof(ITrait.PropertyModifiers))
					.Descendants()
					.ToDictionary(x => x.Name.LocalName, GetPropertyModifierValue);

			return new Trait(name, text, propertyModifiers);
		}

		private static int GetPropertyModifierValue(XElement pXElement)
		{
			int value;
			return int.TryParse(pXElement.Value, out value) ? value : 0;
		}

		public string Serialize(ITrait pObject)
		{
			var xDocument =
				new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(nameof(Trait),
						new List<XElement>
						{
							new XElement(nameof(ITrait.Name), pObject.Name),
							new XElement(nameof(ITrait.Text), pObject.Text),
							new XElement(
								nameof(ITrait.PropertyModifiers),
								pObject.PropertyModifiers
									.Select(x => new XElement(x.Key.ToString(), x.Value))
									.ToArray<object>())
			}.ToArray<object>()));

			return xDocument.ToString();
		}
	}
}
