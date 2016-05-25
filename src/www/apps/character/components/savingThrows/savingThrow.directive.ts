namespace app.Character {

	export interface ISavingThrowScope extends ng.IScope {
		score: app.character.model.ISavingThrow;
		tooltip: string;
	}

	export class savingThrowDirective implements ng.IDirective {
		public templateUrl: string;
		public controller: any;
		public controllerAs: string = "controller";
		public scope: Object;
		public restrict = 'E';
		public replace: boolean = true;
		public link: (
			scope: ISavingThrowScope,
			instanceElement: ng.IAugmentedJQuery,
			instanceAttributes: ng.IAttributes,
			controller: {}
        ) => void;

		constructor() {
				this.templateUrl = "/apps/character/components/savingThrows/savingThrow.tmpl.html";
			this.scope = {
				score: "=",
			}

			this.link = (
				scope: ISavingThrowScope,
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
			const directive = () => new savingThrowDirective();
			// directive.$inject = [];
			return directive;
		}
	}
}