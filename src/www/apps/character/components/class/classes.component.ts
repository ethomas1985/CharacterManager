namespace app.Character.Classes {
	import ICharacter = app.character.model.ICharacter;

	export class ClassesController extends app.Character.BaseCardController {
		character: ICharacter;

		constructor() {
			super();
		}
	}

	export class ClassesComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = ClassesController;
			this.templateUrl = "/apps/character/components/class/classes.tmpl.html";

			this.bindings = {
				character: "<"
			};
		}
	}

	angular.module("character")
		.component("classes", new ClassesComponent());
}