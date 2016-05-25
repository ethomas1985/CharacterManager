namespace app.Character {

	import Character = app.character.model.ICharacter;

	angular.module('character', [])
		.component('character', new app.Character.CharacterComponent())

		.component('main', new app.Character.mainCardComponent())
		// .component('race', new raceComponent())
		// .component('classes', new classesComponent())
		// .component('class', new classComponent())

		.component('status', new app.Character.statusCardComponent())

		.component('abilityScores', new app.Character.abilityScoresComponent())
		.directive('abilityScore', app.Character.abilityScoreDirective.factory())

		.component('defensiveScores', new app.Character.defensiveScoresComponent())
		.directive('defensiveScore', app.Character.defensiveScoreDirective.factory())

		.component('offensiveScores', new app.Character.offensiveScoresComponent())
		.directive('offensiveScore', app.Character.offensiveScoreDirective.factory())

		.component('savingThrows', new app.Character.savingThrowsComponent())
		.directive('savingThrow', app.Character.savingThrowDirective.factory())
		// .component('skills', new skillsComponent())
		// .component('feats', new featsComponent())
		.config(configureRouteProvider);

	configureRouteProvider.$inject = ['$routeProvider'];
	function configureRouteProvider($routeProvider: ng.route.IRouteProvider) {
		console.log('Adding Routes for character Module.');
		$routeProvider
			.when("/character", {template: "<character></character>"})
	}
}