using Faturamento.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faturamento.Domain.Models
{
    [Table("notasfiscais")]
    public class NotasFiscais
    {
        [Key]
        public int notafiscalid { get; private set; }
        public int numeracaosequencial { get; private set; }
        public EnumStatusNotaFiscal status { get; private set; }
        public DateOnly datacriacao { get; private set; }

        public NotasFiscais() { }

        public NotasFiscais(int NumeraçãoSequencial, EnumStatusNotaFiscal Status, DateOnly DataCriacao)
        {
            this.numeracaosequencial = NumeraçãoSequencial;
            this.status = Status;
            this.datacriacao = DataCriacao;
        }

        public void FecharNota(EnumStatusNotaFiscal NovoStatus)
        {
            this.status = NovoStatus;
        }
    }
}
