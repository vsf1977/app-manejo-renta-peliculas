import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Peliculamodel } from 'src/app/modelos/modelos.module';
import { DatabaseService } from 'src/app/servicios/database.service';

@Component({
  selector: 'app-pelicula',
  templateUrl: './pelicula.component.html',
  styleUrls: ['./pelicula.component.css']
})
export class PeliculaComponent implements OnInit {

  constructor(private Datos: DatabaseService) { }

  peliculas: Peliculamodel[] = [];
  pelicula: Peliculamodel = new Peliculamodel();
  edicion_borrado = false;
  ultima_pelicula = 0;
  btnCrear = false;
  btnEditar = false;
  btnBorrar = false;

  ngOnInit(): void {      
    this.Datos.checkSesion("pelicula");
    this.actualizarInfo();
  }

  actualizarInfo(){
    this.Datos.getMovies().subscribe((data) => {      
      this.peliculas = data;
      data.forEach((x) =>{
        if (parseInt(x.id) > this.ultima_pelicula)
          this.ultima_pelicula = parseInt(x.id);
      })
      this.pelicula.id = String(this.ultima_pelicula + 1);
    });
  }

  crearPelicula(f: NgForm){
    if (!f.valid)
      alert("Falta información, revisar los datos")                
    else                                      
      this.Datos.newMovie(this.pelicula).subscribe((data)=>{
        console.log(data);        
        if (data == null){
          alert("Se creó la pelicula exitosamente")
          this.actualizarInfo();
          f.reset();
        }
        else
          alert("Ocurrio un error al crear la pelicula")
    }); 
  }

  editarPelicula(f: NgForm){
    if (!this.edicion_borrado) {
      this.edicion_borrado = true;    
      this.btnCrear = true;
      this.btnBorrar = true;
    }
    else{
      if (!f.valid)
        alert("Falta información, revisar los datos");
      else 
      {
        this.Datos.editMovie(this.pelicula).subscribe((data)=>{
          if (data == null){
            alert("Se editó la pelicula exitosamente")            
          }
          else
            alert("Ocurrio un error al editar la pelicula")
          f.reset();
          this.actualizarInfo();
          this.edicion_borrado = false;
          this.btnCrear = false;
          this.btnBorrar = false;
        });
      }
    }
  }

  borrarPelicula(f: NgForm){
    let existe = false;
    if (!this.edicion_borrado) {
        this.edicion_borrado = true;  
        this.btnCrear = true;
        this.btnEditar = true;
      }
      else{     
        this.Datos.getReservations().subscribe((data) => {  
          data.forEach(x => {
            if (x.id_pelicula == this.pelicula.id)
              existe = true;
          });    
          if (existe)
            alert("Hay una reserva de esta pelicula y no se puede eliminar")
          else{
            this.Datos.deleteMovie(this.pelicula.id).subscribe((data)=>{
              console.log(data);        
              if (data == null){
                alert("Se borró la pelicula exitosamente")            
              }
              else
                alert("Ocurrio un error al borrar la pelicula")              
            });
          }
          f.reset();
          this.actualizarInfo();
          this.edicion_borrado = false;
          this.btnCrear = false;
          this.btnEditar = false;
        });     
      }
    }

  
  selIdpelicula(){
    let pelicula_elegida : Peliculamodel;
    pelicula_elegida = this.peliculas.filter(x => x.id == this.pelicula.id)[0];
    this.pelicula.titulo = pelicula_elegida.titulo;
    this.pelicula.descripcion = pelicula_elegida.descripcion;
    this.pelicula.director = pelicula_elegida.director;
    this.pelicula.costo_alquiler = pelicula_elegida.costo_alquiler;
    this.pelicula.descripcion = pelicula_elegida.descripcion;
    this.pelicula.cantidad = pelicula_elegida.cantidad;
    this.pelicula.actores = pelicula_elegida.actores;
  }

}
