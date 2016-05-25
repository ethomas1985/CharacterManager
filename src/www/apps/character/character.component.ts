namespace app.Character {

	import Character = app.character.model.ICharacter;
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
		character: Character;

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
						(result: ng.IHttpPromiseCallbackArg<Character>) => this.setCharacter(result.data));
			}
		}

		setCharacter(character: Character) {
			this.character = character;

			// this.character.Name = "Character McCharacterFace";
			// this.character.MaxHealthPoints = 100;
			// this.character.HealthPoints = 48;

			this.isLoading = false;
		}

		expandCard() {
			this.viewExpanded = !this.viewExpanded;
		}
	}
}