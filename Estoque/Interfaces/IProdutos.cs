using Estoque.Models;
using Estoque.DTOs;

namespace Estoque.Interfaces
{
    public interface IProdutos
    {
        Task CriarProduto(Produtos Produto);
        Task<List<ProdutosDTO>> GetProduto();
        Task<ProdutosDTO> GetProdutoPorDescrição(string Descricao);
        Task<ProdutosDTO> GetProdutoPorId(int ProdutoId);
        Task AtualizarProduto(ProdutosDTO Produto);
        Task BaixarEstoque(int ProdutoId, int? QuantidadeParaBaixar);
        Task DeletarProduto(int ProdutoId);
    }
}
