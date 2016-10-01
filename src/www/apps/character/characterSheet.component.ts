namespace app.Character {
	import ICharacter = app.character.model.ICharacter;

	export class BaseCardController {
		public viewExpanded: boolean;

		constructor() {
			this.viewExpanded = true;
		}

		public expandCard() {
			this.viewExpanded = !this.viewExpanded;
		}
	}

	class CharacterSheetController {
		isLoading: boolean;
		character: ICharacter;

		private viewExpanded: boolean = false;

		static $inject = ["$http"];
		constructor(private $http: ng.IHttpService) {
			this.isLoading = false;

			let url = "http://localhost:8888/api/CharacterGenerator/CreatePreBuilt";

			if (!this.character) {
				this.isLoading = true;
				this.$http
					.post(url, null)
					.then(
						(result: ng.IHttpPromiseCallbackArg<ICharacter>) => this.setCharacter(result.data));
			}
		}

		setCharacter(character: ICharacter) {
			this.character = character;
			this.isLoading = false;
		}
	}

	class CharacterSheetComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "characterController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = CharacterSheetController;
			this.templateUrl = "/apps/character/character.tmpl.html";
		}
	}

	angular.module("character")
		.component("characterSheet", new CharacterSheetComponent());
}