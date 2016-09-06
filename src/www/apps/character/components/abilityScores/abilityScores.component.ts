namespace app.Character.AbilityScores {
	import ICharacter = app.character.model.ICharacter;

	export class AbilityScoresController extends app.Character.BaseCardController {
		character: ICharacter;

		constructor() {
			super();
		}
	}

	export class AbilityScoresComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = AbilityScoresController;
			this.templateUrl = "/apps/character/components/abilityScores/abilityScores.tmpl.html";
			this.bindings = {
				character: "<"
			};
		}
	}

	angular.module("character")
		.component("abilityScores", new AbilityScoresComponent());
}