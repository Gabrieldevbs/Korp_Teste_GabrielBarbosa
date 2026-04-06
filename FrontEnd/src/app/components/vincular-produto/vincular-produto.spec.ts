import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VincularProduto } from './vincular-produto';

describe('VincularProduto', () => {
  let component: VincularProduto;
  let fixture: ComponentFixture<VincularProduto>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VincularProduto],
    }).compileComponents();

    fixture = TestBed.createComponent(VincularProduto);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
