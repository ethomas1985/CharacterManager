namespace app.Character.OffensiveScores {
	import ICharacter = app.character.model.ICharacter;

	export class OffensiveScoresController extends app.Character.BaseCardController {
		character: ICharacter;

		constructor() {
			super();
		}
	}

	export class OffensiveScoresComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = OffensiveScoresController;
			this.templateUrl = "/apps/character/components/offensiveScores/offensiveScores.tmpl.html";

			this.bindings = {
				character: "<" };
		}
	}

	angular.module("character")
		.component("offensiveScores", new OffensiveScoresComponent());
}