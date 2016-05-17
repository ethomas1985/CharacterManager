var app;
(function (app) {
    var components;
    (function (components) {
        var FooterController = (function () {
            function FooterController() {
                this.title = "CharacterManager";
                this.version = "0.0.0.1";
            }
            return FooterController;
        }());
        components.FooterController = FooterController;
        var FooterComponent = (function () {
            function FooterComponent() {
                this.controllerAs = "footerController";
                this.transclude = true;
                this.controller = FooterController;
                this.templateUrl = "apps/components/footer.tmpl.html";
            }
            return FooterComponent;
        }());
        components.FooterComponent = FooterComponent;
    })(components = app.components || (app.components = {}));
})(app || (app = {}));
//# sourceMappingURL=footer.component.js.map