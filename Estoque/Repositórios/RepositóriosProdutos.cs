using Estoque.DTOs;
using Estoque.Interfaces;
using Estoque.Models;
using Microsoft.EntityFrameworkCore;


namespace Estoque.Repositórios
{
    public class RepositóriosProdutos : IProdutos
    {
        private readonly ConnectionContext _Contexto;

        public RepositóriosProdutos(ConnectionContext Contexto)
        {
            this._Contexto = Contexto;
        }

        public async Task CriarProduto(Produtos Produto)
        {
            _Contexto.Produtos.Add(Produto);
            await _Contexto.SaveChangesAsync();
        }

        public async Task<List<ProdutosDTO>> GetProduto()
        {
            var Produtos = await _Contexto.Produtos.Select(p => new ProdutosDTO
            {
                ProdutoId = p.produtoid,
                Descricao = p.descricao,
                Codigo = p.codigo,
                Saldo = p.saldo
            }).ToListAsync();

            return Produtos;
        }

        public async Task<ProdutosDTO> GetProdutoPorDescrição(string Descricao)
        {
            var Produtos = await _Contexto.Produtos.Select(p => new ProdutosDTO
            {
                ProdutoId = p.produtoid,
                Descricao = p.descricao,
                Codigo = p.codigo,
                Saldo = p.saldo
            }).Where(p => p.Descricao == Descricao).FirstOrDefaultAsync();

            return Produtos;
        }

        public async Task<ProdutosDTO> GetProdutoPorId(int ProdutoId)
        {
            var Produtos = await _Contexto.Produtos.Select(p => new ProdutosDTO
            {
                ProdutoId = p.produtoid,
                Descricao = p.descricao,
                Codigo = p.codigo,
                Saldo = p.saldo
            }).Where(p => p.ProdutoId == ProdutoId).FirstOrDefaultAsync();

            return Produtos;
        }

        public async Task<ProdutosDTO> GetProdutoPorCodigo(string Codigo)
        {
            var Produtos = await _Contexto.Produtos.Select(p => new ProdutosDTO
            {
                ProdutoId = p.produtoid,
                Descricao = p.descricao,
                Codigo = p.codigo,
                Saldo = p.saldo
            }).Where(p => p.Codigo == Codigo).FirstOrDefaultAsync();

            return Produtos;
        }

        public async Task AtualizarProduto(ProdutosDTO Produto)
        {
            var ProdutoParaAtualizar = await _Contexto.Produtos.Where(p => p.produtoid == Produto.ProdutoId).FirstOrDefaultAsync();
            ProdutoParaAtualizar.AtualizaProdutos(Produto.Codigo, Produto.Descricao, Produto.Saldo);
            await _Contexto.SaveChangesAsync();
        }

        public async Task BaixarEstoque(int ProdutoId, int? QuantidadeParaBaixar)
        {
            var ProdutoParaBaixarEstoque = await _Contexto.Produtos.Where(p => p.produtoid == ProdutoId).FirstOrDefaultAsync();
            ProdutoParaBaixarEstoque.BaixarEstoque(QuantidadeParaBaixar);
            await _Contexto.SaveChangesAsync();
        }

        public async Task DeletarProduto(int ProdutoId) 
        {
            var ProdutoParaDeletar = await _Contexto.Produtos.Where(p => p.produtoid == ProdutoId).FirstOrDefaultAsync();
            _Contexto.Produtos.Remove(ProdutoParaDeletar);
            await _Contexto.SaveChangesAsync();
        }
    }
}
