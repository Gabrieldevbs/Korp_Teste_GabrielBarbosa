import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EstoqueService } from '../../core/services/estoque';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';

export interface Produto {
  produtoId: number;
  codigo: string;
  descricao: string;
  saldo: number;
}

@Component({
  selector: 'app-editar-produto',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './editar-produto.html',
  styleUrls: ['./editar-produto.css']
})

export class EditarProduto {

  produto: Produto = {
    produtoId: 0,
    codigo: '',
    descricao: '',
    saldo: 0
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private estoqueService: EstoqueService,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.estoqueService.BuscarProdutoPorId(id)
      .subscribe(data => {
        this.produto = ({...data});
        this.cd.detectChanges();
      });
  }

  salvar() {
    if(this.produto.saldo == null || this.produto.saldo < 1){
      return alert("O Saldo do produto deve ser um valor inteiro maior do que 0")
    }

    this.estoqueService.AtualizarProduto(this.produto.produtoId,
      this.produto.codigo, this.produto.descricao, this.produto.saldo
    )
      .subscribe({
        next: (data) => {
        alert('Produto atualizado com sucesso!');
        this.router.navigate(['/produtos']);
        },
        error: (err: HttpErrorResponse) => {
              console.error(err);
              alert(err.error.error); 
        }
      });
  }
}

