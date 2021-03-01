import { Injectable } from '@angular/core';
import { Peliculamodel, Reservamodel, Usuariomodel } from '../modelos/modelos.module';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, Subject} from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class DatabaseService {

  respuesta: any;
  constructor(private http: HttpClient, private router: Router) { }

  getUsers(): Observable<Usuariomodel[]> {
    return this.http.get<Usuariomodel[]>('https://localhost:44308/usuario');
  }

  getMovies(): Observable<Peliculamodel[]> {
    return this.http.get<Peliculamodel[]>('https://localhost:44308/pelicula');
  }

  getReservations(): Observable<Reservamodel[]> {
    return this.http.get<Reservamodel[]>('https://localhost:44308/reserva');
  }

  getUser(id: string,password: string): Observable<Usuariomodel> {
    return this.http.get<Usuariomodel>('https://localhost:44308/usuario/?id='+id+"&password="+password);
  }

  newUser(usuario : Usuariomodel): Observable<any> {     
    return this.http.post<any>('https://localhost:44308/usuario', usuario);
  }

  newReservation(reserva : Reservamodel): Observable<any> {     
    return this.http.post<any>('https://localhost:44308/reserva', reserva);
  }

  newMovie(pelicula : Peliculamodel): Observable<any> {     
    return this.http.post<any>('https://localhost:44308/pelicula', pelicula);
  }

  editReservation(reserva : Reservamodel): Observable<any> {     
    return this.http.put<any>('https://localhost:44308/reserva', reserva);
  }

  editMovie(pelicula : Peliculamodel): Observable<any> {     
    return this.http.put<any>('https://localhost:44308/pelicula', pelicula);
  }

  deleteReservation(id : string): Observable<any> {     
    return this.http.delete<any>('https://localhost:44308/reserva/?id_reserva='+ id);
  }

  deleteMovie(id : string): Observable<any> {     
    return this.http.delete<any>('https://localhost:44308/pelicula/?id='+ id);
  }

  checkUser(user: Usuariomodel): Observable<boolean>{
    let key: any;
    let exist = false;
    let res = new Subject<boolean>();     
    this.getUsers().subscribe((data) => {      
      for (key in data) {     
        if (user.id == data[key].id) {
          exist = true;
        }
      }         
      if (exist)
        res.next(true);
      else
        res.next(false);
    });   
    return res;  
  }


  login(user: Usuariomodel) {
    this.getUser(user.id,user.password).subscribe((data) => {      
      if (data != null){
        console.log(data);
        localStorage.setItem('usuario', data.id);          
        localStorage.setItem('nombre', data.nombre);       
        localStorage.setItem('rol', data.rol);
        this.router.navigate(['reserva']);    
      }
      else
        alert('Correo ó contraseña equivocada');      
    });
  }

  checkSesion(ruta : string){
    let key: any;    
    let usuario_log = localStorage.getItem('usuario');
    let nombre_log = localStorage.getItem('nombre');
    let rol_log = localStorage.getItem('rol');
    if (usuario_log == null)
      if (ruta == 'registro')
        this.router.navigate(['registro']);
      else
        this.router.navigate(['login']);
    else{
      this.getUsers().subscribe((data) => {      
        for (key in data) {     
          if (usuario_log == data[key].id && nombre_log == data[key].nombre) {
            if (rol_log == "Administrador"){
              if (ruta == "pelicula")
                this.router.navigate(['pelicula']);
              else  
                this.router.navigate(['reserva']);
            }
            else
              this.router.navigate(['reserva']);
          }
        }     
      });
    }
  }  

  logout(){
    localStorage.clear();
    this.router.navigate(['login']);
  }

}