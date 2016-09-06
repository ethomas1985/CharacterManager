namespace app.Character.Classes {
	interface IClass {

	}

	interface ICharacterClass {
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

	export class ClassController implements ICharacterClass {
		characterClass: ICharacterClass;

		get Class(): IClass { return this.characterClass.Class; }
		get Level(): number { return this.characterClass.Level; }

		get IsFavored(): boolean { return this.characterClass.IsFavored; }
		get HitPoints(): number[] { return this.characterClass.HitPoints; }

		get SkillAddend(): number { return this.characterClass.SkillAddend; }

		get BaseAttackBonus(): number { return this.characterClass.BaseAttackBonus; }
		get Fortitude(): number { return this.characterClass.Fortitude; }
		get Reflex(): number { return this.characterClass.Reflex; }
		get Will(): number { return this.characterClass.Will; }
	}

	export class ClassComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "ctrl";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = ClassController;
			this.templateUrl = "/apps/character/components/class/class.tmpl.html";
			this.bindings = {
				characterClass: "<"
			};
		}
	}

	angular.module("character")
		.component("class", new ClassComponent());
}