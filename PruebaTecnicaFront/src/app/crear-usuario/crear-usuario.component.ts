import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonaUsuarioService } from '../persona-usuario.service';

@Component({
  selector: 'app-crear-usuario',
  templateUrl: './crear-usuario.component.html',
  styleUrls: ['./crear-usuario.component.css'],
})
export class CrearUsuarioComponent {
  usuarioForm: FormGroup;
  mensaje = null;

  constructor(
    private fb: FormBuilder,
    private personaUsuarioService: PersonaUsuarioService
  ) {
    this.usuarioForm = this.fb.group({
      nombreUsuario: ['', Validators.required],
      contrasena: ['', Validators.required],
    });
  }

  crearUsuario() {
    if (!this.usuarioForm.valid) {
      return;
    }

    this.personaUsuarioService.crearUsuario(this.usuarioForm.value).subscribe(
      (res) => {
        if (res.success) {
          this.mensaje = res.message;
          this.usuarioForm.reset();
          setTimeout(() => {
            this.mensaje = null;
          }, 2500);
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
