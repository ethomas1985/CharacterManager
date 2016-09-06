namespace app.Character.AbilityScores {

	interface IAbilityScore {
		Type: string;
		Score: number;
		Modifier: number;
	}

	class AbilityScoreController implements IAbilityScore {
		ability: IAbilityScore;

		get Type(): string {
			return this.ability.Type;
		}

		get Score(): number{
			return this.ability.Score;
		}

		get Modifier(): number {
			return this.ability.Modifier;
		}
	}

	class AbilityScoreComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "ctrl";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = AbilityScoreController;
			this.templateUrl = "/apps/character/components/abilityScores/abilityScore.tmpl.html";
			this.bindings = {
				ability: "<"
			};
		}
	}

	angular.module("character")
		.component("abilityScore", new AbilityScoreComponent());
}