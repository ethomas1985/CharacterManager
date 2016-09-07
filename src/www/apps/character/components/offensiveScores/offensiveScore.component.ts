namespace app.Character.OffensiveScores {

	interface OffensiveScore {
		Type: String;
		Score: number;
	}

	class OffensiveScoreController {
		offense: OffensiveScore;

		constructor() {
		}
	}

	class OffensiveScoreComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "ctrl";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = OffensiveScoreController;
			this.templateUrl = "/apps/character/components/offensiveScores/offensiveScore.tmpl.html";

			this.bindings = {
				offense: "<"
			};
		}
	}

	angular.module("character")
		.component("offensiveScore", new OffensiveScoreComponent());
}