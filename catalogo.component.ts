import { Component, OnInit } from '@angular/core';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatGridListModule} from '@angular/material/grid-list';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Producto } from '../../interface/producto';
import { ProductoService } from '../../service/producto.service';
import { CommonModule, NgOptimizedImage } from '@angular/common';
import { Carrito } from '../../interface/carrito';


@Component({
  selector: 'app-catalogo',
  standalone: true,
  imports: [CommonModule,   MatFormFieldModule, MatInputModule, MatSelectModule, MatIconModule,
    MatCardModule,MatGridListModule, ReactiveFormsModule, RouterLink, RouterOutlet, NgOptimizedImage ],
  templateUrl: './catalogo.component.html',
  styleUrl: './catalogo.component.css'
})
export class CatalogoComponent implements OnInit {
  // imagenes asignada a variable
  urlimg1 = 'assets/img/catalogo1.png';
  urlimg2 = 'assets/img/catalogo2.png';
  urlimg3 = 'assets/img/catalogo3.png';
  urlimg4 = 'assets/img/catalogo4.png';
  urlimg5 = 'assets/img/catalogo5.png';
  urlimg6 = 'assets/img/catalogo6.png';
  //
   // lista de productos
   productos:Producto[]=[];
  // producto
   productoObj:Producto ={
    productoId: 0,
    categoriaId: 0,
    productoNombre : '',
    productoDescripcion : '',
    productoPrecio: 0,
    rutaImg: '',
    disponibilidad: 0,
   };

   carrito:Carrito={
    carritoId: 0,
    productoId: 0,
    productoNombre: '',
    precioCarrito: 0,
    estadoCarritoEnum: 0,
    cantidad:1
   }
   listCarrito:Carrito[]=[];

   dato_session:string='';






  constructor( private productoService_:ProductoService, private router:Router){}

  ngOnInit(): void {

    this.getObtenerProductos();

  }

  getObtenerProductos():Producto[]{
     this.productoService_.getProductos().subscribe((data) => {
          console.log(data);
          this.productos=data;
     });
     return this.productos;
  }

  AgregarCarrito(dato:Producto):void{
      console.log(dato);

      this.carrito = {
        carritoId : 0,
        productoId: dato.productoId,
        productoNombre:dato.productoNombre,
        precioCarrito:dato.productoPrecio,
        estadoCarritoEnum:0,
        cantidad:1
      }


      if (window.sessionStorage.getItem('carritoSession') == undefined) {
        let au =self.crypto.randomUUID();
        sessionStorage.setItem("carritoSession",au);
      }
      console.log(this.carrito);
      this.listCarrito.push(this.carrito);
      var lista = JSON.stringify(this.listCarrito);
      sessionStorage.setItem("carritoList",lista);

      var carJson = JSON.stringify(this.carrito);
      sessionStorage.setItem("carrito",carJson);
      this.router.navigate(['/carrito',this.carrito.productoId])



  }

}
