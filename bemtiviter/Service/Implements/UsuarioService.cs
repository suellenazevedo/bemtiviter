using bemtiviter.Data;
using bemtiviter.Model;
using Microsoft.EntityFrameworkCore;

namespace bemtiviter.Service.Implements
{
    public class UsuarioService : IUsuarioService
    {
        public readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios
                .ToListAsync();
        }

        public async Task<Usuario?> GetById(long id)
        {
            try
            {
                var Usuario = await _context.Usuarios
                    .FirstAsync(u => u.Id == id);

                return Usuario;
            }
            catch
            {
                return null;
            }

        }

        public async Task<Usuario?> GetByEmail(string email)
        {
            try
            {
                var BuscaUsuario = await _context.Usuarios
                    .Where(u => u.Email == email)
                    .FirstOrDefaultAsync();

                return BuscaUsuario;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Usuario?> Create(Usuario usuario)
        {
            var BuscaUsuario = await GetByEmail(usuario.Email);

            if (BuscaUsuario is not null)
                return null;

            if (usuario.Foto is null || usuario.Foto == "")
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario?> Update(Usuario usuario)
        {

            var UsuarioUpdate = await _context.Usuarios.FindAsync(usuario.Id);

            if (UsuarioUpdate is null)
                return null;

            if (usuario.Foto is null || usuario.Foto == "")
                usuario.Foto = "https://i.imgur.com/I8MfmC8.png";

            _context.Entry(UsuarioUpdate).State = EntityState.Detached;
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
