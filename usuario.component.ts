import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormsModule } from '@angular/forms';
import { RouterLink, RouterModule } from '@angular/router';
import { Usuario } from '../../interface/usuario';
import { UsuarioService } from '../../service/usuario.service';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-usuario',
  standalone: true,
  imports: [ HttpClientModule, RouterModule, RouterLink, FormsModule, CommonModule, MatCardModule
  ],
  templateUrl: './usuario.component.html',
  styleUrl: './usuario.component.css'
})
export class UsuarioComponent implements OnInit{

  listadoUsuario: Usuario[] = [];
  listadoUsuarioFiltrado: Usuario[] = [];
  page: number = 1;
  pageSize: number = 5;
  totalItems: number = 0;

  newUsuario: Usuario = { usuarioId: 0,usuarioCedula:'', usuarioNombre: '',usuarioApellido: '',usuarioEmail: '',usuarioUP:'',usuarioPassword: '',usuarioEstado: false};
  selectedUsuario: Usuario | null = null;

  constructor(private _UsuarioService: UsuarioService) {}

  ngOnInit(): void {
    this.obtenerUsuarios();
  }


  obtenerUsuarios(): void {
    this._UsuarioService.getUsuario().subscribe({
      next: data => {
        console.log('Datos recibidos:', data);
        this.listadoUsuario = data;
        this.totalItems = data.length;

      },
      error: error => {
        console.error('Ocurrió un error al obtener los Usuarios:', error);
      },
      complete: () => {
        console.info('Obtención de Usuarios completa');
      }
    });
  }

  eliminarUsuario(id?: number): void {
    if (id === undefined) {
      alert('El ID del Usuario es indefinido');
      return;
    }

    this._UsuarioService.eliminarUsuario(id).subscribe({
      next: () => {
        console.log('Usuario eliminado');
        this.obtenerUsuarios();
        alert('Usuario eliminado exitosamente');
      },
      error: error => {
        alert('Ocurrió un error al eliminar el Usuario');
        console.error('Ocurrió un error al eliminar el Usuario:', error);
      },
      complete: () => {
        console.info('Eliminación de Usuario completa');
      }
    });
  }

  updateUsuario(): void {
    if (this.selectedUsuario) {
      this._UsuarioService.modificarUsuario(this.selectedUsuario).subscribe({
        next: () => {
          console.log('Usuario actualizado exitosamente');
          this.obtenerUsuarios();
          this.selectedUsuario = null;
          alert('Usuario actualizado exitosamente');
        },
        error: error => {
          console.error('Ocurrió un error al actualizar el Usuario:', error);
          alert('Ocurrió un error al actualizar el Usuario');
        }
      });
    }
  }

  addUsuario(): void {
    this._UsuarioService.addUsuario(this.newUsuario).subscribe({
      next: () => {
        console.log('Usuario agregado exitosamente');
        this.obtenerUsuarios();
        this.newUsuario = {
          usuarioId: 0,
          usuarioCedula: '',
          usuarioNombre: '',
          usuarioApellido: '',
          usuarioEmail: '',
          usuarioUP:'',
          usuarioPassword: '',
          usuarioEstado: false
        };
        alert('Usuario agregado exitosamente');
      },
      error: error => {
        console.error('Ocurrió un error al agregar el Usuario:', error);
        alert('Ocurrió un error al agregar el Usuario');
      }
    });
  }


  get paginatedItems(): Usuario[] {
    const start = (this.page - 1) * this.pageSize;
    const end = start + this.pageSize;
    return this.listadoUsuarioFiltrado.slice(start, end);
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  trackById(index: number, Usuario: Usuario): number {
    return Usuario.usuarioId ?? index;
  }


}
