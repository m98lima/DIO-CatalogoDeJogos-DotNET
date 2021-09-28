using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    
    public class JogosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<object>>> Obter() 
        {
            return Ok();
        }
        
        [HttpGet("{idjogo: guid}")]
        public async Task<ActionResult<object>> Obter(Guid idjogo) 
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<object>> InserirJogo(object jogo)
        {
            return Ok();
        }

        [HttpPut("{idjogo:guid}")]
        public async Task<ActionResult> AtualizarJogo(Guid idjogo, object jogo)
        {
            return Ok();
        }
        
        [HttpPatch("{idjogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo(Guid idjogo, double preco)
        {
            return Ok();
        }

        [HttpDelete("{idjogo:guid}")]
        public async Task<ActionResult> ApagarJogo(Guid idjogo)
        {
            return Ok();
        }
    }
}