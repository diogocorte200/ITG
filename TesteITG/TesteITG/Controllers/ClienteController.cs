using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteITG.Domain.Domain;
using TesteITG.Domain.Service;
using TesteITG.Entity.Entity;

namespace TesteITG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService<ClienteModel, Cliente> _cliente;

        public ClienteController(ClienteService<ClienteModel, Cliente> pessoaService)
        {
            _cliente = pessoaService;
        }
        [HttpPost("Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] ClienteModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (cliente == null)
                return BadRequest();

            var clienteResposta = await _cliente.AdicionarCliente(cliente);

            if (clienteResposta == null)
            {
                return StatusCode(500, "Erro ao adicionar cliente!");
            }
            if (clienteResposta.ExibicaoMensagem != null)
            {
                return StatusCode(clienteResposta.ExibicaoMensagem.StatusCode, clienteResposta);
            }

            return Ok(clienteResposta);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarTodas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pessoas = await _cliente.ListarPessoas();

            return Ok(pessoas);
        }
        [HttpPut("Editar")]
        public async Task<IActionResult> Editar(Guid idCliente, [FromBody] ClienteModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var retorno = await _cliente.EditarCliente(cliente, idCliente);

            return Ok();
        }

        [HttpDelete("Deletar")]
        public async Task<IActionResult> DeletarCliente(Guid idcliente)
        {
            if (idcliente == null)
            {
                return BadRequest();
            }

            var resultado = await _cliente.DeletarCliente(idcliente);

            return Ok();
        }
    }
}
