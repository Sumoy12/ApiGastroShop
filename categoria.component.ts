import { Categoria } from './../../interface/categoria';

import { MatTable } from '@angular/material/table';
import { Component, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { MatPaginator } from '@angular/material/paginator';
import {MatFormFieldModule, MatHint} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import {MatSnackBar, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatSelectModule} from '@angular/material/select';
import {provideNativeDateAdapter} from '@angular/material/core';
import {MatIconModule} from '@angular/material/icon';

import { MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';

import { DatePipe } from '@angular/common';
import { CategoriaService } from '../../service/categoria.service';
import { OnInit } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-categoria',
  standalone: true,
  imports: [MatCardModule, MatTable, RouterOutlet, ReactiveFormsModule,
    MatSlideToggleModule, MatPaginator, MatCardModule, MatGridListModule, MatFormFieldModule, MatInputModule,
    MatButtonModule,  MatSelectModule, MatDatepickerModule, MatTableModule,
    DatePipe, MatIconModule, ReactiveFormsModule, HttpClientModule, RouterModule, RouterLink, FormsModule, CommonModule],
    providers: [provideNativeDateAdapter()],
  templateUrl: './categoria.component.html',
  styleUrl: './categoria.component.css'
})
export class CategoriaComponent implements OnInit {
  listadoCategoria: Categoria[] = [];
  listadoCategoriaFiltrado: Categoria[] = [];
  filtroNombre: string = '';
  page: number = 1;
  pageSize: number = 5;
  totalItems: number = 0;

  ingredientesCount: number = 0;
  condimentosCount: number = 0;
  acompanamientosCount: number = 0;
  bebidasCount: number = 0;

  newCategoria: Categoria = { categoriaId:0, categoriaNombre:'', descripcion:'', categoriaEstado: true};
  selectedCategoria: Categoria | null = null;


  nombre: string[] = [];   // Llena con los rangos de precios disponibles

  constructor(private _CategoriaService: CategoriaService) {}

  ngOnInit(): void {
    this.obtenerCategoria();
    this.cargarFiltros();
  }

  obtenerCategoria(): void {
    this._CategoriaService.getCategoria().subscribe({
      next: data => {
        console.log('Datos recibidos:', data);
        this.listadoCategoria = data;
        this.totalItems = data.length;
         // Llenar listas de opciones

        this.cargarFiltros();
        this.filtrar();
      },
      error: error => {
        console.error('Ocurrió un error al obtener los Inventarios:', error);
      },
      complete: () => {
        console.info('Obtención de Inventarios completa');
      }
    });
  }

  cargarFiltros(): void {
    // Cargar los valores posibles para los filtros (esto puede ser desde un servicio o una fuente de datos)
    // Ejemplo de carga estática, reemplazar con la carga dinámica según necesidad

    this.nombre = [...new Set(this.listadoCategoria.map(cat => cat.categoriaNombre))];
  }


  addCategoria(): void {
    this._CategoriaService.addCategoria(this.newCategoria).subscribe({
      next: () => {
        console.log('Categoria agregado exitosamente');
        this.obtenerCategoria();
        this.newCategoria = {categoriaId:0, categoriaNombre:'', descripcion:'', categoriaEstado: true};
        alert('Inventario agregado exitosamente');
      },
      error: error => {
        console.error('Ocurrió un error al agregar el inventario:', error);
        alert('Inventario agregado exitosamente');
      }
    });
  }

  updateInventario(): void {
    if (this.selectedCategoria) {
      this._CategoriaService.modificarCategoria(this.selectedCategoria).subscribe({
        next: () => {
          console.log('Inventario actualizado exitosamente');
          this.obtenerCategoria();
          this.selectedCategoria = null;
          alert('Inventario actualizado exitosamente');
        },
        error: error => {
          console.error('Ocurrió un error al actualizar el inventario:', error);
          alert('Inventario actualizado exitosamente');
        }
      });
    }
  }

  eliminarCategoria(id?: number): void {
    if (id === undefined) {
      alert('El ID de la categoria es indefinido');
      return;
    }

    this._CategoriaService.eliminarCategoria(id).subscribe({
      next: () => {
        console.log('Inventario eliminado');
        this.obtenerCategoria();
        alert('Categpria eliminado exitosamente');
      },
      error: error => {
        alert('Ocurrió un error al eliminar el Categoria');
        console.error('Ocurrió un error al eliminar el Categoria:', error);
      },
      complete: () => {
        console.info('Eliminación de Categoria completa');
      }
    });
  }

  filtrar(): void {
    this.listadoCategoriaFiltrado = this.listadoCategoria.filter(categoria => {
      const cumpleProducto = this.filtroNombre === '' || categoria.categoriaNombre.toLowerCase().includes(this.filtroNombre.toLowerCase());

      return cumpleProducto;
    });

    this.totalItems = this.listadoCategoriaFiltrado.length;
    this.page = 1;
  }


  get paginatedItems(): Categoria[] {
    const start = (this.page - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.listadoCategoriaFiltrado.slice(start, end);
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  trackById(index: number, categoria: Categoria): number {
    return categoria.categoriaId ?? index;
  }




}
