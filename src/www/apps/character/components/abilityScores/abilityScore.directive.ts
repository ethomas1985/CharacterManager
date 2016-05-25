namespace app.Character {
	export class abilityScoreDirective implements ng.IDirective {
		public templateUrl: string;
		public scope:Object;
		public restrict = 'E';
		public replace: boolean = true;

		constructor() {
			this.templateUrl = "/apps/character/components/abilityScores/abilityScore.tmpl.html";
			this.scope = {
				score: "=",
			}
		}

		link() {
		}

		static factory(): ng.IDirectiveFactory {
			const directive = () => new abilityScoreDirective();
			// directive.$inject = [];
			return directive;
		}
	}
}