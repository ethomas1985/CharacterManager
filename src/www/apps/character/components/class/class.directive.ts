namespace app.Character.Classes {
	export class classDirective implements ng.IDirective {
		public templateUrl: string;
		public scope:Object;
		public restrict = 'E';
		public replace: boolean = true;

		constructor() {
			this.templateUrl = "/apps/character/components/class/class.tmpl.html";
			this.scope = {
				characterClass: "=characterClass",
			}
		}

		link() {
		}

		static factory(): ng.IDirectiveFactory {
			const directive = () => new classDirective();
			// directive.$inject = [];
			return directive;
		}
	}
}