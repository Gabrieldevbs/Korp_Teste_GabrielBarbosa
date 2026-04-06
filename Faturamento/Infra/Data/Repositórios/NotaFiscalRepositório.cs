using Faturamento.Application.DTOs;
using Faturamento.Domain.Enum;
using Faturamento.Domain.Interfaces.Repositórios;
using Faturamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Faturamento.Infra.Data.Repositórios
{
    public class NotaFiscalRepositório : INotaFiscalRepositório
    {
        private readonly ConnectionContext _Contexto;

        public NotaFiscalRepositório(ConnectionContext Contexto)
        {
            this._Contexto = Contexto;
        }

        public async Task CriarNotaFiscal(NotasFiscais NotaFiscal)
        {
            _Contexto.NotasFiscais.Add(NotaFiscal);
            await _Contexto.SaveChangesAsync();
        }

        public async Task<List<NotasFiscaisDTO>> GetNotasFiscais()
        {
            var NotasFiscais = await _Contexto.NotasFiscais.Select(NF => new NotasFiscaisDTO
            {
                NotaFiscalId = NF.notafiscalid,
                NumeracaoSequencial = NF.notafiscalid,
                Status = NF.status,
                DataCriacao = NF.datacriacao
            }).ToListAsync();

            return NotasFiscais;
        }

        public async Task<NotasFiscaisDTO> GetNotasFiscaisPorId(int NotaFiscalId)
        {
            var NotaFiscal = await _Contexto.NotasFiscais.Select(NF => new NotasFiscaisDTO
            {
                NotaFiscalId = NF.notafiscalid,
                NumeracaoSequencial = NF.notafiscalid,
                Status = NF.status,
                DataCriacao = NF.datacriacao
            }).Where(NF => NF.NotaFiscalId == NotaFiscalId).FirstOrDefaultAsync();

            return NotaFiscal;
        }

        public async Task<NotasFiscaisDTO> GetUltimaNotaFiscal()
        {
            var UltimaNotaFiscal = await _Contexto.NotasFiscais.Select(NF => new NotasFiscaisDTO
            {
                NotaFiscalId = NF.notafiscalid,
                NumeracaoSequencial = NF.notafiscalid,
                Status = NF.status,
                DataCriacao = NF.datacriacao
            }).OrderByDescending(NF => NF.NotaFiscalId).FirstOrDefaultAsync();

            return UltimaNotaFiscal;
        }

        public async Task FecharNotaFiscal(int NotaFiscalId)
        {
            var NotaFiscal = await _Contexto.NotasFiscais.Where(NF => NF.notafiscalid == NotaFiscalId).FirstOrDefaultAsync();
            NotaFiscal.FecharNota(EnumStatusNotaFiscal.Fechada);
            await _Contexto.SaveChangesAsync();
        }

        public async Task DeletarNotaFiscal(int NotaFiscalId)
        {
            var NotaFiscal = await _Contexto.NotasFiscais.Where(NF => NF.notafiscalid == NotaFiscalId).FirstOrDefaultAsync();
            _Contexto.NotasFiscais.Remove(NotaFiscal);
            await _Contexto.SaveChangesAsync();
        }
    }
}
