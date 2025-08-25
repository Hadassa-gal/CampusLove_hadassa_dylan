using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces.Services;

public interface IUserService
{
    Task<User?> ObtenerUsuarioPorIdAsync(int id);
    Task<IEnumerable<User>> ConsultarUsuariosAsync();
    Task RegistrarUsuarioAsync(string nombre, string email);
    Task ActualizarUsuarioAsync(int id, string nombre, string email);
    Task EliminarUsuarioAsync(int id);
}