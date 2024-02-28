using bemtiviter.DTOs;
using bemtiviter.Model;
using bemtiviter.Service;
using bemtiviter.Service.Implements;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace bemtiviter.Controller
{
    [Route("~/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<UsuarioDTO> _usuarioValidator;

        public UsuarioController(
            IUsuarioService usuarioService,
            IValidator<UsuarioDTO> usuarioValidator
            )
        {
            _usuarioService = usuarioService;
            _usuarioValidator = usuarioValidator;

        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _usuarioService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _usuarioService.GetById(id);

            if (Resposta is null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(Resposta);
        }

        [HttpPost("cadastrar")]
        public async Task<ActionResult> Create([FromBody] UsuarioDTO usuarioDTO)
        {
            var validarUsuario = await _usuarioValidator.ValidateAsync(usuarioDTO);

            if (!validarUsuario.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarUsuario);

            if (EmailExists(usuarioDTO.Email) && usuarioDTO is not null)
                return BadRequest("O e-mail já está em uso por outro usuário.");

            var usuario = new Usuario(usuarioDTO.Nome, usuarioDTO.Email, usuarioDTO.Foto);
            var Resposta = await _usuarioService.Create(usuario);

            if (Resposta is null)
                return BadRequest("Usuário já cadastrado!");

            return CreatedAtAction(nameof(GetById), new { id = Resposta.Id }, Resposta);
        }

        [HttpPut("atualizar")]
        public async Task<ActionResult> Update([FromBody] UsuarioDTO usuarioDTO, long id)
        {
            if (id == 0 || !UsuarioExists(id))
                return BadRequest("O Id do Usuário é inválido!");

            var validarUsuario = await _usuarioValidator.ValidateAsync(usuarioDTO);

            if (!validarUsuario.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarUsuario);

            var BuscaUsuario = await _usuarioService.GetById(id);

            if (EmailExists(usuarioDTO.Email) && usuarioDTO is not null && id != BuscaUsuario.Id)
                return BadRequest("O e-mail já está em uso por outro usuário.");

            BuscaUsuario.Nome = usuarioDTO.Nome;
            BuscaUsuario.Email = usuarioDTO.Email;
            BuscaUsuario.Foto = usuarioDTO.Foto;

            var Resposta = await _usuarioService.Update(BuscaUsuario);

            if (Resposta is null)
                return NotFound("Usuário não encontrado!");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaUsuario = await _usuarioService.GetById(id);

            if (BuscaUsuario is null)
                return NotFound("Usuario não encontrado!");

            await _usuarioService.Delete(BuscaUsuario);

            return NoContent();

        }

        private bool EmailExists(string email)
        {
            return (_usuarioService.GetAll().Result?.Any(e => e.Email == email)).GetValueOrDefault();
        }

        private bool UsuarioExists(long id)
        {
            return (_usuarioService.GetAll().Result?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
