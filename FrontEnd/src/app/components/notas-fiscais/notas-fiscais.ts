import { Component } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ChangeDetectorRef } from '@angular/core';
import { RouterLink } from "@angular/router";
import { Faturamento } from '../../core/services/faturamento';

@Component({
  selector: 'app-notas-fiscais',
  imports: [CommonModule, RouterLink],
  templateUrl: './notas-fiscais.html',
  styleUrl: './notas-fiscais.css',
})
export class NotasFiscais {

  notas: any[] = []
  
    constructor(
      private FaturamentoService: Faturamento,
      private cd: ChangeDetectorRef
    ){}
  
      ngOnInit(): void {
        this.FaturamentoService.ListarNotas().subscribe(data => {
          console.log('RETORNO API:', data);
          this.notas = data
          this.cd.detectChanges();
        })
      }

      CriarNota(){
        this.FaturamentoService.CriarNotas().subscribe(() => {
          window.location.reload();
          alert('Nota criada com sucesso!')
      })
      }

      FecharNota(id: number){
  this.FaturamentoService.FecharNota(id)
    .subscribe({
      next: (response: any) => {

        if (response instanceof Blob) {

          console.log("TYPE:", response.type);

          if (response.type === 'application/json') {
            const reader = new FileReader();

            reader.onload = () => {
              console.error("Erro backend:", reader.result);
              alert(reader.result);
            };

            reader.readAsText(response);
            return;
          }

          const url = URL.createObjectURL(response);

          const newTab = window.open();
          if (newTab) newTab.location.href = url;

          setTimeout(() => URL.revokeObjectURL(url), 1000);

          alert('Nota fechada com sucesso!');
          window.location.reload();
          return;
        }

        if (response.fileContents) {

          const base64 = response.fileContents;

          const byteCharacters = atob(base64);
          const byteNumbers = new Array(byteCharacters.length);

          for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
          }

          const byteArray = new Uint8Array(byteNumbers);
          const blob = new Blob([byteArray], { type: 'application/pdf' });

          const url = URL.createObjectURL(blob);
          window.open(url);

          return;
        }

        console.error("Resposta inesperada");
      },

      error: (err: HttpErrorResponse) => {
        console.error(err);

        if (err.error instanceof Blob) {
          const reader = new FileReader();

          reader.onload = () => {
            alert(reader.result);
          };

          reader.readAsText(err.error);
        } else {
          alert('Erro ao fechar nota');
        }
      }
    });
}

      DeletarNotas(id: number) {
        this.FaturamentoService.DeletarNotas(id)
        .subscribe({
          next: (data) => {
              window.location.reload();
              alert('Nota Excluída com sucesso!')
            },
            error: (err: HttpErrorResponse) => {
              console.error(err);
              alert(err.error.error); 
            }
      });
  }
}
