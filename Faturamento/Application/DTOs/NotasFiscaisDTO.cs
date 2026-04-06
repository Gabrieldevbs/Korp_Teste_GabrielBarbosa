using Faturamento.Domain.Enum;

namespace Faturamento.Application.DTOs
{
    public class NotasFiscaisDTO
    {
        public int NotaFiscalId { get; set; }
        public int NumeracaoSequencial { get; set; }
        public EnumStatusNotaFiscal Status { get; set; }
        public DateOnly DataCriacao { get; set; }

        public NotasFiscaisDTO() { }
    }
}
