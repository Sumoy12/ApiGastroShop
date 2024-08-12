import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Inventario } from '../../interface/inventario';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { InventarioService } from '../../service/inventario.service';
@Component({
  selector: 'app-inventario',
  templateUrl: './inventario.component.html',
  standalone: true,
  imports: [
    HttpClientModule, RouterModule, RouterLink, FormsModule, CommonModule, ReactiveFormsModule
  ],
  providers: [InventarioService],
  styleUrls: ['./inventario.component.css']
})
export class InventarioComponent implements OnInit {
  listadoInventario: Inventario[] = [];
  listadoInventarioFiltrado: Inventario[] = [];
  filtroNombre: string = '';
  filtroProducto: string = '';
  filtroCantidad: number = 0;
  filtroProveedor: string = '';
  page: number = 1;
  pageSize: number = 5;
  totalItems: number = 0;

  ingredientesCount: number = 0;
  condimentosCount: number = 0;
  acompanamientosCount: number = 0;
  bebidasCount: number = 0;

  newInventario: Inventario = { inventarioId: 0, productoId: 0, producto: { productoNombre: '' }, cantidad: 0, precio: 0, tipo: '', estado: '', proveedorId: 0, proveedor: { nombre: '' } };
  selectedInventario: Inventario | null = null;

  productos: string[] = []; // Llena con los nombres de productos disponibles
  precios: string[] = [];   // Llena con los rangos de precios disponibles
  proveedores: string[] = []; // Llena con los nombres de proveedores disponibles

  constructor(private _InventarioService: InventarioService) {}

  ngOnInit(): void {
    this.obtenerInventarios();
    this.cargarFiltros();
  }

  obtenerInventarios(): void {
    this._InventarioService.getInventarios().subscribe({
      next: data => {
        console.log('Datos recibidos:', data);
        this.listadoInventario = data;
        this.totalItems = data.length;
         // Llenar listas de opciones


        this.proveedores = Array.from(new Set(data.map(inv => inv.proveedor.nombre)));



        this.cargarFiltros();
        this.filtrar();
        this.updateCounts();
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
    this.productos = [...new Set(this.listadoInventario.map(inv => inv.producto.productoNombre))];
    this.precios = [...new Set(this.listadoInventario.map(inv => inv.precio.toString()))];
    this.proveedores = [...new Set(this.listadoInventario.map(inv => inv.proveedor.nombre))];
  }

  updateCounts(): void {
    this.ingredientesCount = this.listadoInventario.filter(inv => inv.tipo === 'Ingrediente').length;
    this.condimentosCount = this.listadoInventario.filter(inv => inv.tipo === 'Condimento').length;
    this.acompanamientosCount = this.listadoInventario.filter(inv => inv.tipo === 'Acompañamiento').length;
    this.bebidasCount = this.listadoInventario.filter(inv => inv.tipo === 'Bebida').length;
  }

  addInventario(): void {
    this._InventarioService.addInventario(this.newInventario).subscribe({
      next: () => {
        console.log('Inventario agregado exitosamente');
        this.obtenerInventarios();
        this.newInventario = { inventarioId: 0, productoId: 0, producto: { productoNombre: '' }, cantidad: 0, precio: 0, tipo: '', estado: '', proveedorId: 0, proveedor: { nombre: '' } };
        alert('Inventario agregado exitosamente');
      },
      error: error => {
        console.error('Ocurrió un error al agregar el inventario:', error);
        alert('Inventario agregado exitosamente');
      }
    });
  }

  updateInventario(): void {
    if (this.selectedInventario) {
      this._InventarioService.modificarInventario(this.selectedInventario).subscribe({
        next: () => {
          console.log('Inventario actualizado exitosamente');
          this.obtenerInventarios();
          this.selectedInventario = null;
          alert('Inventario actualizado exitosamente');
        },
        error: error => {
          console.error('Ocurrió un error al actualizar el inventario:', error);
          alert('Inventario actualizado exitosamente');
        }
      });
    }
  }

  eliminarInventario(id?: number): void {
    if (id === undefined) {
      alert('El ID del Inventario es indefinido');
      return;
    }

    this._InventarioService.eliminarInventario(id).subscribe({
      next: () => {
        console.log('Inventario eliminado');
        this.obtenerInventarios();
        alert('Inventario eliminado exitosamente');
      },
      error: error => {
        alert('Inventario eliminado exitosamente');
        console.error('Ocurrió un error al eliminar el Inventario:', error);
      },
      complete: () => {
        console.info('Eliminación de Inventario completa');
      }
    });
  }

  filtrar(): void {
    this.listadoInventarioFiltrado = this.listadoInventario.filter(inventario => {
      const cumpleProducto = this.filtroNombre === '' || inventario.producto.productoNombre.toLowerCase().includes(this.filtroNombre.toLowerCase());
      const cumpleCantidad = this.filtroCantidad === 0 || inventario.cantidad <= this.filtroCantidad; // Ajusta aquí si deseas filtrar por máximo
      const cumpleProveedor = this.filtroProveedor === '' || inventario.proveedor.nombre.toLowerCase().includes(this.filtroProveedor.toLowerCase());
      const cumpleNombre = this.filtroNombre === '' || inventario.producto.productoNombre.toLowerCase().includes(this.filtroNombre.toLowerCase());

      return cumpleProducto && cumpleCantidad && cumpleProveedor && cumpleNombre;
    });

    this.totalItems = this.listadoInventarioFiltrado.length;
    this.page = 1;
  }


  get paginatedItems(): Inventario[] {
    const start = (this.page - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.listadoInventarioFiltrado.slice(start, end);
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  trackById(index: number, inventario: Inventario): number {
    return inventario.inventarioId ?? index;
  }
}
