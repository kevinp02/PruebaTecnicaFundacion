import { Component } from '@angular/core';
import { PersonaUsuarioService } from '../persona-usuario.service';

@Component({
  selector: 'app-lista-personas',
  templateUrl: './lista-personas.component.html',
  styleUrls: ['./lista-personas.component.css'],
})
export class ListaPersonasComponent {
  personas: any[] = [];

  constructor(private personaUsuarioService: PersonaUsuarioService) {
    this.personaUsuarioService.getPersonas().subscribe((res) => {
      this.personas = res;
    });
  }
}
