import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './componentes/login/login.component';
import { PeliculaComponent } from './componentes/pelicula/pelicula.component';
import { RegistroComponent } from './componentes/registro/registro.component';
import { ReservaComponent } from './componentes/reserva/reserva.component';


const routes: Routes = [
    {path : 'login', component: LoginComponent },
    {path : 'registro', component: RegistroComponent },
    {path : 'reserva', component: ReservaComponent },
    {path : 'pelicula', component: PeliculaComponent },
    {path: '**', pathMatch: 'full', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }