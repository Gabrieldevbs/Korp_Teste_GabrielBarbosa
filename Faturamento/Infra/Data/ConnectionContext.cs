using Faturamento.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Faturamento.Infra.Data
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> Options) : base(Options)
        {
        }

        public DbSet<NotasFiscais> NotasFiscais { get; set; }
        public DbSet<ProdutosNotaFiscal> ProdutosNotaFiscal { get; set; }

    }
}
