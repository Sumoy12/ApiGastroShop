import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { FacturacionComponent } from './facturacion.component';

describe('FacturaComponent', () => {
  let component: FacturacionComponent;
  let fixture: ComponentFixture<FacturacionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FacturacionComponent],
      imports: [ FormsModule ]
    })
    .compileComponents();
  });
   
    beforeEach(() => {
      fixture = TestBed.createComponent(FacturacionComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
