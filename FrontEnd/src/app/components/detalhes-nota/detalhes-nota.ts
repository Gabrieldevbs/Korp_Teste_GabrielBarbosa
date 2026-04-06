import { Component } from '@angular/core';
import { Faturamento } from '../../core/services/faturamento';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ListaProdutos } from '../lista-produtos/lista-produtos';
import { ChangeDetectorRef } from '@angular/core';
import { identifierName } from '@angular/compiler';

@Component({
  selector: 'app-detalhes-nota',
  imports: [CommonModule, ListaProdutos],
  templateUrl: './detalhes-nota.html',
  styleUrl: './detalhes-nota.css',
})
export class DetalhesNota {
  produtosvinculados: any = []
  id: number = 0

  constructor(
    private FaturamentoService: Faturamento,
    private route: ActivatedRoute,
    private cd: ChangeDetectorRef
  ){}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
        this.FaturamentoService.ListarProdutoEmNota(id).subscribe(data => {
          console.log('RETORNO API:', data);
          this.produtosvinculados = data
          this.id = id
          this.cd.detectChanges();
        })
      }

      DeletarProdutoEmNota(ProdutoId: number) {
        this.FaturamentoService.DeletarProdutoEmNota(this.id, ProdutoId)
        .subscribe({
          next: (data) => {
              window.location.reload();
              alert('Produto Excluído com sucesso!')
            },
            error: (err: HttpErrorResponse) => {
              console.error(err);
              alert(err.error.error); 
            }
      });
  }
}
