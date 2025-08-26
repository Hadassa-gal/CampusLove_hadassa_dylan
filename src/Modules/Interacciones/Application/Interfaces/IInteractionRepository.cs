using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Interacciones.Application.Interfaces
{
    public interface IInteractionRepository
    {
        Interaccion? GetById(int id);
        IEnumerable<Interaccion> GetAll();
        void Add(Interaccion interaccion);
        void Update(Interaccion interaccion);
        void Delete(int id);
    }
}
