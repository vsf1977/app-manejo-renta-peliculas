import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './componentes/login/login.component';
import { MainComponent } from './componentes/main/main.component';
import { RegistroComponent } from './componentes/registro/registro.component';


const routes: Routes = [
    {path : 'login', component: LoginComponent },
    {path : 'registro', component: RegistroComponent },
    {path : 'main', component: MainComponent },
    {path: '**', pathMatch: 'full', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }