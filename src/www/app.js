var app;
(function (app) {
    "use strict";
    angular.module('app', ['ngRoute', 'ngMdIcons'])
        .config(configureRoutes)
        .component('omni', new app.components.OmnibarComponent())
        .component('foot', new app.components.FooterComponent());
    configureRoutes.$inject = ['$routeProvider', '$locationProvider'];
    function configureRoutes($routeProvider) {
        $routeProvider
            .when("/", {
            template: "<character-creator></character-creator>"
        })
            .otherwise("/");
    }
})(app || (app = {}));
//# sourceMappingURL=app.js.map