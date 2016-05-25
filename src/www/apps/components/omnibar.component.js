var app;
(function (app) {
    var components;
    (function (components) {
        class OmnibarController {
            constructor($location) {
                this.$location = $location;
                this.image = "http://paizo.com/image/content/Logos/PathfinderRPGLogo_500.jpeg";
                this.commands = [
                    {
                        Name: "Wizard",
                        Tooltip: "Start New Character with Wizard",
                        Icon: "note_add",
                        Action: () => this.startWizard($location)
                    },
                    {
                        Name: "New",
                        Tooltip: "Create a blank Character (Advanced)",
                        Icon: "description",
                        Action: () => this.newCharacter($location)
                    },
                ];
            }
            newCharacter($location) {
                console.log("Create New Character Action invoked");
                $location.url("/character");
            }
            startWizard($location) {
                console.log("Start New Character Wizard Action invoked");
                $location.url("/wizard");
            }
        }
        OmnibarController.$inject = ['$location'];
        components.OmnibarController = OmnibarController;
        class OmnibarComponent {
            constructor() {
                this.controllerAs = "omniController";
                this.transclude = true;
                this.controller = OmnibarController;
                this.templateUrl = "apps/components/omnibar.tmpl.html";
                // this.bindings ={
                // 	commands: "="
                // }
            }
        }
        components.OmnibarComponent = OmnibarComponent;
    })(components = app.components || (app.components = {}));
})(app || (app = {}));
//# sourceMappingURL=omnibar.component.js.map