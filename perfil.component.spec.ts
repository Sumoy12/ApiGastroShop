import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPerfilComponent } from './perfil.component';

describe('GPerfilComponent', () => {
  let component: ListPerfilComponent;
  let fixture: ComponentFixture<ListPerfilComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListPerfilComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListPerfilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
