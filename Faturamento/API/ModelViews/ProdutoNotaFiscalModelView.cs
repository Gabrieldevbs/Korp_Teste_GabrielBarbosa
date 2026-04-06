using System.ComponentModel.DataAnnotations;

namespace Faturamento.API.ModelViews
{
    public class ProdutoNotaFiscalModelView
    {
        [Required]
        public int NotaFiscalId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
