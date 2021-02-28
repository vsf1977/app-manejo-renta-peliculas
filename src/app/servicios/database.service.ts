import { Injectable } from '@angular/core';
import { Usuariomodel } from '../modelos/modelos.module';
import { HttpClient } from '@angular/common/http';
import { Observable} from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class DatabaseService {

  respuesta: any;
  constructor(private http: HttpClient, private router: Router) { }

  getUsers(): Observable<Usuariomodel[]> {
    return this.http.get<Usuariomodel[]>('https://localhost:44308/usuario');
  }

  checkUser(user: Usuariomodel) {
    let login = false;
    let key: any;
    this.getUsers().subscribe((data) => {      
      for (key in data) {     
        if (user.id == data[key].id && user.password == data[key].password) {
          localStorage.setItem('usuario', user.id);          
          localStorage.setItem('nombre', data[key].nombre);          
          this.router.navigate(['main']);                  
          login = true;
        }
      }
      if (!login) {
        alert('Correo ó contraseña equivocada');
      }
    });
  }

  checkSesion(){
    let key: any;
    let usuario_log = localStorage.getItem('usuario');
    let nombre_log = localStorage.getItem('nombre');
    this.getUsers().subscribe((data) => {      
      for (key in data) {     
        if (usuario_log == data[key].id && nombre_log == data[key].nombre) {
          this.router.navigate(['main']);
        }
      }     
    });
  }



/*
  getProducts(): Observable<Productmodel[]> {
    return this.http.get<Productmodel[]>('https://prueba-1a6f0.firebaseio.com/productos/.json');
  }

  deleteProduct(key: string) {
    // tslint:disable-next-line: max-line-length
    return this.http.delete('https://prueba-1a6f0.firebaseio.com/productos/' + key + '/.json');
}

  getCountry(): Observable<any>{
    return this.http.get<any>('https://restcountries.eu/rest/v2/all');
  }

  saveProduct(producto: Productmodel): Observable<number> {
    const datos = JSON.stringify(producto);
    return this.http.post<number>('https://prueba-1a6f0.firebaseio.com/productos/.json', datos);
  }

  editProduct(producto: Productmodel, key: string): Observable<number> {
    const datos = JSON.stringify(producto);
    return this.http.put<number>('https://prueba-1a6f0.firebaseio.com/productos/' + key + '/.json', datos);
  }

  getUrlImage(id: string) {
    return this.storage.ref(id).getDownloadURL();
  }*/

}