using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faturamento.Domain.Models
{
    [Table("produtosnotafiscal")]
    public class ProdutosNotaFiscal
    {
        [Key]
        public int itemnotafiscalid { get; private set; }
        public int notafiscalid { get; private set; }
        public int produtoid { get; private set; }
        public int quantidade { get; private set; }

        public ProdutosNotaFiscal() { }

        public ProdutosNotaFiscal(int NotaFiscalId, int ProdutoId, int Quantidade)
        {
            this.notafiscalid = NotaFiscalId;
            this.produtoid = ProdutoId;
            this.quantidade = Quantidade;
        }
    }
}
