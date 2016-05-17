var app;
(function (app) {
    var components;
    (function (components) {
        var OmnibarController = (function () {
            function OmnibarController() {
                this.image = "http://paizo.com/image/content/Logos/PathfinderRPGLogo_500.jpeg";
            }
            return OmnibarController;
        }());
        components.OmnibarController = OmnibarController;
        var OmnibarComponent = (function () {
            function OmnibarComponent() {
                this.controllerAs = "omniController";
                this.transclude = true;
                this.controller = OmnibarController;
                this.templateUrl = "apps/components/omnibar.tmpl.html";
            }
            return OmnibarComponent;
        }());
        components.OmnibarComponent = OmnibarComponent;
    })(components = app.components || (app.components = {}));
})(app || (app = {}));
//# sourceMappingURL=omnibar.component.js.map