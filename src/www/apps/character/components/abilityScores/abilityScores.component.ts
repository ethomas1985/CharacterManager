namespace app.Character.AbilityScores {
	import ICharacter = app.character.model.ICharacter;

	export class abilityScoresComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };
		public require: string | string[] | { [controller: string]: string };

		constructor() {
			this.controller = abilityScoresController;
			this.templateUrl = "/apps/character/components/abilityScores/abilityScores.tmpl.html";

			this.require = {
				parentCtrl: '^character' }
		}
	}

	export class abilityScoresController extends app.Character.BaseCardController {
		private parentCtrl: any;
		private $controller: app.Character.CharacterController;

		$onInit = () => {
			this.$controller = this.parentCtrl;
		}

		constructor() {
			super();
		}

		get character(): ICharacter{
			return this.$controller.character;
		}
		set character(character: ICharacter){
			this.$controller.character = character;
		}
	}
}