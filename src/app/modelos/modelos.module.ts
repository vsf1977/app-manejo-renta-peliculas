import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})

export class Usuariomodel {
  
  nombre : string
  rol : string
  id: string
  password: string
  
}

export class Peliculamodel {
  
  id : string
  titulo : string
  descripcion: string
  director: string
  costo_alquiler : string
  cantidad : string
  actores: string
  
}

export class Reservamodel {
  
  id_reserva : string
  id_pelicula  : string
  id_usuario: string
  fecha_reserva: string
  entregado : string
  
}
