namespace app.Character.DefensiveScores {
	import ICharacter = app.character.model.ICharacter;

	export class defensiveScoresComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "cardController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };
		public require: string | string[] | { [controller: string]: string };

		constructor() {
			this.controller = defensiveScoresController;
			this.templateUrl = "/apps/character/components/defensiveScores/defensiveScores.tmpl.html";

			this.require = {
				parentCtrl: '^character' }
		}
	}

	export class defensiveScoresController extends app.Character.BaseCardController{
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