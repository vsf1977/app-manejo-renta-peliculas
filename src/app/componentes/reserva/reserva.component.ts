import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Peliculamodel, Reservamodel, Usuariomodel } from 'src/app/modelos/modelos.module';
import { DatabaseService } from 'src/app/servicios/database.service';

@Component({
  selector: 'app-reserva',
  templateUrl: './reserva.component.html',
  styleUrls: ['./reserva.component.css']
})
export class ReservaComponent implements OnInit {

  usuarios: Usuariomodel[] = [];
  reserva : Reservamodel =  new Reservamodel();
  peliculas: Peliculamodel[] = [];
  reservas: Reservamodel[] = [];
  edicion_borrado = false;
  ultima_reserva = 0;
  btnCrear = false;
  btnEditar = false;
  btnBorrar = false;

  constructor(private Datos: DatabaseService) { }

  ngOnInit(): void {      
    this.Datos.checkSesion("reserva");            
    this.Datos.getUsers().subscribe((data) => { 
      this.usuarios = data.filter(x => x.rol == "cliente");
    });
    this.Datos.getMovies().subscribe((data) => {      
      this.peliculas = data;
    });  
    this.actualizarInfo();
  }

  actualizarInfo(){
    this.Datos.getReservations().subscribe((data) => {      
      this.reservas = data;
      data.forEach((x) =>{
        if (parseInt(x.id_reserva) > this.ultima_reserva)
          this.ultima_reserva = parseInt(x.id_reserva);
      })
      this.reserva.id_reserva = String(this.ultima_reserva + 1); 
      this.reserva.entregado = "no";  
    });
  }

  crearReserva(f: NgForm){
    if (!f.valid)
      alert("Falta información, revisar los datos")                
    else                                      
      this.Datos.newReservation(this.reserva).subscribe((data)=>{
        if (data == null){
          alert("Se creó la reserva exitosamente")
          this.actualizarInfo();
          f.reset();
        }
        else
          alert("Ocurrio un error al crear la reserva")
    }); 
  }

  editarReserva(f: NgForm){
    if (!this.edicion_borrado) {
      this.edicion_borrado = true;    
      this.btnCrear = true;
      this.btnBorrar = true;
    }
    else{
      if (!f.valid)
        alert("Falta información, revisar los datos");
      else 
        this.Datos.editReservation(this.reserva).subscribe((data)=>{
          if (data == null){
            alert("Se editó la reserva exitosamente")            
          }
          else
            alert("Ocurrio un error al editar la reserva")
          f.reset();
          this.actualizarInfo();
          this.edicion_borrado = false;
          this.btnCrear = false;
          this.btnBorrar = false;
      });
    }
  }

  borrarReserva(f: NgForm){
    if (!this.edicion_borrado) {
      this.edicion_borrado = true;  
      this.btnCrear = true;
      this.btnEditar = true;
    }
    else{     
      this.Datos.deleteReservation(this.reserva.id_reserva).subscribe((data)=>{
        console.log(data);        
        if (data == null){
          alert("Se borró la reserva exitosamente")            
        }
        else
          alert("Ocurrio un error al borrar la reserva")
        f.reset();
        this.actualizarInfo();
        this.edicion_borrado = false;
        this.btnCrear = false;
        this.btnEditar = false;
      });
    }
  }



  selIdReserva(){
    let reserva_elegida : Reservamodel;
    reserva_elegida = this.reservas.filter(x => x.id_reserva == this.reserva.id_reserva)[0];
    this.reserva.id_usuario = reserva_elegida.id_usuario;
    this.reserva.id_pelicula = reserva_elegida.id_pelicula;
    this.reserva.entregado = reserva_elegida.entregado;
    const format = 'yyyy-MM-dd';    
    const locale = 'en-US';
    this.reserva.fecha_reserva = formatDate(reserva_elegida.fecha_reserva, format, locale);
  }


}
