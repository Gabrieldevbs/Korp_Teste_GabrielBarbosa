namespace Faturamento.Application.Serviços
{
    public class EstoqueClient
    {
        private readonly HttpClient _http;

        public EstoqueClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> ProdutoExiste(int produtoId)
        {
            var response = await _http.GetAsync($"API/V1/Produtos/{produtoId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<string> BaixarEstoque(int ProdutoId, int QuantidadeParabaixar)
        {
            var response = await _http.PutAsync($"API/V1/Produtos/BaixarEstoque/{ProdutoId}?QuantidadeParaBaixar={QuantidadeParabaixar}", null);

            if (response.IsSuccessStatusCode)
            {
                return "true";
            }

            return response.Content.ToString();
        }

    }
}
