import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Resena, resenaEnum } from '../../interface/resena';
import { ResenaService } from '../../service/resena.service';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-resena',
  standalone: true,
  imports: [HttpClientModule, RouterModule, RouterLink, FormsModule, CommonModule, MatCardModule, ReactiveFormsModule],
  templateUrl: './resena.component.html',
  styleUrl: './resena.component.css'
})
export class ResenaComponent implements OnInit {

  listadoResena: Resena[] = [];
  listadoResenaFiltrado: Resena[] = [];
  filtroIdResena: number = 0;
  filtroIdUsuario: number = 0;
  filtroNombreUsuario: string = '';
  page: number = 1;
  pageSize: number = 5;
  totalItems: number = 0;



  newResena: Resena = { resenaId: 0, usuarioId: 0, usuario: { usuarioNombre: '' }, comentario: '', calificacion:resenaEnum.Bueno , };
  selectedResena: Resena | null = null;

  usuario: string[] = []; // Llena con los nombres de productos disponibles
  resena :string[] = [];
  nomUser: string[] = [];

  constructor(private _ResenaService: ResenaService) {}

  ngOnInit(): void {
    this.obtenerResena();
    //this.cargarFiltros();
  }

  obtenerResena(): void {
    this. _ResenaService.getResena().subscribe({
      next: data => {
        console.log('Datos recibidos:', data);
        this.listadoResena = data;
        this.totalItems = data.length;
         // Llenar listas de opciones
        this.usuario = Array.from(new Set(data.map(inv => inv.usuario.usuarioNombre)));
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
    this.resena = [...new Set(this.listadoResena.map(inv => inv.resenaId.toString()))];
    this.usuario = [...new Set(this.listadoResena.map(inv => inv.usuarioId.toString()))];
    this.nomUser = [...new Set(this.listadoResena.map(inv => inv.usuario.usuarioNombre))];
  }

  addResena(): void {
    this._ResenaService.addResena(this.newResena).subscribe({
      next: () => {
        console.log('Inventario agregado exitosamente');
        this.obtenerResena();
        this.newResena = { resenaId: 0, usuarioId: 0, usuario: { usuarioNombre: '' }, comentario: '', calificacion:resenaEnum.Bueno};
        alert('Inventario agregado exitosamente');
      },
      error: error => {
        console.error('Inventario agregado exitosamente', error);
        alert('Inventario agregado exitosamente');
      }
    });
  }

  updateResena(): void {
    if (this.selectedResena) {
      this._ResenaService.modificarResena(this.selectedResena).subscribe({
        next: () => {
          console.log('Inventario actualizado exitosamente');
          this.obtenerResena();
          this.selectedResena = null;
          alert('Inventario actualizado exitosamente');
        },
        error: error => {
          console.error('Inventario actualizado exitosamente', error);
          alert('Inventario actualizado exitosamente');
        }
      });
    }
  }

  eliminarResumen(id?: number): void {
    if (id === undefined) {
      alert('El ID del Resumen es indefinido');
      return;
    }

    this._ResenaService.eliminarResena(id).subscribe({
      next: () => {
        console.log('Reseña eliminado');
        this.obtenerResena();
        alert('Reseña eliminado exitosamente');
      },
      error: error => {
        alert('Reseña eliminado exitosamente');
        console.error('Ocurrió un error al eliminar la Reseña:', error);
      },
      complete: () => {
        console.info('Eliminacion de Reseña completa');
      }
    });
  }

  filtrar(): void {
    this.listadoResenaFiltrado = this.listadoResena.filter(resena => {
      const cumpleResena = this.filtroIdResena === 0 || resena.resenaId <= this.filtroIdResena;
      const cumpleUsuario = this.filtroIdUsuario === 0 || resena.usuarioId <= this.filtroIdUsuario; // Ajusta aquí si deseas filtrar por máximo
      const cumpleNonUser = this.filtroNombreUsuario === '' || resena.usuario.usuarioNombre.toLowerCase().includes(this.filtroNombreUsuario.toLowerCase());

      return cumpleResena && cumpleUsuario && cumpleNonUser;
    });

    this.totalItems = this.listadoResenaFiltrado.length;
    this.page = 1;
  }


  get paginatedItems(): Resena[] {
    const start = (this.page - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.listadoResenaFiltrado.slice(start, end);
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  trackById(index: number, resena: Resena): number {
    return resena.resenaId ?? index;
  }

}
