import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DatabaseService } from 'src/app/servicios/database.service';

@Component({
  selector: 'app-barra-superior',
  templateUrl: './barra-superior.component.html',
  styleUrls: ['./barra-superior.component.css']
})
export class BarraSuperiorComponent implements OnInit {

  nombre = '';  
  administrador : boolean;
  constructor(private datos: DatabaseService, private router: Router) {
    this.nombre = localStorage.getItem('nombre');
    this.administrador = false;
    if (localStorage.getItem('rol') == "Administrador")
    {
      this.administrador = true;
    }
  }

  ngOnInit(): void {
  }

  CerrarSesion()  {
    this.datos.logout();   
  }

}
