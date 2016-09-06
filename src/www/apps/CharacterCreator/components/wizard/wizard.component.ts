namespace app.CharacterCreator {

	interface IAbilityScoreSet {
		Strength: number;
		Dexterity: number;
		Constitution: number;
		Intelligence: number;
		Wisdom: number;
		Charisma: number;
	}

	interface IAbilityScore {
		Name: string;
		Value: number;
		Text: string;
	}

	export class WizardController {
		scores: IAbilityScore[];
		scoreset: IAbilityScoreSet;

		constructor() {
			this.scores = [
				<IAbilityScore>{
					Name: "Strength",
					Value: 0,
					Text: "Strength Description Goes Here"
				},
				<IAbilityScore>{
					Name: "Dexterity",
					Value: 0,
					Text: "Dexterity Description Goes Here"
				},
				<IAbilityScore>{
					Name: "Constitution",
					Value: 0,
					Text: "Constitution Description Goes Here"
				},
				<IAbilityScore>{
					Name: "Intelligence",
					Value: 0,
					Text: "Intelligence Description Goes Here"
				},
				<IAbilityScore>{
					Name: "Wisdom",
					Value: 0,
					Text: "Wisdom Description Goes Here"
				},
				<IAbilityScore>{
					Name: "Charisma",
					Value: 0,
					Text: "Charisma Description Goes Here"
				},
			];

			this.scoreset = <IAbilityScoreSet>{
				Strength: 0,
				Dexterity: 0,
				Constitution: 0,
				Intelligence: 0,
				Wisdom: 0,
				Charisma: 0
			};
		}
	}

	export class WizardComponent implements ng.IComponentOptions {
		public controller: any;
		public controllerAs: string = "characterController";
		public templateUrl: string;
		public transclude: boolean = true;

		constructor() {
			this.controller = WizardController;
			this.templateUrl = "/apps/CharacterCreator/components/wizard/wizard.tmpl.html";
		}
	}

}