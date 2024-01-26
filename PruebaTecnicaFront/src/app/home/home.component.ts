import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  nombre = '';
  constructor(private authService: AuthService) {
    this.nombre = this.authService.usuarioValue.nombreUsuario;
    if (!this.nombre) {
      this.authService.cerrarSesion();
    }
  }

  cerrarSesion() {
    this.authService.cerrarSesion();
  }
}
