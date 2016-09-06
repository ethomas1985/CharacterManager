namespace app.Character.DefensiveScores {
	import ICharacter = app.character.model.ICharacter;

	export class DefensiveScoresController extends app.Character.BaseCardController {
		character: ICharacter;

		constructor() {
			super();
		}
	}

	export class DefensiveScoresComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = DefensiveScoresController;
			this.templateUrl = "/apps/character/components/defensiveScores/defensiveScores.tmpl.html";

			this.bindings = {
				character: "<"
			};
		}
	}

	angular.module("character")
		.component("defensiveScores", new DefensiveScoresComponent());
}