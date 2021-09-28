using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogoJogos.DTO.InputModel;
using ApiCatalogoJogos.DTO.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    
    public class JogosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter() 
        {
            return Ok();
        }
        
        [HttpGet("{idjogo: guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter(Guid idjogo) 
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo(JogoInputModel jogo)
        {
            return Ok();
        }

        [HttpPut("{idjogo:guid}")]
        public async Task<ActionResult> AtualizarJogo(Guid idjogo, JogoInputModel jogo)
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