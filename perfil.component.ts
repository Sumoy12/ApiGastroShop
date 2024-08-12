import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { Perfil } from '../../interface/perfil';
import { MatCardModule } from '@angular/material/card';
import { MatTable } from '@angular/material/table';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';


import { HttpClientModule } from '@angular/common/http';
import { RouterLink, RouterModule } from '@angular/router';

import {MatFormFieldModule} from '@angular/material/form-field';

import { FormsModule } from '@angular/forms';
import { PerfilService } from '../../service/perfil.service';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [MatCardModule, MatTable, CommonModule, ReactiveFormsModule, RouterLink, RouterModule, MatFormFieldModule, CommonModule,FormsModule, HttpClientModule],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css'
})
export class PerfilComponent implements OnInit {
  perfiles: Perfil[] = [];
  filteredPerfiles: Perfil[] = [];
  filtroId: number | null = null;
  filtroNombre: string = '';
  filtroUsuario: string = '';
  newPerfil: Perfil = { perfilId: 0, nombrePerfil: '', apellidosPerfil: '', emailPerfil: '', usuarioPerfil: '',   passwordPerfil: '', estadoPerfil: true}; // Inicializar newPerfil

  editPerfil: Perfil = { perfilId: 0, nombrePerfil: '', apellidosPerfil: '', emailPerfil: '', usuarioPerfil: '', passwordPerfil: '', estadoPerfil: true };
  editMode: boolean = false;

  // Método para seleccionar un perfil para editar
  selectPerfil(perfil: Perfil): void {
    this.editPerfil = { ...perfil }; // Clonar el perfil para edición
    this.editMode = true; // Activar el modo de edición
  }

  constructor(private perfilService: PerfilService) {}


  ngOnInit(): void {
    this.loadPerfiles();
  }

  loadPerfiles(): void {
    this.perfilService.getPerfiles().subscribe(data => {
      this.perfiles = data;
      this.filtrar();
    });
  }


  filtrar(): void {
    this.filteredPerfiles = this.perfiles.filter(perfil =>
      (this.filtroId === null || perfil.perfilId === this.filtroId) &&
      (this.filtroNombre === '' || perfil.nombrePerfil.toLowerCase().includes(this.filtroNombre.toLowerCase())) &&
      (this.filtroUsuario === '' || perfil.usuarioPerfil.toLowerCase().includes(this.filtroUsuario.toLowerCase()))
    );
  }

  onSearchIdChange(event: Event): void {
    const inputValue = (event.target as HTMLInputElement).value;
    this.filtroId = inputValue ? Number(inputValue) : null;
    this.filtrar();
  }

  onSearchNombreChange(event: Event): void {
    const inputValue = (event.target as HTMLInputElement).value;
    this.filtroNombre = inputValue;
    this.filtrar();
  }

 onSearchUsuarioChange(event: Event): void {
  const inputValue = (event.target as HTMLInputElement).value;
  this.filtroUsuario = inputValue;
  this.filtrar();
  }

  // Métodos para crear, modificar y eliminar perfiles (deberías implementar formularios para cada acción)
  createPerfil(perfil: Perfil): void {
    this.perfilService.createPerfil(perfil).subscribe(() => this.loadPerfiles());
  }

  updatePerfil(id: number, perfil: Perfil): void {
    this.perfilService.updatePerfil(id, perfil).subscribe(() => {
      this.loadPerfiles();
    });
  }

  deletePerfil(id: number): void {
    if (id === undefined) {
      alert('El ID del Inventario es indefinido');
      return;
    }

    this.perfilService.deletePerfil(id).subscribe({
      next: () => {
        console.log('Perfil eliminado');
        this.loadPerfiles;
        alert('Perfil eliminado exitosamente');
      },
      error: error => {
        alert('Ocurrió un error al eliminar el perfil');
        console.error('Ocurrió un error al eliminar el perfil:', error);
      },
      complete: () => {
        console.info('Eliminación de Perfil completa');
      }
    });
  }}
