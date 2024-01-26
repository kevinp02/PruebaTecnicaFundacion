import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent {
  username = '';
  password = '';
  loading = false;

  constructor(private authService: AuthService, private router: Router) {}

  iniciarSesion() {
    this.loading = true;
    const usuario = { usuario: this.username, contrasena: this.password };

    this.authService.iniciarSesion(usuario).subscribe(
      (res: any) => {
        this.loading = false;
        this.router.navigate(['/home']);
      },
      (error: any) => {
        console.error('Sign-in error', error);
      }
    );
  }
}
