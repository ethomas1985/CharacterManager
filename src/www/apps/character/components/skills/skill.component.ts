namespace app.Character.Skills {
	import ICharacter = app.character.model.ICharacter;

	interface SkillScore {
		Ability: string;
		Ranks: number;
		AbilityModifier: number;
		ClassModifier: number;
		MiscModifier: number;
		TemporaryModifier: number;
		ArmorClassPenalty: number;
		Total: number;
		Skill: ISkill;
	}

	interface ISkill {
		Name: string;
		AbilityType: string;
		TrainedOnly: boolean;
		ArmorCheckPenalty: boolean;
	}

	export class SkillController {
		skill: SkillScore;

		constructor() {
		}

		get Ability(): string {
			return this.skill.Ability.substring(0, 3);
		}
	}

	export class SkillComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "ctrl";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = SkillController;
			this.templateUrl = "/apps/character/components/skills/skill.tmpl.html";

			this.bindings = {
				skill: "<"
			};
		}
	}

	angular.module("character")
		.component("skill", new SkillComponent());
}