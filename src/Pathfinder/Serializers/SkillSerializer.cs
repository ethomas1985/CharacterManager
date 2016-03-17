using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers
{
	internal class SkillSerializer : ISerializer<ISkill, string>
	{
		public ISkill Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			throw new System.NotImplementedException();
		}

		public string Serialize(ISkill pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
