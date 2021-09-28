using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.DTO.InputModel;
using ApiCatalogoJogos.DTO.ViewModel;
using ApiCatalogoJogos.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter(
                                                [FromQuery, Range(1, int.MaxValue)] int pagina = 1,
                                                [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);

            if (!jogos.Any())
                return NoContent();
            
            return Ok(jogos);
        }
        
        [HttpGet("{idjogo: guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idjogo) 
        {
            var jogo = await _jogoService.Obter(idjogo);

            if (jogo == null)
                return NoContent();
            
            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInput)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInput);
                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idjogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idjogo,
                                                [FromBody] JogoInputModel jogoInput)
        {
            try
            {
                await _jogoService.Atualizar(idjogo, jogoInput);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe esse jogo");
            }
        }
        
        [HttpPatch("{idjogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idjogo, 
                                                [FromRoute]double preco)
        {
            try
            {
                await _jogoService.Atualizar(idjogo, preco);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe esse jogo");
            }
        }

        [HttpDelete("{idjogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idjogo)
        {
            try
            {
                await _jogoService.Remover(idjogo);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound("Não existe esse jogo");
            }
        }
    }
}