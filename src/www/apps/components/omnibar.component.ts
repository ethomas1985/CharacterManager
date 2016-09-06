namespace app.components {

	export interface ICommand {
		Name: string;
		Tooltip: string;
		Icon: string;
		Action: () => void;
	}

	export class OmnibarController {
		public image: string = "http://paizo.com/image/content/Logos/PathfinderRPGLogo_500.jpeg";
		public commands: ICommand[];


		static $inject = ["$location"];

		constructor(private $location: ng.ILocationService) {
			this.commands = [
				<ICommand>{
					Name: "Wizard",
					Tooltip: "Start New Character with Wizard",
					Icon: "note_add",
					Action: () => this.startWizard($location)
				},
				<ICommand>{
					Name: "New",
					Tooltip: "Create a blank Character (Advanced)",
					Icon: "description",
					Action: () => this.newCharacter($location)
				},
			];
		}

		newCharacter($location: ng.ILocationService) {
			console.log("Create New Character Action invoked");
			$location.url("/character");
		}

		startWizard($location: ng.ILocationService) {
			console.log("Start New Character Wizard Action invoked");
			$location.url("/wizard");
		}
	}

	export class OmnibarComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "omniController";
		public templateUrl: string;
		public transclude: boolean = true;
		public bindings: { [binding: string]: string };

		constructor() {
			this.controller = OmnibarController;
			this.templateUrl = "apps/components/omnibar.tmpl.html";
			// this.bindings ={
			// 	commands: "="
			// }
		}
	}

	angular.module("app")
		.component("omni", new app.components.OmnibarComponent());
}