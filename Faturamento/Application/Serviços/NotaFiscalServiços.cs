using Faturamento.Application.DTOs;
using Faturamento.Domain.Enum;
using Faturamento.Domain.Interfaces.Repositórios;
using Faturamento.Domain.Interfaces.Serviços;
using Faturamento.Domain.Models;


namespace Faturamento.Application.Serviços
{
    public class NotaFiscalServiços : INotaFiscalServiços
    {

        private readonly INotaFiscalRepositório _NotaFiscalRepositório;
        private readonly IProdutosNotaFiscalRepositório _ProdutosNotaFiscalRepositório; 
        private readonly EstoqueClient _EstoqueClient;
        private readonly PdfService _PdfService;

        public NotaFiscalServiços(INotaFiscalRepositório NotaFiscalRepositório, EstoqueClient EstoqueClient,
            IProdutosNotaFiscalRepositório ProdutosNotaFiscalRepositório, PdfService PdfService)
        {
            this._NotaFiscalRepositório = NotaFiscalRepositório;
            this._ProdutosNotaFiscalRepositório = ProdutosNotaFiscalRepositório;
            this._EstoqueClient = EstoqueClient;
            this._PdfService = PdfService;
        }

        public async Task CriarNotaFiscal() 
        {
            var UltimaNotaFiscal = await _NotaFiscalRepositório.GetUltimaNotaFiscal();

            if (UltimaNotaFiscal == null)
            {
                var NotaFiscalParaCriarNovo = new NotasFiscais(
                1,
                EnumStatusNotaFiscal.Aberta,
                DateOnly.FromDateTime(DateTime.Today)
                );

                await _NotaFiscalRepositório.CriarNotaFiscal(NotaFiscalParaCriarNovo);
            }

            var NotaFiscalParaCriar = new NotasFiscais(
                UltimaNotaFiscal.NumeracaoSequencial + 1,
                EnumStatusNotaFiscal.Aberta,
                DateOnly.FromDateTime(DateTime.Today)
            );

            await _NotaFiscalRepositório.CriarNotaFiscal(NotaFiscalParaCriar);


        }

        public async Task<List<NotasFiscaisDTO>> GetNotasFiscais()
        {
            var NotasFiscais = await _NotaFiscalRepositório.GetNotasFiscais();

            if (NotasFiscais.Count == 0)
            {
                throw new KeyNotFoundException("Não existem notas cadastradas");
            }

            return NotasFiscais;
        }

        public async Task<NotasFiscaisDTO> GetNotasFiscaisPorId(int NotaFiscalId)
        {
            var NotaFiscal = await _NotaFiscalRepositório.GetNotasFiscaisPorId(NotaFiscalId);

            if (NotaFiscal == null)
            {
                throw new KeyNotFoundException("Não existem notas cadastradas com esse Id");
            }

            return NotaFiscal;
        }

        public async Task<byte[]> FecharNotaFiscal(int NotaFiscalId)
        {
            var NotaFiscalParaFechar = await _NotaFiscalRepositório.GetNotasFiscaisPorId(NotaFiscalId);

            if (NotaFiscalParaFechar == null)
            {
                throw new KeyNotFoundException("Não existem notas cadastradas com esse Id");
            }

            if (NotaFiscalParaFechar.Status == EnumStatusNotaFiscal.Fechada)
            {
                throw new ArgumentException("A nota fiscal já está fechada!");
            }

            var ProdutosNotaFiscal = await _ProdutosNotaFiscalRepositório.GetProdutosNotaFiscalAsync(NotaFiscalId);

            foreach(var produto in ProdutosNotaFiscal)
            {
                var Estoque = await _EstoqueClient.BaixarEstoque(produto.produtoid, produto.quantidade);
                if (Estoque != "true")
                {
                    throw new ArgumentException($"Não foi possível baixar o estoque do produto {produto.produtoid}, " +
                        $"pois o saldo de estoque é menor que a quantidade que consta na nota fiscal");
                }
            }

            var pdf = await _PdfService.GerarNotaFiscal(NotaFiscalId, ProdutosNotaFiscal);

            await _NotaFiscalRepositório.FecharNotaFiscal(NotaFiscalId);

            return pdf;
        }

        public async Task DeletarNotaFiscal(int NotaFiscalId)
        {
            var NotaFiscalParaDeletar = await _NotaFiscalRepositório.GetNotasFiscaisPorId(NotaFiscalId);

            if (NotaFiscalParaDeletar == null)
            {
                throw new KeyNotFoundException("Não existem notas cadastradas com esse Id");
            }

            if (NotaFiscalParaDeletar.Status == EnumStatusNotaFiscal.Fechada)
            {
                throw new ArgumentException("A nota fiscal já está fechada, portanto não é possível excluir!");
            }

            await _NotaFiscalRepositório.DeletarNotaFiscal(NotaFiscalId);

        }
    }
}
