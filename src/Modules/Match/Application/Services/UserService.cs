using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;
using CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces.Repository;
using CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces.Services;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<User?> ObtenerUsuarioPorIdAsync(int id) => await _repo.GetByIdAsync(id);

    public async Task<IEnumerable<User>> ConsultarUsuariosAsync() => await _repo.GetAllAsync();

    public async Task RegistrarUsuarioAsync(string nombre, string email)
    {
        var user = new User { Nombre = nombre, Email = email };
        await _repo.AddAsync(user);
    }

    public async Task ActualizarUsuarioAsync(int id, string nombre, string email)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user is null) return;
        user.Nombre = nombre;
        user.Email = email;
        await _repo.UpdateAsync(user);
    }

    public async Task EliminarUsuarioAsync(int id) => await _repo.DeleteAsync(id);
}