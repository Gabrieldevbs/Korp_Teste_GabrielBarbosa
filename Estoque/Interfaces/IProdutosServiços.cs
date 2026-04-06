using Estoque.DTOs;
using Estoque.Models;

namespace Estoque.Interfaces
{
    public interface IProdutosServiços
    {
        Task<string> CriarProduto(ProdutoModelView Produto);
        Task<List<ProdutosDTO>> GetProduto();
        Task<ProdutosDTO> GetProdutoPorId(int ProdutoId);
        Task<string> AtualizarProduto(ProdutosDTO Produto);
        Task<string> BaixarEstoque(int ProdutoId, int QuantidadeParaBaixar);
        Task<string> DeletarProduto(int ProdutoId);
    }
}
