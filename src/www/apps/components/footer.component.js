var app;
(function (app) {
    var components;
    (function (components) {
        class FooterController {
            constructor() {
                this.title = "CharacterManager";
                this.version = "0.0.0.1";
            }
        }
        components.FooterController = FooterController;
        class FooterComponent {
            constructor() {
                this.controllerAs = "footerController";
                this.transclude = true;
                this.controller = FooterController;
                this.templateUrl = "apps/components/footer.tmpl.html";
            }
        }
        components.FooterComponent = FooterComponent;
    })(components = app.components || (app.components = {}));
})(app || (app = {}));
//# sourceMappingURL=footer.component.js.map