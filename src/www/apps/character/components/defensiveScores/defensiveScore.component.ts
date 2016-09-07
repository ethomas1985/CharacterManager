namespace app.Character.DefensiveScores {
	interface DefensiveScore {
		Type: String;
		Score: number;
	}

	class DefensiveScoreController {
		defense: DefensiveScore;

		constructor() {
		}
	}

	class DefensiveScoreComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "ctrl";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = DefensiveScoreController;
			this.templateUrl = "/apps/character/components/defensiveScores/defensiveScore.tmpl.html";
			this.bindings = {
				defense: "<"
			};
		}
	}

	angular.module("character")
		.component("defensiveScore", new DefensiveScoreComponent());
}