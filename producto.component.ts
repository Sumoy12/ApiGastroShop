import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [FormsModule, MatCardModule, MatTableModule, CommonModule, ReactiveFormsModule],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.css'
})
export class ProductoComponent {

  filtroNombre: string = '';
  page: number = 1;
  totalPages: number = 1;
  paginatedItems: any[] = [];
  selectedProducto: any = {};
  newProducto: any = {};


  filtrar(): void {

  }


  addProducto(): void {

  }


  updateProducto(): void {

  }


  eliminarProducto(productoId: number): void {

  }


  trackById(index: number, item: any): number {
    return item.productoId;
  }
}


