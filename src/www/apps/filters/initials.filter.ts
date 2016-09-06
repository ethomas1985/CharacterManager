namespace app.filters {
	export function InitialsFilter() {
		return Initials;
	}

	export function Initials(value: string): string {
		return value.split(" ").map(function (s) { return s.charAt(0); }).join("");
	}

	angular.module("app")
		.filter("initials", InitialsFilter);
}