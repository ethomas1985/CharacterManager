/// <reference path="_all.ts" />

namespace app {
	"use strict";

	let characterManager =
		angular.module(
			"app",
			["ngRoute",
			"ngMdIcons",
			"ngMaterial",
			"character"]);
	characterManager.config(configureRouteProvider);

	function configureRouteProvider(
		$routeProvider: ng.route.IRouteProvider,
		$locationProvider: ng.ILocationProvider) {
		console.log("Adding Root Routes");
		$routeProvider
			.otherwise("/");

        // use the HTML5 History API
        // $locationProvider.html5Mode(true);
	}
}