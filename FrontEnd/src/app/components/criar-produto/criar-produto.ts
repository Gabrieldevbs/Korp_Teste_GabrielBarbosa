import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EstoqueService } from '../../core/services/estoque';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';

export interface CriaProduto {
  codigo: string;
  descricao: string;
  saldo: number;
}

@Component({
  selector: 'app-criar-produto',
  imports: [FormsModule, CommonModule],
  templateUrl: './criar-produto.html',
  styleUrl: './criar-produto.css',
})
export class CriarProduto {

  Produto: CriaProduto = {
      codigo: '',
      descricao: '',
      saldo: 0
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private estoqueService: EstoqueService,
  ) {}

  
  criar() {

    if(this.Produto.saldo == null || this.Produto.saldo < 1){
      return alert("O Saldo do produto deve ser um valor inteiro maior do que 0")
    }

    this.estoqueService.CriaProduto(
      this.Produto.codigo, this.Produto.descricao, this.Produto.saldo
    )
      .subscribe({
        next: (data) => {
        alert('Produto criado com sucesso!');
        this.router.navigate(['/produtos']);
        },
        error: (err: HttpErrorResponse) => {
              console.error(err);
              alert(err.error.error); 
        }
      });

  }
}
