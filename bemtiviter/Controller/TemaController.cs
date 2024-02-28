using bemtiviter.DTOs;
using bemtiviter.Model;
using bemtiviter.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace bemtiviter.Controller
{
    [Route("~/temas")]
    [ApiController]
    public class TemaController : ControllerBase
    {

        private readonly ITemaService _temaService;
        private readonly IValidator<TemaDTO> _temaValidator;

        public TemaController(
            ITemaService temaService,
            IValidator<TemaDTO> temaValidator
            )
        {
            _temaService = temaService;
            _temaValidator = temaValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _temaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _temaService.GetById(id);

            if (Resposta is null)
            {
                return NotFound("Tema não encontrado.");
            }

            return Ok(Resposta);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TemaDTO temaDTO)
        {
            var validarTema = await _temaValidator.ValidateAsync(temaDTO);

            if (!validarTema.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);

            var tema = new Tema(temaDTO.Descricao);
            var Resposta = await _temaService.Create(tema);
            return CreatedAtAction(nameof(GetById), new { id = Resposta.Id }, Resposta);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] TemaDTO temaDTO, long id)
        {
            if (id == 0 || !ThemeExists(id))
                return BadRequest("ID do tema é invalido ou não existe.");

            var validarTema = await _temaValidator.ValidateAsync(temaDTO);

            if (!validarTema.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarTema);

            var tema = await _temaService.GetById(id);
            tema.Descricao = temaDTO.Descricao;

            var Resposta = await _temaService.Update(tema);

            if (Resposta is null)
                return NotFound("Tema não encontrado.");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaTema = await _temaService.GetById(id);

            if (BuscaTema is null)
                return NotFound("Tema não encontrado.");

            await _temaService.Delete(BuscaTema);

            return NoContent();

        }

        private bool ThemeExists(long id)
        {
            return (_temaService.GetAll().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
