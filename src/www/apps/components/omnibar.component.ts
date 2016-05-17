
namespace app.components {

	export class OmnibarController {
		public image: string = "http://paizo.com/image/content/Logos/PathfinderRPGLogo_500.jpeg";
	}

	export class OmnibarComponent implements ng.IComponentOptions{
		public controller:any;
		public controllerAs: string = "omniController";
		public templateUrl: string;
		public transclude: boolean = true;

		constructor(){
			this.controller = OmnibarController;
			this.templateUrl = "apps/components/omnibar.tmpl.html";
		}
	}
}