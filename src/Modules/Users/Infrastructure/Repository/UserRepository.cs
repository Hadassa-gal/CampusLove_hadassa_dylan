using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using CampusLove_hadassa_dylan.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Infrastructure
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => u.Activo)
                .OrderBy(u => u.Nombre)
                .ToListAsync();
        }

        public async Task<List<User>> GetByCarreraAsync(string carrera)
        {
            return await _context.Users
                .Where(u => u.Activo && u.Carrera.Contains(carrera))
                .ToListAsync();
        }

        public async Task<User?> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                user.Activo = false; // Soft delete
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetUsersWithSimilarInterestsAsync(List<string> intereses)
        {
            var users = await _context.Users
                .Where(u => u.Activo)
                .ToListAsync();

            // Filtrar por intereses similares en memoria (debido a la conversión de List<string>)
            return users.Where(u => u.Intereses.Any(i => intereses.Contains(i))).ToList();
        }
    }
}