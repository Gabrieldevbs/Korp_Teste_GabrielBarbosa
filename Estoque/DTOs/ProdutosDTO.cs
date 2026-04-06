namespace Estoque.DTOs
{
    public class ProdutosDTO
    {
        public int ProdutoId { get; set; }
        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
        public int? Saldo { get; set; }

        public ProdutosDTO() { }

        public ProdutosDTO(int ProdutoId, string Codigo, string Descricao, int? Saldo)
        {
            this.ProdutoId = ProdutoId;
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.Saldo = Saldo;
        }
    }
}
