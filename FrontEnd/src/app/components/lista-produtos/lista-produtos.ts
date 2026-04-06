import { Component } from '@angular/core';
import { EstoqueService } from '../../core/services/estoque';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-lista-produtos',
  imports: [CommonModule],
  templateUrl: './lista-produtos.html',
  styleUrl: './lista-produtos.css',
})
export class ListaProdutos {
  produtos: any[] = []
  
    constructor(
      private EstoqueService: EstoqueService,
      private cd: ChangeDetectorRef
    ){}
  
      ngOnInit(): void {
        this.EstoqueService.ListarProdutos().subscribe(data => {
          console.log('RETORNO API:', data);
          this.produtos = data
          this.cd.detectChanges();
        })
      }

      DeletarProdutos(id: number) {
        this.EstoqueService.DeletarProdutos(id)
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
