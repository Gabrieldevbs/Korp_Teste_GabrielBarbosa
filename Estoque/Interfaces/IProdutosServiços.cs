using Estoque.DTOs;
using Estoque.Models;

namespace Estoque.Interfaces
{
    public interface IProdutosServiços
    {
        Task CriarProduto(ProdutoModelView Produto);
        Task<List<ProdutosDTO>> GetProduto();
        Task<ProdutosDTO> GetProdutoPorId(int ProdutoId);
        Task AtualizarProduto(ProdutosDTO Produto);
        Task BaixarEstoque(int ProdutoId, int QuantidadeParaBaixar);
        Task DeletarProduto(int ProdutoId);
    }
}
