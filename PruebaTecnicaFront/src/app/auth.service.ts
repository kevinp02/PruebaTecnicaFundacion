import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { Usuario } from './classes/usuario';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:4000/api';

  private usuarioSubject!: BehaviorSubject<Usuario>;
  public usuario!: Observable<Usuario>;

  constructor(private router: Router, private http: HttpClient) {
    this.usuarioSubject = new BehaviorSubject<Usuario>(new Usuario());
    this.usuario = this.usuarioSubject.asObservable();
  }

  public get usuarioValue(): Usuario {
    return this.usuarioSubject.value;
  }

  iniciarSesion(usuario: {
    usuario: string;
    contrasena: string;
  }): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/usuariopersona/autenticar`, usuario)
      .pipe(
        map((usuario: Usuario) => {
          this.usuarioSubject.next(usuario);
          return usuario;
        })
      );
  }

  cerrarSesion() {
    this.usuarioSubject.next(new Usuario());
    this.router.navigate(['/login']);
  }
}
