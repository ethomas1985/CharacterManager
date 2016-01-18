namespace Pathfinder.Interface
{
	public interface IClass
	{
		int BaseAttackBonus { get; }
		int Fortitude { get; }
		int Reflex { get; }
		int Will { get; }
		int Skills { get; }
		int FCSSkills { get; }
		int FCHp { get; }
	}
}