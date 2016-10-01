namespace app.Character.Skills {
	import ICharacter = app.character.model.ICharacter;

	export class SkillsController extends app.Character.BaseCardController {
		character: ICharacter;

		constructor() {
			super();
		}
	}

	export class SkillsComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = SkillsController;
			this.templateUrl = "/apps/character/components/skills/skills.tmpl.html";

			this.bindings = {
				character: "<"
			};
		}
	}

	angular.module("character")
		.component("skills", new SkillsComponent());
}