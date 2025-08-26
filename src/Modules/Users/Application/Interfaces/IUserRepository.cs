using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces
{
    public interface IUserRepository
    {
        Usuario? GetById(int id);
        IEnumerable<Usuario> GetAll();
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(int id);
    }
}
