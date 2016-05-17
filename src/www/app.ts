
namespace app {
	"use strict";

	angular.module('app', ['ngRoute', 'ngMdIcons'])
		.config(configureRoutes)
		.component('omni', new app.components.OmnibarComponent())
		.component('foot', new app.components.FooterComponent())
		;

	configureRoutes.$inject = ['$routeProvider', '$locationProvider'];
	function configureRoutes($routeProvider: ng.route.IRouteProvider) {
		$routeProvider
			.when("/",<ng.route.IRoute>{
				template: "<character-creator></character-creator>"
			})
			.otherwise("/");
	}
}