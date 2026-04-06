using Estoque.DTOs;
using Estoque.Interfaces;
using Estoque.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Estoque.Serviços
{
    public class ProdutosServiços : IProdutosServiços
    {
        private readonly IProdutos _ProdutosRepositório;

        public ProdutosServiços(IProdutos ProdutosRepositório)
        {
            this._ProdutosRepositório = ProdutosRepositório;
        }

        public async Task CriarProduto(ProdutoModelView Produto)
        {
            var ProdutoExiste = await _ProdutosRepositório.GetProdutoPorDescrição(Produto.Descricao);
            if (ProdutoExiste != null)
            {
                throw new ArgumentException("Esse produto já existe.");
            }

            var NovoProduto = new Produtos(Produto.Codigo, Produto.Descricao, Produto.Saldo);

            await _ProdutosRepositório.CriarProduto(NovoProduto);

        }

        public async Task<List<ProdutosDTO>> GetProduto()
        {
            var Produtos = await _ProdutosRepositório.GetProduto();
            
            if (Produtos.Count == 0)
            {
                throw new KeyNotFoundException("Nenhum produto encontrado.");
            }

            return Produtos;
        }

        public async Task<ProdutosDTO> GetProdutoPorId(int ProdutoId)
        {
            var Produto = await _ProdutosRepositório.GetProdutoPorId(ProdutoId);

            if (Produto == null)
            {
                throw new KeyNotFoundException("Nenhum produto encontrado.");
            }

            return Produto;
        }

        public async Task AtualizarProduto(ProdutosDTO Produto)
        {
            var ProdutoExiste = await _ProdutosRepositório.GetProdutoPorId(Produto.ProdutoId);

            if (ProdutoExiste == null)
            {
                throw new ArgumentException("Esse produto Não existe!");
            }

            if (Produto.Codigo == ProdutoExiste.Codigo && Produto.Descricao == Produto.Descricao && Produto.Saldo == ProdutoExiste.Saldo)
            {
                throw new ArgumentException("Nenhuma alteração foi feita no produto.");
            }

            var ProdutoAtualizar = new ProdutosDTO
            (
                Produto.ProdutoId,
                Produto.Codigo ?? ProdutoExiste.Codigo,
                Produto.Descricao ?? ProdutoExiste.Codigo,
                Produto.Saldo ?? ProdutoExiste.Saldo
            );

            await _ProdutosRepositório.AtualizarProduto(ProdutoAtualizar);

        }

        public async Task BaixarEstoque(int ProdutoId, int QuantidadeParaBaixar)
        {
            var ProdutoExiste = await _ProdutosRepositório.GetProdutoPorId(ProdutoId);

            if (ProdutoExiste == null)
            {
                throw new KeyNotFoundException("Não existe produto com esse Id");
            }

            var NovoSaldo = ProdutoExiste.Saldo - QuantidadeParaBaixar;

            if (NovoSaldo < 0)
            {
                throw new ArgumentException("Não há saldo disponível para baixa. Saldo de estoque restante: " + ProdutoExiste.Saldo);
            }

            await _ProdutosRepositório.BaixarEstoque(ProdutoId, NovoSaldo);

        }

        public async Task DeletarProduto(int ProdutoId)
        {
            var ProdutoExiste = await _ProdutosRepositório.GetProdutoPorId(ProdutoId);

            if (ProdutoExiste == null)
            {
                throw new ArgumentException("Esse produto Não existe!");
            }

            await _ProdutosRepositório.DeletarProduto(ProdutoId);
        }
    }
}
