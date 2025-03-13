import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ApiService {
    private baseUrl = 'http://localhost:5289/api'; 

    constructor(private http: HttpClient) { 
        
    }

    // HTTP GET Request Example
    getData(): Observable<any> {
        return this.http.get<any>(`${this.baseUrl}/led/status`);
    }

    // HTTP POST Request Example
    postData(color: string): Observable<any> {
        return this.http.post<any>(`${this.baseUrl}/led/setColor`, { color },
        {
            headers: { 'Content-Type': 'application/json' }
        }
        );
    }

    startTemperature(): Observable<any> {
        return this.http.get<any>(`${this.baseUrl}/led/startTemperature`);
    }

    getTemperature(): Observable<any> {
        return this.http.get<any>(`${this.baseUrl}/led/temperature`);
    }
    

}