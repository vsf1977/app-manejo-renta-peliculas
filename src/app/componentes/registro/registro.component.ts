import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
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

  crearUsuario(f: NgForm) {           
    this.Datos.checkUser(this.usuario).subscribe((data)=>{       
      if (data)
        alert("Ya existe este usuario")
      else{
        if (!f.valid)
          alert("Falta información, revisar los datos")                
        else                  
          this.Datos.newUser(this.usuario).subscribe((data)=>{
            if (data == null){
              alert("Se creo el usuario exitosamente")
              localStorage.setItem('usuario', this.usuario.id);          
              localStorage.setItem('nombre', this.usuario.nombre); 
              localStorage.setItem('rol', this.usuario.rol);  
              this.router.navigate(['main']);
            }
            else
              alert("Ocurrio un error al crear el usuario")
          });  
      }
    });
  }

  volverLogin()
  {
    this.router.navigate(['login']);
  }

}
