import { Component } from '@angular/core';
import { timeInterval } from 'rxjs';

import { ApiService } from 'src/services/api.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	title = 'app';

	public currentColor: string = "null";
	public temperature: number = 0.0;

	constructor(private apiService: ApiService) {}

	ngOnInit() {
		this.apiService.startTemperature().subscribe(response => {
			this.temperature = response.temperature;
			console.log('GET Response:', response);
		});

		setInterval(() => {
			this.apiService.getTemperature().subscribe(response => {
				this.temperature = response.temperature * 9/5 + 32;
				console.log('GET Response:', response);
			});
		}, 1000);
	}

	public httpGet() {
		this.apiService.getData().subscribe(response => {
			this.currentColor = response.color;
			console.log('GET Response:', response);
		});

		this.apiService.getTemperature().subscribe(response => {
			this.temperature = response.temperature;
			console.log('GET Response:', response);
		});
	}

	public httpPost(color: string) {
		this.apiService.postData(color).subscribe(response => {
			console.log('POST Response:', response);
		});
	}

}