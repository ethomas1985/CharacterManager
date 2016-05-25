namespace app.filters {
	export function CamelToHumanFilter() {
		return CamelToHuman;
	}

	export function CamelToHuman(value: string): string {
		return value.replace(/([A-Z])/g, ' $1');
	}
}