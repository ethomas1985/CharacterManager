namespace app.CharacterCreator {
	angular
		.module("characterCreator", ["ngRoute"])
		.config(configureRouteProvider);

	configureRouteProvider.$inject = ["$routeProvider"];
	function configureRouteProvider($routeProvider: ng.route.IRouteProvider) {
		console.log("Adding Route for characterCreator Module.");
		$routeProvider
			.when("/wizard", <ng.route.IRoute>{ template: "<new-wizard></new-wizard>" });
	};
}