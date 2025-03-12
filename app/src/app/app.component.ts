import { Component } from '@angular/core';

import { ApiService } from 'src/services/api.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	title = 'app';

	public currentColor: string = "null";

	constructor(private apiService: ApiService) {}

	public httpGet() {
		this.apiService.getData().subscribe(response => {
			this.currentColor = response.color;
			console.log('GET Response:', response);
		});
	}

	public httpPost(color: string) {
		this.apiService.postData(color).subscribe(response => {
			console.log('POST Response:', response);
		});
	}

}