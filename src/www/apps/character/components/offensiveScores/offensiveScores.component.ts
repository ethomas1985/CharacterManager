namespace app.Character {
	import Character = app.character.model.ICharacter;

	export class offensiveScoresComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };
		public require: string | string[] | { [controller: string]: string };

		constructor() {
			this.controller = offensiveScoresController;
			this.templateUrl = "/apps/character/components/offensiveScores/offensiveScores.tmpl.html";

			this.require = {
				parentCtrl: '^character' }
		}
	}

	export class offensiveScoresController {
		private viewExpanded: boolean;

		private parentCtrl: any;
		private $controller: app.Character.CharacterController;
		// private character: Character;

		$onInit = () => {
			this.$controller = this.parentCtrl;
		}

		constructor() {
			this.viewExpanded = true;
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