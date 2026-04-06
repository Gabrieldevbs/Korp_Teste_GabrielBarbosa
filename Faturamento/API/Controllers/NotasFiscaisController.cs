using Faturamento.Domain.Interfaces.Serviços;
using Microsoft.AspNetCore.Mvc;

namespace Faturamento.API.Controllers
{
    [ApiController]
    [Route("API/V1/NotasFiscais")]
    public class NotasFiscaisController : ControllerBase
    {
        private readonly INotaFiscalServiços _NotaFiscalServiços;

        public NotasFiscaisController(INotaFiscalServiços NotaFiscalServiços)
        {
            this._NotaFiscalServiços = NotaFiscalServiços;
        }

        [HttpPost]
        public async Task<IActionResult> CriarNotaFiscal()
        {
            await _NotaFiscalServiços.CriarNotaFiscal();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetNotasFiscais()
        {
            var NotasFiscais = await _NotaFiscalServiços.GetNotasFiscais();
            return Ok(NotasFiscais);
        }

        [HttpPut]
        public async Task<IActionResult> FecharNotaFiscal(int NotaFiscalId)
        {
            var NotaFechada = await _NotaFiscalServiços.FecharNotaFiscal(NotaFiscalId);
            return File(NotaFechada, "application/pdf", $"nota_fiscal_{NotaFiscalId}.pdf");
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarNotaFiscal(int NotaFiscalId)
        {
            await _NotaFiscalServiços.DeletarNotaFiscal(NotaFiscalId);
            return Ok();
        }
    }
}
