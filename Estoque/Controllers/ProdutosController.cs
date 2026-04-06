using Estoque.DTOs;
using Estoque.Interfaces;
using Estoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Controllers
{
    [ApiController]
    [Route("API/V1/Produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosServiços _ProdutosServiços;

        public ProdutosController(IProdutosServiços ProdutosServiços)
        {
            this._ProdutosServiços = ProdutosServiços;
        }

        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] ProdutoModelView Produto)
        {
            await _ProdutosServiços.CriarProduto(Produto);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            var Produtos = await _ProdutosServiços.GetProduto();
            return Ok(Produtos);
        }

        [HttpGet]
        [Route("{ProdutoId}")]
        public async Task<IActionResult> GetProdutoPorId(int ProdutoId)
        {
            var produto = await _ProdutosServiços.GetProdutoPorId(ProdutoId);
            return Ok(produto);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarProduto([FromBody]ProdutosDTO Produto)
        {
            await _ProdutosServiços.AtualizarProduto(Produto);
            return Ok();
        }

        [HttpPut]
        [Route("BaixarEstoque/{ProdutoId}")]
        public async Task<IActionResult> BaixarEstoque(int ProdutoId, int QuantidadeParaBaixar)
        {
            await _ProdutosServiços.BaixarEstoque(ProdutoId, QuantidadeParaBaixar);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletarProduto(int ProdutoId)
        {
            await _ProdutosServiços.DeletarProduto(ProdutoId);
            return Ok();
        }
    }
}
