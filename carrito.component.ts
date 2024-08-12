import { CarritoService } from './../../service/carrito.service';
import { Carrito, EstadoCarritoEnum } from './../../interface/carrito';
import { Component, OnInit } from '@angular/core';

import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';

import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';

import 'sweetalert2/dist/sweetalert2.css';
import Swal from 'sweetalert2';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { parse } from 'path';
import { Categoria } from '../../interface/categoria';
import { requestCarrito } from '../../interface/requestCarrito';


@Component({
  selector: 'app-carrito',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    MatCardModule,
    MatGridListModule,
    CurrencyPipe,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatDialogModule,
    RouterLink,
    CommonModule,
  ],
  templateUrl: './carrito.component.html',
  styleUrl: './carrito.component.css',
})
export class CarritoComponent implements OnInit {
  valorCompra: number = 0.0;
  productoId: number = 0;
  orderCarritoImp: number = 0;
  orderCarritoImpStr: string = '';

  ELEMENT_DATA: Carrito[] = [];

  dataSource: Carrito[] = [];

  CARRITO_OBJ: Carrito = {
    carritoId: 0,
    productoId: 0,
    productoNombre: '',
    precioCarrito: 0.0,
    estadoCarritoEnum: EstadoCarritoEnum.Activo,
    cantidad: 1,
  };

  categoria:Categoria ={
    categoriaId: 0,
    categoriaNombre: '',
    descripcion: '',
    categoriaEstado: true
  }

  request_: requestCarrito ={
    productoId: 0,
    categoriaId: 0,
    categoria : this.categoria,
    productoNombre : '',
    productoDescripcion : '',
    productoPrecio: 0,
    rutaImg: '',
    disponibilidad: 0

  }

  constructor(
    private activatedroute: ActivatedRoute,
    private router: Router,
    private Carrito_Service: CarritoService
  ) {}

  ngOnInit(): void {
    if (sessionStorage.getItem('orderSession') == undefined) {

       this.Carrito_Service.getCarritos().subscribe(dato=> {
        let indice = dato.length - 1;
        let order =  dato[indice].carritoId;
        if (order != undefined) {
          this.orderCarritoImp = order + 1;
        }
        sessionStorage.setItem('orderSession', this.orderCarritoImp.toString());
       });

    } else {
      if (sessionStorage.getItem('orderSession') != undefined) {
        let dato = sessionStorage.getItem('orderSession');
        this.orderCarritoImpStr = dato!.toString();
        this.orderCarritoImp = parseInt(this.orderCarritoImpStr);
      }
    }

    this.activatedroute.paramMap.subscribe({
      next: (params) => {
        let dat = params.get('id');
        if (dat != undefined) {
          this.productoId = parseInt(dat.toString());
        }
      },
    });

    if (this.productoId == 0) {
      this.valorCompra = this.Carrito_Service.valorTotalCarrito();
      if (this.valorCompra  <= 0) {
        this.router.navigate(['/catalogo']);
      }else{
        this.mostrarCarrito();
      }




    } else {
      if (this.Carrito_Service.validarExisteCarrtito(this.productoId)) {
        //this.totalCompra(this.productoId, 1);
        this.valorCompra = this.Carrito_Service.valorTotalCarrito();
        this.mostrarCarrito();
      } else {
        let datosSession = sessionStorage.getItem('carrito')?.toString();
        if (datosSession) {
          let d = JSON.parse(datosSession!.toString());
          this.CARRITO_OBJ = {
            carritoId: this.orderCarritoImp,
            productoId: d.productoId,
            productoNombre: d.productoNombre,
            precioCarrito: d.precioCarrito,
            estadoCarritoEnum: 0,
            cantidad: 1,
          };
        }

        this.Carrito_Service.addListaCarrito(this.CARRITO_OBJ);
        this.valorCompra = this.Carrito_Service.valorTotalCarrito();
        this.mostrarCarrito();
        sessionStorage.removeItem('carrito');
      }
    }
  }

