import { Component, OnInit } from '@angular/core';
import { DatabaseService } from '../../servicios/database.service';
import { Usuariomodel }  from '../../modelos/modelos.module';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  usuario: Usuariomodel = new Usuariomodel();

  constructor(private Datos: DatabaseService, private router: Router) { }

  ngOnInit(): void {      
    this.Datos.checkSesion("reserva");
  }

  login() {
    this.Datos.login(this.usuario);
  }

  registro()
  {
    this.router.navigate(['registro']);
  }


}
