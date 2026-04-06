import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesNota } from './detalhes-nota';

describe('DetalhesNota', () => {
  let component: DetalhesNota;
  let fixture: ComponentFixture<DetalhesNota>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalhesNota],
    }).compileComponents();

    fixture = TestBed.createComponent(DetalhesNota);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
