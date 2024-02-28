using bemtiviter.DTOs;
using bemtiviter.Model;
using bemtiviter.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace bemtiviter.Controller
{
    [Route("~/postagens")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly IValidator<PostagemDTO> _postagemValidator;

        public PostagemController(
            IPostagemService postagemService,
            IValidator<PostagemDTO> postagemValidator
            )
        {
            _postagemService = postagemService;
            _postagemValidator = postagemValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _postagemService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _postagemService.GetById(id);

            if (Resposta is null)
            {
                return NotFound();
            }
            return Ok(Resposta);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PostagemDTO postagemDTO)
        {
            var validatePost = await _postagemValidator.ValidateAsync(postagemDTO);

            if (!validatePost.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validatePost);

            var postagem = new Postagem(postagemDTO.Titulo, postagemDTO.Texto, postagemDTO.TemaId, postagemDTO.UsuarioId);
            var response =  await _postagemService.Create(postagem);

            if (Response is null)
                return BadRequest("Tema não encontrado.");


            return CreatedAtAction(nameof(GetById), new {id = postagem.Id}, postagem);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PostagemDTO postagemDTO, long id)
        {
            if (id == 0 || !PostagemExists(id))
                return BadRequest("ID da postagem é invalido ou não existe.");

            var validatePost = await _postagemValidator.ValidateAsync(postagemDTO);

            if (!validatePost.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validatePost);

            var postagem = await _postagemService.GetById(id);
            postagem.Titulo = postagemDTO.Titulo;
            postagem.Texto = postagemDTO.Texto;
            postagem.TemaId = postagemDTO.TemaId;

            var Response = await _postagemService.Update(postagem);

            if (Response is null)
                return NotFound("Post ou tema não encontrado");

            return Ok(Response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var FindPost = await _postagemService.GetById(id);

            if (FindPost is null)
                return NotFound("Post não encontrado.");

            await _postagemService.Delete(FindPost);

            return NoContent();
        }

        private bool PostagemExists(long id)
        {
            return (_postagemService.GetAll().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
