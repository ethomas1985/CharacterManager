namespace app.Character.DefensiveScores {

	export interface IDefensiveScoreScope extends ng.IScope {
		score: app.character.model.IDefenseScore;
		tooltip: string;
	}

	export class defensiveScoreDirective implements ng.IDirective {
		public templateUrl: string;
		public controller: any;
		public controllerAs: string = "controller";
		public scope: Object;
		public restrict = 'E';
		public replace: boolean = true;
		public link: (
			scope: ng.IScope,
			instanceElement: ng.IAugmentedJQuery,
			instanceAttributes: ng.IAttributes,
			controller: {}
        ) => void;

		constructor() {
			this.templateUrl = "/apps/character/components/defensiveScores/defensiveScore.tmpl.html";
			this.scope = {
				score: "=",
			}

			this.link = (
				scope: IDefensiveScoreScope,
				instanceElement: ng.IAugmentedJQuery,
				instanceAttributes: ng.IAttributes,
				controller: {}) => {
				// let values: string[] = [];
				// if (scope.score.ArmorBonus) {
				// 	values.push('[Armor: ' + scope.score.ArmorBonus + ']');
				// }

				// if (scope.score.ShieldBonus) {
				// 	values.push('[Shield: ' + scope.score.ShieldBonus + ']');
				// }

				// if (scope.score.DexterityModifier) {
				// 	values.push('[Dexterity: ' + scope.score.DexterityModifier + ']');
				// }

				// values.push('[Size: ' + scope.score.SizeModifier + ']');

				// if (scope.score.NaturalBonus) {
				// 	values.push('[Natural: ' + scope.score.NaturalBonus + ']');
				// }

				// values.push('[Deflect: ' + scope.score.DeflectBonus + ']');

				// if (scope.score.DodgeBonus) {
				// 	values.push('[Dodge: ' + scope.score.DodgeBonus + ']');
				// }

				// values.push('[Misc.: ' + scope.score.MiscellaneousBonus + ']');
				// values.push('[Temp.: ' + scope.score.TemporaryBonus + ']');

				// scope.tooltip = values.join(" + ");
			}
		}

		static factory(): ng.IDirectiveFactory {
			const directive = () => new defensiveScoreDirective();
			// directive.$inject = [];
			return directive;
		}
	}
}