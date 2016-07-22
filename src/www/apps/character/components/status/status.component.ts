namespace app.Character {
	import Character = app.character.model.ICharacter;

	export class statusCardComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };
		public require: string | string[] | { [controller: string]: string };

		constructor() {
			this.controller = statusController;
			this.templateUrl = "/apps/character/components/status/status.tmpl.html";

			this.require = {
				parentCtrl: '^character' }
		}
	}

	export class statusController {
		private viewExpanded: boolean;
		private parentCtrl: any;
		private $controller: app.Character.CharacterController;

		$onInit = () => {
			this.$controller = this.parentCtrl;
		}

		constructor() {
			this.viewExpanded = false;
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

		getCurrentHealthValue() : number {
			let health =  100 * (this.character.HealthPoints / this.character.MaxHealthPoints);
			return health;
		}
	}
}