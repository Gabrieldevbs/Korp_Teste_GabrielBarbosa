namespace Faturamento.Application.DTOs
{
    public class ProdutosNotaFiscalDTO
    {
        public int ItemNotaFiscalId { get; set; }
        public int NotaFiscalId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
