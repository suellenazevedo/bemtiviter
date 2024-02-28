using bemtiviter.Model;

namespace bemtiviter.Service
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAll();

        Task<Usuario?> GetById(long id);

        Task<Usuario?> GetByEmail(string usuario);

        Task<Usuario?> Create(Usuario usuario);

        Task<Usuario?> Update(Usuario usuario);

        Task Delete(Usuario usuario);
    }
}
