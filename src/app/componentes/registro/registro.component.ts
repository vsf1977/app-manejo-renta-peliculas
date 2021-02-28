import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuariomodel } from 'src/app/modelos/modelos.module';
import { DatabaseService } from 'src/app/servicios/database.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  usuario: Usuariomodel = new Usuariomodel();

  constructor(private Datos: DatabaseService, private router: Router) { }

  ngOnInit(): void {
    this.Datos.checkSesion();
  }

  crearUsuario() {
    //this.Datos.checkUser(this.usuario);
  }

  volverLogin()
  {
    this.router.navigate(['registrlogino']);
  }

}
