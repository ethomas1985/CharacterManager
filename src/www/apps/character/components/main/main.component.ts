namespace app.Character {
	import Character = app.character.model.ICharacter;

	export class MainCardController extends app.Character.BaseCardController {
		character: Character;

		constructor() {
			super();
		}

		get title(): string {
			return this.character ? "Main" : "Main | Character was not passed.";
		}
	}

	export class MainCardComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = MainCardController;
			this.templateUrl = "/apps/character/components/main/main.tmpl.html";

			this.bindings = {
				character: "<"
			};
		}
	}

	angular.module("character")
		.component("main", new MainCardComponent());
}