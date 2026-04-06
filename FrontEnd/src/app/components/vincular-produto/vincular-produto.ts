import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Faturamento } from '../../core/services/faturamento';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { ListaProdutos } from '../lista-produtos/lista-produtos';

export interface VinculaProduto {
  notafiscalid: number;
  produtoid: number;
  quantidade: number;
}

@Component({
  selector: 'app-vincular-produto',
  imports: [CommonModule, FormsModule, ListaProdutos],
  templateUrl: './vincular-produto.html',
  styleUrl: './vincular-produto.css',
})
export class VincularProduto {
  Produto: VinculaProduto = {
      notafiscalid: 0,
      produtoid: 0,
      quantidade: 0
  };

  constructor(
    private route: ActivatedRoute,
    private FaturamentoService: Faturamento,
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.Produto.notafiscalid = id
  }

  criar() {

    if(this.Produto.quantidade == null || this.Produto.quantidade < 1){
      return alert("A quantidade do produto deve ser um valor inteiro maior do que 0")
    }

    this.FaturamentoService.VincularProdutoEmNota(
      this.Produto.notafiscalid, this.Produto.produtoid, this.Produto.quantidade
    )
      .subscribe({
        next: (data) => {
        alert('Produto vinculado com sucesso!');
        },
        error: (err: HttpErrorResponse) => {
              console.error(err);
              alert(err.error.error); 
        }
      });

  }
}
