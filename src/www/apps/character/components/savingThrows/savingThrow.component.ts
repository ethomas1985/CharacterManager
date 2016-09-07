namespace app.Character.SavingThrows {

	interface SavingThrow {
		Type: string;
		Score: number;
	}

	class SavingThrowController {

		constructor() {
		}
	}

	class SavingThrowComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "ctrl";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = SavingThrowController;
			this.templateUrl = "/apps/character/components/savingThrows/savingThrow.tmpl.html";

			this.bindings = {
				throw: "<"
			};
		}
	}

	angular.module("character")
		.component("savingThrow", new SavingThrowComponent());
}