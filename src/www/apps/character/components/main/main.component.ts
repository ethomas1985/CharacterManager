namespace app.Character {
	import Character = app.character.model.ICharacter;

	export class mainCardComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };
		public require: string | string[] | { [controller: string]: string };

		constructor() {
			this.controller = mainCardController;
			this.templateUrl = "/apps/character/components/main/main.tmpl.html";

			this.require = {
				parentCtrl: '^character' }
		}
	}

	export class mainCardController {
		private viewExpanded: boolean;

		private parentCtrl: any;
		private $controller: app.Character.CharacterController;
		// private character: Character;

		$onInit = () => {
			this.$controller = this.parentCtrl;
		}

		constructor() {
			this.viewExpanded = false;
		}

		get title() :string {
			return this.character ? 'Main' : 'Main | Character was not passed.'
		}

		get character(): Character{
			return this.$controller.character;
		}
		set character(character: Character){
			this.$controller.character = character;
		}

		expandCard() {
			this.viewExpanded = !this.viewExpanded;
		}
	}
}