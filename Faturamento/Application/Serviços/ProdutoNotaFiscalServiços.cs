using Faturamento.API.ModelViews;
using Faturamento.Domain.Enum;
using Faturamento.Domain.Interfaces.Repositórios;
using Faturamento.Domain.Interfaces.Serviços;
using Faturamento.Domain.Models;

namespace Faturamento.Application.Serviços
{
    public class ProdutoNotaFiscalServiços : IProdutoNotaFiscalServiços
    {
        private readonly IProdutosNotaFiscalRepositório _ProdutoNotaFiscalRepositório;
        private readonly INotaFiscalRepositório _NotaFiscalRepositório;
        private readonly EstoqueClient _EstoqueClient; 

        public ProdutoNotaFiscalServiços(IProdutosNotaFiscalRepositório ProdutoNotaFiscalRepositório, 
            INotaFiscalRepositório NotaFiscalRepositório, EstoqueClient EstoqueClient)
        {
            this._ProdutoNotaFiscalRepositório = ProdutoNotaFiscalRepositório;
            this._NotaFiscalRepositório = NotaFiscalRepositório;
            this._EstoqueClient = EstoqueClient;
        }

        public async Task<string> AddProdutoNaNotaFiscal(ProdutoNotaFiscalModelView ProdutoNotaFiscal)
        {
            var NotaFiscalExiste = await _NotaFiscalRepositório.GetNotasFiscaisPorId(ProdutoNotaFiscal.NotaFiscalId);
            
            if (NotaFiscalExiste == null)
            {
                throw new KeyNotFoundException("Essa nota fiscal não existe!");
            }

            if (NotaFiscalExiste.Status == EnumStatusNotaFiscal.Fechada)
            {
                throw new ArgumentException("A nota fiscal já está fechada, portanto não é possível fazer alterações nela!");
            }

            var ProdutoExiste = await _EstoqueClient.ProdutoExiste(ProdutoNotaFiscal.ProdutoId);

            if (!ProdutoExiste)
            {
                throw new KeyNotFoundException("O produto não existe!");
            }

            var ProdutoExisteNaNotaFiscal = await _ProdutoNotaFiscalRepositório.GetProdutoNotaFiscalAsync(
                ProdutoNotaFiscal.NotaFiscalId,
                ProdutoNotaFiscal.ProdutoId);

            if (ProdutoExisteNaNotaFiscal != null)
            {
                throw new ArgumentException("Este produto já existe nesta nota fiscal!");
            }

            var ProdutoNotaFiscalAdicionado = new ProdutosNotaFiscal(
                ProdutoNotaFiscal.NotaFiscalId,
                ProdutoNotaFiscal.ProdutoId,
                ProdutoNotaFiscal.Quantidade
            );

            await _ProdutoNotaFiscalRepositório.AddProdutoNaNotaFiscal(ProdutoNotaFiscalAdicionado);

            return "Produto adicionado com sucesso";
        }

        public async Task<List<ProdutosNotaFiscal>> GetProdutosNotaFiscalAsync(int NotaFiscalId)
        {
            var NotaFiscalExiste = await _NotaFiscalRepositório.GetNotasFiscaisPorId(NotaFiscalId);

            if (NotaFiscalExiste == null)
            {
                throw new KeyNotFoundException("Essa nota fiscal não existe!");
            }

            var ProdutosNotaFiscal = await _ProdutoNotaFiscalRepositório.GetProdutosNotaFiscalAsync(NotaFiscalId);

            if(ProdutosNotaFiscal.Count == 0)
            {
                throw new KeyNotFoundException("Não existem produtos cadastrados para essa nota fiscal!");
            }

            return ProdutosNotaFiscal;
        }


        public async Task<string> DeletarProdutoDaNotaFiscal(int NotaFiscalId, int ProdutoId)
        {
            var NotaFiscalExiste = await _NotaFiscalRepositório.GetNotasFiscaisPorId(NotaFiscalId);

            if (NotaFiscalExiste == null)
            {
                throw new KeyNotFoundException("Essa nota fiscal não existe!");
            }

            if (NotaFiscalExiste.Status == EnumStatusNotaFiscal.Fechada) 
            {
                throw new ArgumentException("A nota fiscal já está fechada, portanto não é possível fazer alterações nela!");
            }

            var ProdutoExiste = await _EstoqueClient.ProdutoExiste(ProdutoId);

            if (!ProdutoExiste)
            {
                throw new KeyNotFoundException("O produto não existe!");
            }

            await _ProdutoNotaFiscalRepositório.DeletarProdutoDaNotaFiscal(NotaFiscalId, ProdutoId);

            return "Produto adicionado com sucesso";
        }
    }
}
