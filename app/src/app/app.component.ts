import { Component } from '@angular/core';

import { ApiService } from 'src/services/api.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	title = 'app';

	constructor(private apiService: ApiService) {}

	public httpGet() {
		this.apiService.getData().subscribe(response => {
			console.log('GET Response:', response);
		});
	}

	public httpPost() {
		this.apiService.postData("red").subscribe(response => {
			console.log('POST Response:', response);
		});
	}

}