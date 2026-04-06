import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Enviroment } from '../../../enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class Faturamento {
  private ApiNotas = `${Enviroment.APIFaturamento}/API/V1/NotasFiscais`
  private ApiNotasProduto = `${Enviroment.APIFaturamento}/API/V1/ProdutoNotaFiscal`

  constructor(private http: HttpClient){}
  
  CriarNotas() {
      return this.http.post(this.ApiNotas, {});
    }

  VincularProdutoEmNota(
    NotaFiscalId: number, 
    ProdutoId: number, 
    Quantidade: number){
      return this.http.post(this.ApiNotasProduto, {
        NotaFiscalId,
        ProdutoId,
        Quantidade
      })
  }

  ListarNotas() {
      return this.http.get<any[]>(this.ApiNotas);
    }
  
  ListarProdutoEmNota(NotaId: number){
    return this.http.get<any[]>(`${this.ApiNotasProduto}?NotaFiscalId=${NotaId}`);
  }

  FecharNota(NotaFiscalId: number){
    return this.http.put(`${this.ApiNotas}?NotaFiscalId=${NotaFiscalId}`, {}, {
    responseType: 'blob'
  })
  }

  DeletarProdutoEmNota(NotaId: number, ProdutoId: number){
    return this.http.delete(`${this.ApiNotasProduto}?NotaFiscalId=${NotaId}&ProdutoId=${ProdutoId}`)
  }

  DeletarNotas(NotaId: number){
      return this.http.delete(`${this.ApiNotas}?NotaFiscalId=${NotaId}`)
  }
}
