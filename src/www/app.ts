
namespace app {
	"use strict";

	var characterManager =
		angular.module('app', ['ngRoute', 'ngMdIcons', 'ngMaterial', 'characterCreator', 'character']);
	characterManager.config(configureRouteProvider);

	characterManager.filter('camelToHuman', app.filters.CamelToHumanFilter);

	characterManager.directive('labelValue', app.directives.LabelValueDirective.factory());

	characterManager.component('omni', new app.components.OmnibarComponent());
	characterManager.component('foot', new app.components.FooterComponent());

	function configureRouteProvider(
		$routeProvider: ng.route.IRouteProvider,
		$locationProvider: ng.ILocationProvider) {
		console.log('Adding Root Routes');
		$routeProvider
			.otherwise("/");

        // use the HTML5 History API
        // $locationProvider.html5Mode(true);
	}
}