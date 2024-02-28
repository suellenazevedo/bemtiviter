using bemtiviter.Model;

namespace bemtiviter.Service
{
    public interface IPostagemService
    {
        Task<IEnumerable<Postagem>> GetAll();

        Task<Postagem?> GetById(long id);

        Task<Postagem?> Create(Postagem postagem);

        Task<Postagem?> Update(Postagem postagem);

        Task Delete(Postagem postagem);
    }
}
