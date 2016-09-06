namespace app.Character {
	angular.module("character", [])
		// .component('race', new raceComponent())
		// .component('classes', new classesComponent())

		// .component('skills', new skillsComponent())
		// .component('feats', new featsComponent())
		.config(configureRouteProvider);

	configureRouteProvider.$inject = ["$routeProvider"];
	function configureRouteProvider($routeProvider: ng.route.IRouteProvider) {
		console.log("Adding Routes for character Module.");
		$routeProvider
			.when("/character", { template: "<character></character>" });
	}
}