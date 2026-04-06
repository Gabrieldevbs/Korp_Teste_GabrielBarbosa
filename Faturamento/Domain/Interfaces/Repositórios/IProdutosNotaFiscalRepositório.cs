using Faturamento.Domain.Models;

namespace Faturamento.Domain.Interfaces.Repositórios
{
    public interface IProdutosNotaFiscalRepositório
    {
        Task AddProdutoNaNotaFiscal(ProdutosNotaFiscal ProdutoNotaFiscal);
        Task<List<ProdutosNotaFiscal>> GetProdutosNotaFiscalAsync(int NotaFiscalId);
        Task<ProdutosNotaFiscal> GetProdutoNotaFiscalAsync(int NotaFiscalId, int ProdutoId);
        Task DeletarProdutoDaNotaFiscal(int NotaFiscalId, int ProdutoId);

    }
}
