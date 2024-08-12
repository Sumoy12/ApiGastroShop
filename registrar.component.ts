import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import {MatDialog, MatDialogModule}from '@angular/material/dialog';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatSelectModule} from '@angular/material/select';
import {ChangeDetectionStrategy, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { Usuario } from '../../interface/usuario';
import { UsuarioService } from '../../service/usuario.service';


@Component({
  selector: 'app-registrar',
  standalone: true,
  imports: [   FormsModule,
    MatDialogModule,
    RouterLink,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatCardModule,
    MatInputModule,
    MatGridListModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule],

  templateUrl: './registrar.component.html',
  styleUrl: './registrar.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,

})
export class RegistrarComponent {

  listadoUsuario: Usuario[] = [];
  ngOnInit(): void {
    this.obtenerUsuarios();


  }
  page: number = 1;
  pageSize: number = 5;
  totalItems: number = 0;




  constructor(private _UsuarioService: UsuarioService) {}


  regis = 'assets/img/LOGO-ROJO.png';

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
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


addUsuario(): void {

  };
}






