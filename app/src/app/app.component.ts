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

	public sendReq() {
		this.apiService.getData().subscribe(response => {
			console.log('GET Response:', response);
		});
	}

}