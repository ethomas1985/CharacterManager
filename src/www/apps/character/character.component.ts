namespace app.Character {
	import ICharacter = app.character.model.ICharacter;

	export class BaseCardController {
		public viewExpanded: boolean;

		constructor() {
			this.viewExpanded = false;
		}

		public expandCard() {
			this.viewExpanded = !this.viewExpanded;
		}
	}

	export class CharacterComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "characterController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = CharacterController;
			this.templateUrl = "/apps/character/character.tmpl.html";
			// this.bindings = {
			// 	character: "="
			// };
		}
	}

	export class CharacterController {
		isLoading: boolean;
		character: ICharacter;

		private viewExpanded: boolean = false;

		static $inject = ['$http'];
		constructor(private $http: ng.IHttpService) {
			this.isLoading = false;

			let url = 'http://localhost:8888/api/CharacterGenerator/CreateTyrida';

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
}