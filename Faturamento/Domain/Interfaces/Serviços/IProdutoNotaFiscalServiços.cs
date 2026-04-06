using Faturamento.API.ModelViews;
using Faturamento.Domain.Models;

namespace Faturamento.Domain.Interfaces.Serviços
{
    public interface IProdutoNotaFiscalServiços
    {
        Task AddProdutoNaNotaFiscal(ProdutoNotaFiscalModelView ProdutoNotaFiscal);
        Task<List<ProdutosNotaFiscal>> GetProdutosNotaFiscalAsync(int NotaFiscalId);
        Task DeletarProdutoDaNotaFiscal(int NotaFiscalId, int ProdutoId);
    }
}
