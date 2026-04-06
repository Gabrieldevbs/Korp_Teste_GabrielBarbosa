using Faturamento.Application.DTOs;


namespace Faturamento.Domain.Interfaces.Serviços
{
    public interface INotaFiscalServiços
    {
        Task<string> CriarNotaFiscal();
        Task<List<NotasFiscaisDTO>> GetNotasFiscais();
        Task<NotasFiscaisDTO> GetNotasFiscaisPorId(int NotaFiscalId);
        Task<byte[]> FecharNotaFiscal(int NotaFiscalId);
        Task<string> DeletarNotaFiscal(int NotaFiscalId);
    }
}
