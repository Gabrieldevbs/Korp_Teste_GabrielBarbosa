using Faturamento.API.ModelViews;
using Faturamento.Domain.Interfaces.Serviços;
using Faturamento.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Faturamento.API.Controllers
{
    [ApiController]
    [Route("API/V1/ProdutoNotaFiscal")]
    public class ProdutoNotaFiscalController : ControllerBase
    {
        private readonly IProdutoNotaFiscalServiços _ProdutoNotaFiscalServiços;

        public ProdutoNotaFiscalController(IProdutoNotaFiscalServiços ProdutoNotaFiscalServiços)
        {
            this._ProdutoNotaFiscalServiços = ProdutoNotaFiscalServiços;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarProduto([FromBody]ProdutoNotaFiscalModelView ProdutoNotaFiscal)
        {
            await _ProdutoNotaFiscalServiços.AddProdutoNaNotaFiscal(ProdutoNotaFiscal);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutosNotaFiscalAsync(int NotaFiscalId)
        {
            var ProdutosNotaFiscal = await _ProdutoNotaFiscalServiços.GetProdutosNotaFiscalAsync(NotaFiscalId);
            return Ok(ProdutosNotaFiscal);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarProduto(int NotaFiscalId, int ProdutoId)
        {
            await _ProdutoNotaFiscalServiços.DeletarProdutoDaNotaFiscal(NotaFiscalId, ProdutoId);
            return Ok();
        }
    }
}
