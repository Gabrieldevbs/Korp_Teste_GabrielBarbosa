import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Enviroment } from '../../../enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class EstoqueService {
    private Api = `${Enviroment.APIEstoque}/API/V1/Produtos`

    constructor(private http: HttpClient){}

    CriaProduto(
      Codigo: string,
      Descricao: string,
      Saldo: number
    ) {
      return this.http.post(this.Api, {
        Codigo,
        Descricao,
        Saldo
      });
    }

    ListarProdutos() {
      console.log(this.Api)
      return this.http.get<any[]>(this.Api);
    }

    BuscarProdutoPorId(ProdutoId: number){
      return this.http.get<any>(`${this.Api}/${ProdutoId}`)
    }

    AtualizarProduto(
      ProdutoId: number,
      Codigo?: string,
      Descricao?: string,
      Saldo?: number
    ) {
      return this.http.put(this.Api, {
        ProdutoId,
        Codigo,
        Descricao,
        Saldo
      });
    }

    DeletarProdutos(ProdutoId: number){
      return this.http.delete(`${this.Api}?ProdutoId=${ProdutoId}`)
    }
}
