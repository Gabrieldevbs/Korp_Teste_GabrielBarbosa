using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace Estoque.Models
{

    [Table("produtos")]
    public class Produtos
    {
        [Key]
        public int produtoid { get; private set; }
        public string codigo { get; private set; }
        public string descricao { get; private set; }
        public int? saldo { get; private set; }

        public Produtos() { }

        public Produtos(string Codigo, string Descricao, int Saldo) 
        {
            this.codigo = Codigo;
            this.descricao = Descricao;
            this.saldo = Saldo;
        }

        public void AtualizaProdutos(string Codigo, string Descricao, int? Saldo)
        {
            this.codigo = Codigo;
            this.descricao = Descricao;
            this.saldo = Saldo;
        }

        public void BaixarEstoque(int? Saldo)
        {
            this.saldo = Saldo;
        }
    }
}
