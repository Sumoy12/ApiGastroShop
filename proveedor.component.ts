import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Proveedor } from '../../interface/proveedor';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ProveedorService

 } from '../../service/proveedor.service';
@Component({
  selector: 'app-Proveedor',
  templateUrl: './Proveedor.component.html',
  standalone: true,
  imports: [
    HttpClientModule, RouterModule, RouterLink, FormsModule, CommonModule, ReactiveFormsModule
  ],
  providers: [ProveedorService],
  styleUrls: ['./proveedor.component.css']
})
export class ProveedorComponent implements OnInit {
  listadoProveedor: Proveedor[] = [];
  listadoProveedorFiltrado: Proveedor[] = [];

  filtroTelefono: string = '';
  filtroNombre: string = '';
  filtroEstado: boolean = false; // Inicialmente el checkbox está desmarcado

  //Cambiar a Activo y Inactivo en la tabla (Solo Vista)
  getEstadoDescripcion(estado: boolean): string {
    return estado ? 'Activo' : 'Inactivo';
  }


  page: number = 1;
  pageSize: number = 5;
  totalItems: number = 0;

  ingredientesCount: number = 0;
  condimentosCount: number = 0;
  acompanamientosCount: number = 0;
  bebidasCount: number = 0;

  newProveedor: Proveedor = { proveedorId: 0, nombre: '', email: '', telefono: '', estado: false};
  selectedProveedor: Proveedor | null = null;

  telefonos: string[] = [];
  nombres: string[] = [];
  estados: string[] = [];

  constructor(private _ProveedorService: ProveedorService) {}

  ngOnInit(): void {
    this.obtenerProveedors();
    this.cargarFiltros();
  }

  obtenerProveedors(): void {
    this._ProveedorService.getProveedors().subscribe({
      next: data => {
        console.log('Datos recibidos:', data);
        this.listadoProveedor = data;
        this.totalItems = data.length;
        this.cargarFiltros();
        this.filtrar();
      },
      //error: error => {
      //  console.error('Ocurrió un error al obtener los Proveedors:', error);
      //},
      complete: () => {
        console.info('Obtención de Proveedors completa');
      }
    });
  }

  cargarFiltros(): void {
    // Cargar los valores posibles para los filtros (esto puede ser desde un servicio o una fuente de datos)
    // Ejemplo de carga estática, reemplazar con la carga dinámica según necesidad
    this.estados = [...new Set(this.listadoProveedor.map(pro => pro.estado.toString()))];
    this.nombres = [...new Set(this.listadoProveedor.map(pro => pro.nombre))];
    this.telefonos = [...new Set(this.listadoProveedor.map(pro => pro.telefono))];
  }

  addProveedor(): void {
    this._ProveedorService.addProveedor(this.newProveedor).subscribe({
      next: () => {
        console.log('Proveedor agregado exitosamente');
        this.obtenerProveedors();
        this.newProveedor = { proveedorId: 0, nombre: '', email: '', telefono: '', estado: false};
        alert('Proveedor agregado exitosamente');
      },
      error: error => {
        console.error('Ocurrió un error al agregar el Proveedor:', error);
        alert('Proveedor agregado exitosamente');
      }
    });
  }

  updateProveedor(): void {
    if (this.selectedProveedor) {
      this._ProveedorService.modificarProveedor(this.selectedProveedor).subscribe({
        next: () => {
          console.log('Proveedor actualizado exitosamente');
          this.obtenerProveedors();
          this.selectedProveedor = null;
          alert('Proveedor actualizado exitosamente');
        },
        error: error => {
          console.error('Ocurrió un error al actualizar el Proveedor:', error);
          alert('Proveedor actualizado exitosamente');
        }
      });
    }
  }

  eliminarProveedor(id?: number): void {
    if (id === undefined) {
      alert('El ID del Proveedor es indefinido');
      return;
    }

    this._ProveedorService.eliminarProveedor(id).subscribe({
      next: () => {
        console.log('Proveedor eliminado');
        this.obtenerProveedors();
        alert('Proveedor eliminado exitosamente');
      },
      error: error => {
        alert('Proveedor eliminado exitosamente');
        console.error('Ocurrió un error al eliminar el Proveedor:', error);
      },
      complete: () => {
        console.info('Eliminación de Proveedor completa');
      }
    });
  }


  filtrar(): void {
    this.listadoProveedorFiltrado = this.listadoProveedor.filter(proveedor => {
      const cumpleNombre = this.filtroNombre === '' || proveedor.nombre.toLowerCase().includes(this.filtroNombre.toLowerCase());
      const cumpleTelefono = this.filtroTelefono === '' || proveedor.telefono.toLowerCase().includes(this.filtroTelefono.toLowerCase());
      const cumpleEstado = this.filtroEstado ? proveedor.estado === true : true; // Ajusta según tu lógica

      return cumpleNombre && cumpleTelefono && cumpleEstado;
    });
    this.totalItems = this.listadoProveedorFiltrado.length;
    this.page = 1;
  }

  get paginatedItems(): Proveedor[] {
    const start = (this.page - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.listadoProveedorFiltrado.slice(start, end);
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  trackById(index: number, Proveedor: Proveedor): number {
    return Proveedor.proveedorId ?? index;
  }

}
