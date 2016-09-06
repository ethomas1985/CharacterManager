namespace app.Character.SavingThrows {
	import ICharacter = app.character.model.ICharacter;

	export class SavingThrowsController extends app.Character.BaseCardController {
		character: ICharacter;

		constructor() {
			super();
		}
	}

	export class SavingThrowsComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = SavingThrowsController;
			this.templateUrl = "/apps/character/components/savingThrows/savingThrows.tmpl.html";

			this.bindings = {
				character: "<"
			};
		}
	}

	angular.module("character")
		.component("savingThrows", new SavingThrowsComponent());
}