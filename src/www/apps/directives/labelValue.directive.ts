namespace app.directives {
	export class LabelValueDirective implements ng.IDirective {
		public templateUrl: string;
		public scope: Object;
		public restrict = "E";

		constructor() {
			this.templateUrl = "apps/directives/labelValue.tmpl.html";
			this.scope = {
				label: "@",
				value: "@",
				css: "@"
			};
		}

		link() {
		}

		static factory(): ng.IDirectiveFactory {
			const directive = () => new LabelValueDirective();
			// directive.$inject = [];
			return directive;
		}
	}

	angular.module("app")
		.directive("labelValue", app.directives.LabelValueDirective.factory());
}