import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PersonaUsuarioService {
  private apiUrl = 'http://localhost:4000/api';

  constructor(private http: HttpClient) {}

  crearPersona(persona: any): Observable<any> {
    const requestBody = JSON.stringify(persona);
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post(`${this.apiUrl}/usuariopersona/persona`, requestBody, { headers });
  }

  crearUsuario(usuario: any): Observable<any> {
    const requestBody = JSON.stringify(usuario);
    const headers = { 'Content-Type': 'application/json' };
    return this.http.post(`${this.apiUrl}/usuariopersona/usuario`, requestBody, { headers });
  }

  getPersonas(): Observable<any> {
    const headers = { 'Content-Type': 'application/json' };
    return this.http.get(`${this.apiUrl}/usuariopersona/personas`, { headers });
  }
}
