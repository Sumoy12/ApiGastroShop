import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {MatSelectModule} from '@angular/material/select';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatGridListModule} from '@angular/material/grid-list';
import { RouterLink } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatSelectModule, MatIconModule, MatCardModule,
    MatGridListModule, RouterLink, ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  url7 = 'assets/img/1inicio.png';
  url8 = 'assets/img/2inicio.png';
  url6 = 'assets/img/3inicio.png';
  url9 = 'assets/img/car1.png';
  url10 = 'assets/img/car2.png';
  url11 = 'assets/img/car3.png';
}
