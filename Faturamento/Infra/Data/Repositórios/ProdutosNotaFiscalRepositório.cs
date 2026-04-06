using Faturamento.Domain.Interfaces.Repositórios;
using Faturamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Faturamento.Infra.Data.Repositórios
{
    public class ProdutosNotaFiscalRepositório : IProdutosNotaFiscalRepositório
    {
        private readonly ConnectionContext _Contexto;

        public ProdutosNotaFiscalRepositório (ConnectionContext Contexto)
        {
            this._Contexto = Contexto;
        }

        public async Task AddProdutoNaNotaFiscal(ProdutosNotaFiscal ProdutoNotaFiscal)
        {
            _Contexto.ProdutosNotaFiscal.Add(ProdutoNotaFiscal);
            await _Contexto.SaveChangesAsync();
        }

        public async Task<List<ProdutosNotaFiscal>> GetProdutosNotaFiscalAsync(int NotaFiscalId) 
        {
            var ProdutosNotaFiscal = await _Contexto.ProdutosNotaFiscal.Where(PNF => PNF.notafiscalid == NotaFiscalId).ToListAsync();
            return ProdutosNotaFiscal;
        }

        public async Task<ProdutosNotaFiscal> GetProdutoNotaFiscalAsync(int NotaFiscalId, int ProdutoId)
        {
            var ProdutosNotaFiscal = await _Contexto.ProdutosNotaFiscal
                .Where(PNF => PNF.notafiscalid == NotaFiscalId && PNF.produtoid == ProdutoId)
                .FirstOrDefaultAsync();

            return ProdutosNotaFiscal;
        }

        public async Task DeletarProdutoDaNotaFiscal(int NotaFiscalId, int ProdutoId)
        {
            var ProdutoNotaFiscalParaDeletar = await _Contexto.ProdutosNotaFiscal.Where(PNF =>
            PNF.notafiscalid == NotaFiscalId && PNF.produtoid == ProdutoId).FirstOrDefaultAsync();
            _Contexto.ProdutosNotaFiscal.Remove(ProdutoNotaFiscalParaDeletar);
            await _Contexto.SaveChangesAsync();
        }

    }
}
