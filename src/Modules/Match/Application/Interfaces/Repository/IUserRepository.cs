using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces.Repository;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}