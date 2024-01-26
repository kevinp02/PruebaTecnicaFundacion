import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { HomeComponent } from './home/home.component';
import { CrearPersonaComponent } from './crear-persona/crear-persona.component';
import { CrearUsuarioComponent } from './crear-usuario/crear-usuario.component';
import { ListaPersonasComponent } from './lista-personas/lista-personas.component';

const routes: Routes = [
  {
    path: 'login',
    component: AuthComponent
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'persona',
    component: CrearPersonaComponent
  },
  {
    path: 'lista-personas',
    component: ListaPersonasComponent
  },
  {
    path: 'usuario',
    component: CrearUsuarioComponent
  },
  {
    path: '**',
    pathMatch: 'full',
    redirectTo: 'login'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
