namespace app.Character {
	angular.module('character', [])
		.component('character', new app.Character.CharacterComponent())

		.component('main', new app.Character.mainCardComponent())
		// .component('race', new raceComponent())
		// .component('classes', new classesComponent())

		.component('classes', new app.Character.Classes.classesComponent())
		.directive('class', app.Character.Classes.classDirective.factory())

		.component('status', new app.Character.statusCardComponent())

		.component('abilityScores', new app.Character.AbilityScores.abilityScoresComponent())
		.directive('abilityScore', app.Character.AbilityScores.abilityScoreDirective.factory())

		.component('defensiveScores', new app.Character.DefensiveScores.defensiveScoresComponent())
		.directive('defensiveScore', app.Character.DefensiveScores.defensiveScoreDirective.factory())

		.component('offensiveScores', new app.Character.OffensiveScores.offensiveScoresComponent())
		.directive('offensiveScore', app.Character.OffensiveScores.offensiveScoreDirective.factory())

		.component('savingThrows', new app.Character.SavingThrows.savingThrowsComponent())
		.directive('savingThrow', app.Character.SavingThrows.savingThrowDirective.factory())
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