namespace app.components {

	export class FooterController {
		public title: string = "CharacterManager";
		public version: string = "0.0.0.1";
	}

	export class FooterComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "footerController";
		public templateUrl: string;
		public transclude: boolean = true;

		constructor() {
			this.controller = FooterController;
			this.templateUrl = "apps/components/footer.tmpl.html";
		}
	}

	angular.module("app")
		.component("foot", new app.components.FooterComponent());
}