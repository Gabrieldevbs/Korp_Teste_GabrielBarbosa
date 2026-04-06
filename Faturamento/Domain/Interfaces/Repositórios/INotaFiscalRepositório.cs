using Faturamento.Application.DTOs;
using Faturamento.Domain.Models;

namespace Faturamento.Domain.Interfaces.Repositórios
{
    public interface INotaFiscalRepositório
    {
        Task CriarNotaFiscal(NotasFiscais NotaFiscal);
        Task<List<NotasFiscaisDTO>> GetNotasFiscais();
        Task<NotasFiscaisDTO> GetNotasFiscaisPorId(int NotaFiscalId);
        Task<NotasFiscaisDTO> GetUltimaNotaFiscal();
        Task FecharNotaFiscal(int NotaFiscalId);
        Task DeletarNotaFiscal(int NotaFiscalId);
    }
}
