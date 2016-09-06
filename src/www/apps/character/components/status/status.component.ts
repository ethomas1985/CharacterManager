namespace app.Character {
	import Character = app.character.model.ICharacter;

	export class StatusController {
		private viewExpanded: boolean;

		character: Character;

		constructor() {
			this.viewExpanded = false;
		}

		expandCard() {
			this.viewExpanded = !this.viewExpanded;
		}

		getCurrentHealthValue(): number {
			let health = 100 * (this.character.HealthPoints / this.character.MaxHealthPoints);
			return health;
		}
	}

	export class StatusCardComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = StatusController;
			this.templateUrl = "/apps/character/components/status/status.tmpl.html";

			this.bindings = {
				character: "<"
			};
		}
	}

	angular.module("character")
		.component("status", new StatusCardComponent());
}