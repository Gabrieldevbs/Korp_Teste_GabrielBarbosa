import { Component } from '@angular/core';
import { EstoqueService } from '../../core/services/estoque';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  produtos: any[] = []

  constructor(
    private EstoqueService: EstoqueService
  ){}

    ngOnInit(): void {
      this.EstoqueService.ListarProdutos().subscribe(data => {
        console.log('RETORNO API:', data);
        this.produtos = data
      })
    }
}
