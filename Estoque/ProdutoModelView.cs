using System.ComponentModel.DataAnnotations;

namespace Estoque
{
    public class ProdutoModelView
    {
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int Saldo { get; set; }

        public ProdutoModelView() { }

        public ProdutoModelView(string Codigo, string Descricao, int Saldo)
        {
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.Saldo = Saldo;
        }
    }
}
