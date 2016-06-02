namespace app.Character.OffensiveScores {

	export interface IOffensiveScoreScope extends ng.IScope {
		score: app.character.model.IOffensiveScore;
		tooltip: string;
	}

	export class offensiveScoreDirective implements ng.IDirective {
		public templateUrl: string;
		public controller: any;
		public controllerAs: string = "controller";
		public scope: Object;
		public restrict = 'E';
		public replace: boolean = true;
		public link: (
			scope: IOffensiveScoreScope,
			instanceElement: ng.IAugmentedJQuery,
			instanceAttributes: ng.IAttributes,
			controller: {}
        ) => void;

		constructor() {
				this.templateUrl = "/apps/character/components/offensiveScores/offensiveScore.tmpl.html";
			this.scope = {
				score: "=",
			}

			this.link = (
				scope: IOffensiveScoreScope,
				instanceElement: ng.IAugmentedJQuery,
				instanceAttributes: ng.IAttributes,
				controller: {}) => {
				// 	let values: string[] = [];
				// 	values.push('[Base Attack Bonus: '  + scope.score.BaseAttackBonus + ']');
				// 	values.push('[Ability: '  + scope.score.AbilityModifier + ']');
				// 	values.push('[Size: '  + scope.score.SizeModifier + ']');
				// 	values.push('[Misc.: '  + scope.score.MiscModifier + ']');
				// 	values.push('[Temp.: '  + scope.score.TemporaryModifier + ']');

				// scope.tooltip = values.join(" + ");
			}
		}

		static factory(): ng.IDirectiveFactory {
			const directive = () => new offensiveScoreDirective();
			// directive.$inject = [];
			return directive;
		}
	}
}