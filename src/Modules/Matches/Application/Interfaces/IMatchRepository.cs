using CampusLove_hadassa_dylan.src.Modules.Matches.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Matches.Application.Interfaces
{
    public interface IMatchRepository
    {
        Match? GetById(int id);
        IEnumerable<Match> GetAll();
        void Add(Match match);
        void Update(Match match);
        void Delete(int id);
    }
}
