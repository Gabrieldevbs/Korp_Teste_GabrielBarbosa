using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Estoque.Models;

namespace Estoque
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> Options) : base(Options)
        {
        }

        public DbSet<Produtos> Produtos { get; set; }
    }
}

