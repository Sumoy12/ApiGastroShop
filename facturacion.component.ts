
import { Usuario } from './../../interface/usuario';
import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
// import { RegistroComponent } from '../registro/registro.component';
import {MatSelectModule} from '@angular/material/select';
import { CommonModule } from '@angular/common';
import {MatInputModule} from '@angular/material/input';
import { Facturacion } from '../../interface/facturacion';
import { FacturacionService } from '../../service/facturacion.service';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatGridListModule} from '@angular/material/grid-list';
import { RouterLink, RouterOutlet } from '@angular/router';

import { DialogoComponent } from '../../componenete/dialogo/dialogo.component';


@Component({
  selector: 'app-facturacion',
  standalone: true,
  imports: [MatFormFieldModule, CommonModule, FormsModule, MatInputModule,
    MatSelectModule, MatIconModule, MatCardModule,MatGridListModule, ReactiveFormsModule, RouterLink, RouterOutlet,],
  templateUrl: './facturacion.component.html',
  styleUrl: './facturacion.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FacturacionComponent implements OnInit {
  //facturacion: Facturacion = {
    //facturaId: 0,
    //usuarioId: 0,
    //compraId: 0,
    //email: 'juan.perez@example.com',
    //direccion: '123 Calle Falsa',
    //telefono: '555-1234',
    //producto: 'Pizza',
    //cantidad: 2,
    //precio: 10.00,
    //monto: 20.00,
    //metododepago: 'Tarjeta de Crédito',
    //iva: 4.00,
    //estado: 'Pendiente'
  //};


  ngOnInit(): void {
    // Aquí podrías cargar los datos de la factura desde un servicio
  }

  realizarPago(): void {
    // Lógica para realizar el pago
    console.log('Realizando el pago...');
    // Puedes redirigir al usuario a una página de confirmación o mostrar un mensaje
  }
  readonly dialog = inject(MatDialog);

  openDialog() {
    this.dialog.open(DialogoComponent);
  }

}

