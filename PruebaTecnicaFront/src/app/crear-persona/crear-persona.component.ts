import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonaUsuarioService } from '../persona-usuario.service';

@Component({
  selector: 'app-crear-persona',
  templateUrl: './crear-persona.component.html',
  styleUrls: ['./crear-persona.component.css'],
})
export class CrearPersonaComponent {
  personaForm: FormGroup;
  mensaje = null;

  constructor(
    private fb: FormBuilder,
    private personaUsuarioService: PersonaUsuarioService
  ) {
    this.personaForm = this.fb.group({
      Nombres: ['', Validators.required],
      Apellidos: ['', Validators.required],
      NumeroIdentificacion: ['', Validators.required],
      Email: ['', Validators.required],
      TipoIdentificacion: ['', Validators.required],
    });
  }

  crearPersona() {
    if (!this.personaForm.valid) {
      return;
    }

    this.personaUsuarioService.crearPersona(this.personaForm.value).subscribe(
      (res) => {
        if (res.success) {
          this.mensaje = res.message;
          this.personaForm.reset();
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