  totalCompra(producto: number, flat: number): void {
    if (producto < 0) {
      this.ELEMENT_DATA = this.Carrito_Service.getListaCarrito();
      this.ELEMENT_DATA.forEach((x) => {
        this.valorCompra = this.valorCompra + x.precioCarrito;
        let dato = this.valorCompra.toFixed(2);
        this.valorCompra = parseFloat(dato);
      });
    } else {
      if (flat == 1) {
        this.ELEMENT_DATA = this.Carrito_Service.getListaCarrito();
      }

      this.ELEMENT_DATA.forEach((x) => {
        if (flat == 1) {
          if (x.productoId == producto) {
            this.valorCompra = this.valorCompra + x.precioCarrito;
            let dato = this.valorCompra.toFixed(2);
            this.valorCompra = parseFloat(dato);
          }
        }

        if (flat == 2) {
          if (x.productoId == producto) {
            this.valorCompra = this.valorCompra - x.precioCarrito;
            let dato = this.valorCompra.toFixed(2);
            this.valorCompra = parseFloat(dato);
          }
        }

        if (flat == 3) {
          if (x.productoId == producto) {
            let val = x.precioCarrito * x.cantidad;
            this.valorCompra = val;
            let dato = this.valorCompra.toFixed(2);
            this.valorCompra = parseFloat(dato);
          }
        }
      });
    }
  }

  displayedColumns: string[] = [
    'carritoId',
    'productoId',
    'productoNombre',
    'precioCarrito',
    'estadoCarritoEnum',
    'cantidad',
    'subtotal',
    'accion',
  ];
  swalWithBootstrapButtons = Swal.mixin({
    customClass: {
      confirmButton: 'btn btn-success',
      cancelButton: 'btn btn-danger',
    },
    buttonsStyling: false,
  });

  mostrarCarrito(): void {
    this.dataSource = this.Carrito_Service.getListaCarrito();
  }

  agregarProducto(cantidadParm: number, id: number): void {
    let cantidadActual = 0;

    this.dataSource.forEach((car) => {
      if (car.productoId == id) {
        car.cantidad += 1;
        cantidadActual = car.cantidad;
      }
    });
    this.Carrito_Service.updateCantidad(cantidadActual, id);
    this.mostrarCarrito();
    this.totalCompra(id, 1);
  }

  reducirProducto(cantidadParm: number, id: number): void {
    if (cantidadParm > 1) {
      this.ELEMENT_DATA.forEach((car) => {
        if (car.productoId == id) {
          car.cantidad -= 1;
        }
      });
      this.mostrarCarrito();
      this.totalCompra(id, 2);
    } else {
      this.swalWithBootstrapButtons
        .fire({
          title: 'Eliminar',
          text: 'Está usted seguro de elimiar el pedido?',
          icon: 'warning',
          showCancelButton: true,
          confirmButtonText: 'delete',
          cancelButtonText: 'cancel!',
          reverseButtons: true,
        })
        .then((result) => {
          if (result.isConfirmed) {
            this.ELEMENT_DATA = this.dataSource;
            this.dataSource = this.Carrito_Service.deleteCarritoLista(
              cantidadParm,
              id
            );
            let total = this.dataSource.length;

            if (total == 0) {
              this.router.navigate(['/catalogo']);
            } else {
              this.mostrarCarrito();
              this.totalCompra(id, 2);
              this.swalWithBootstrapButtons.fire({
                title: 'Deleted!',
                text: 'Your file has been deleted.',
                icon: 'success',
              });
            }
          } else if (result.dismiss === Swal.DismissReason.cancel) {
          }
        });
    }
  }

  pagarCarrito(datos: Carrito[], request:requestCarrito): void {
    console.log(datos);
    console.log(request);


    datos.forEach(carrito =>{


      request = {
        productoId: carrito.productoId,
        categoriaId: 0,
        categoria : this.categoria,
        productoNombre : carrito.productoNombre,
        productoDescripcion : '',
        productoPrecio: carrito.precioCarrito,
        rutaImg: '',
        disponibilidad: 1,
      }



      this.Carrito_Service.createCarrito(carrito,request);

    });
    this.router.navigate(['/facturacion']);
  }

  eliminarCarrito(datos: any) {
    this.swalWithBootstrapButtons
      .fire({
        title: 'Eliminar',
        text: 'Está usted seguro de elimiar todo el pedido?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'delete',
        cancelButtonText: 'cancel!',
        reverseButtons: true,
      })
      .then((result) => {
        if (result.isConfirmed) {

          this.dataSource = this.Carrito_Service.deleteCarritoTodo();
          sessionStorage.clear();

          this.router.navigate(['/catalogo']);
        } else if (result.dismiss === Swal.DismissReason.cancel) {
        }
      });
  }
}
