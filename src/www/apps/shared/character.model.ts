namespace app.character.model {
	export interface Map<TValueType> {
		[K: string]: TValueType;
	}

	export enum Alignment {
		Neutral,
		LawfulNeutral,
		ChaoticNeutral,
		LawfulGood,
		NeutralGood,
		ChaoticGood,
		LawfulEvil,
		NeutralEvil,
		ChaoticEvil
	}

	export enum Size {
		Medium,
		Small,
		Tiny,
		Diminutive,
		Fine,
		Large,
		Huge,
		Gargantuan,
		Colossal
	}

	export enum AbilityType {
		Strength,
		Dexterity,
		Constitution,
		Intelligence,
		Wisdom,
		Charisma
	}

	export enum ComponentType {
		Verbal,
		Somatic,
		Material,
		Focus,
		DivineFocus
	}

	export enum DefensiveType {
		ArmorClass,
		FlatFooted,
		Touch,
		CombatManeuverDefense
	}

	export enum EffectType {
		Untyped,
		Alchemical,
		Armor,
		Circumstance,
		Competence,
		Deflection,
		Dodge,
		Enhancement,
		Insight,
		Luck,
		Morale,
		Resistance,
		SacredOrProfane,
		Size
	}

	export enum FeatType {
		General,
		Teamwork,
		Optional,
		Combat,
		Metamagic
	}

	export enum FeatureAbilityType {
		Normal,
		Extraordinary,
		Supernatural,
		SpellLike
	}

	export enum Gender {
		Female,
		Male
	}

	export enum MagicDescriptor {
		Universal,
		Acid,
		Air,
		Chaotic,
		Cold,
		Darkness,
		Death,
		Earth,
		Electricity,
		Evil,
		Fear,
		Fire,
		Force,
		Good,
		LanguageDependent,
		Lawful,
		Light,
		MindAffecting,
		Sonic,
		Water
	}

	export enum MagicSchool {
		Universal,
		Abjuration,
		Conjuration,
		Divination,
		Enchantment,
		Evocation,
		Illusion,
		Necromancy,
		Transmutation
	}

	export enum MagicSubSchool {
		Universal,
		None,
		Calling,
		Creation,
		Healing,
		Summoning,
		Teleportation,
		Scrying,
		Charm,
		Compulsion,
		Figment,
		Glamer,
		Pattern,
		Phantasm,
		Shadow,
		Polymorph
	}

	export enum OffensiveType {
		Melee,
		Ranged,
		CombatManeuverBonus
	}

	export enum SavingThrowType {
		Fortitude,
		Reflex,
		Will
	}

	export interface ICharacter extends INamed {
		Age: number;
		Alignment: Alignment;
		Classes: ICharacterClass[];
		Deity: IDeity;
		Eyes: string;
		Gender: Gender;
		Hair: string;
		Height: string;
		Homeland: string;
		Race: IRace;
		BaseSize: Size;
		Size: Size;
		Weight: string;
		Languages: ILanguage[];
		MaxHealthPoints: number;
		HealthPoints: number;
		Damage: number;
		BaseSpeed: number;
		ArmoredSpeed: number;
		Initiative: number;
		// Ability Scores
		Strength: IAbilityScore;
		Dexterity: IAbilityScore;
		Constitution: IAbilityScore;
		Intelligence: IAbilityScore;
		Wisdom: IAbilityScore;
		Charisma: IAbilityScore;
		// Saving Throws
		Fortitude: ISavingThrow;
		Reflex: ISavingThrow;
		Will: ISavingThrow;
		// Defensive Scores
		ArmorClass: IDefenseScore;
		FlatFooted: IDefenseScore;
		Touch: IDefenseScore;
		CombatManeuverDefense: IDefenseScore;
		BaseAttackBonus: number;
		BaseFortitude: number;
		BaseReflex: number;
		BaseWill: number;
		Melee: IOffensiveScore;
		Ranged: IOffensiveScore;
		CombatManeuverBonus: IOffensiveScore;
		ExperiencePoints: number;
		// Experience: IExperience
		HitDice: IDie[];
		Traits: ITrait[];
		Feats: IFeat[];
		SkillScores: ISkillScore[];
		// Weapons: IWeapon[]
		Purse: IPurse;
		// Inventory: IInventory
		EquipedArmor: IArmor[];
		Effects: IEffect[];
	}

	export interface IAbilityScore {
		Type: AbilityType;
		Score: number;
		Modifier: number;
		Base: number;
		Enhanced: number;
		Inherent: number;
		Temporary: number;
		Penalty: number;
	}

	export interface IArmor extends IItem {
		IsShield: boolean;
		Bonus: number;
		MaximumDexterityBonus: number;
		ArmorCheckPenalty: number;
		ArcaneSpellFailureChance: number;
		Speed: number;
		Weight: number;
	}

	export interface ICharacterClass {
		Class: IClass;
		Level: number;
		IsFavored: boolean;
		HitPoints: number[];
		SkillAddend: number;
		BaseAttackBonus: number;
		Fortitude: number;
		Reflex: number;
		Will: number;
	}

	export interface IClass extends INamed {
		Alignments: Alignment[];
		HitDie: IDie;
		SkillAddend: number;
		Skills: string[];
		ClassLevels: IClassLevel[];
		Features: string[];
	}

	export interface IClassLevel {
		Level: number;
		BaseAttackBonus: number[];
		Fortitude: number;
		Reflex: number;
		Will: number;
		Specials: string[];
		SpellsPerDay: Map<number>;
		SpellsKnown: Map<number>;
		Spells: Map<string[]>;
	}

	export interface IDefenseScore {
		Type: DefensiveType;
		Score: number;
		ArmorBonus: number;
		ShieldBonus: number;
		DexterityModifier: number;
		SizeModifier: number;
		NaturalBonus: number;
		DeflectBonus: number;
		DodgeBonus: number;
		MiscellaneousBonus: number;
		TemporaryBonus: number;
		BaseAttackBonus: number;
		StrengthModifier: number;
	}

	export interface IDeity extends INamed {
	}

	export interface IDie {
		Faces: number;
	}

	export interface IEffect {
		Name: string;
		Type: EffectType;
		Active: boolean;
		ArmorClassNaturalModifier: number;
		ArmorClassOtherModifier: number;
		FortitudeModifier: number;
		ReflexModifier: number;
		WillModifier: number;
		StrengthModifier: number;
		DexterityModifier: number;
		ConstitutionModifier: number;
		IntelligenceModifier: number;
		WisdomModifier: number;
		CharismaModifier: number;
		MeleeAttackModifier: number;
		MeleeDamageModifier: number;
		MeleeOffHandModifier: number;
		MeleeTwoHandedModifier: number;
		RangedAttackModifier: number;
		RangedDamageModifier: number;
		ExtraAttack_TwoWeaponFightingModifier: number;
		ExtraAttack_MeleeModifier: number;
		ExtraAttack_RangedModifier: number;
		GlobalSkillCheckModifier: number;
		UniqueSkillCheckModifiers: Map<number>;
		SizeModifier: number;
	}

	export interface IFeat {
		Name: string;
		FeatType: FeatType;
		Prerequisites: string[];
		Description: string;
		Benefit: string;
		Special: string;
	}

	export interface IFeature extends INamed {
		Body: string;
		AbilityType: FeatureAbilityType;
		SubFeatures: ISubFeature[];
	}

	export interface IItem extends INamed {
	}

	export interface ILanguage extends INamed {
	}

	export interface INamed {
		Name: string;
	}

	export interface IOffensiveScore {
		Type: OffensiveType;
		Score: number;
		BaseAttackBonus: number;
		AbilityModifier: number;
		SizeModifier: number;
		MiscModifier: number;
		TemporaryModifier: number;
	}

	export interface IPurse {
		Platinum: number;
		Gold: number;
		Silver: number;
		Copper: number;
	}

	export interface IRace extends INamed {
		Adjective: string;
		Size: Size;
		BaseSpeed: number;
		AbilityScores: Map<number>;
		Traits: ITrait[];
		Languages: ILanguage[];
		Description: string;
	}

	export interface ISavingThrow {
		Type: SavingThrowType;
		Score: number;
		Base: number;
		Ability: AbilityType;
		AbilityModifier: number;
		Resist: number;
		Misc: number;
		Temporary: number;
	}

	export interface ISkill extends INamed {
		AbilityType: AbilityType;
		TrainedOnly: boolean;
		ArmorCheckPenalty: boolean;
		Description: string;
		Check: string;
		Action: string;
		TryAgain: string;
		Special: string;
		Restriction: string;
		Untrained: string;
	}

	export interface ISkillScore {
		Skill: ISkill;
		Ability: AbilityType;
		Total: number;
		Ranks: number;
		AbilityModifier: number;
		ClassModifier: number;
		MiscModifier: number;
		TemporaryModifier: number;
		ArmorClassPenalty: number;
	}

	export interface ISpell extends INamed {
		School: MagicSchool;
		SubSchool: MagicSubSchool;
		MagicDescriptors: MagicDescriptor[];
		SavingThrow: string;
		Description: string;
		HasSpellResistance: boolean;
		SpellResistance: string;
		CastingTime: string;
		Range: string;
		LevelRequirements: Map<number>;
		Duration: string;
		Components: [ComponentType, string][];
	}

	export interface ISubFeature extends IFeature {
	}

	export interface ITrait extends INamed {
		Active: boolean;
		Text: string;
		PropertyModifiers: Map<number>;
	}
}