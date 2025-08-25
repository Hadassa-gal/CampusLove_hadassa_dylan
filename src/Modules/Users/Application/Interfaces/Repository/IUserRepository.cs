using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Application.Interfaces.Repository;

public interface IUsuarioRepository
{
    int CrearUsuario(Userss usuario);
    Userss ObtenerUsuario(int id);
    List<Userss> ObtenerUsuariosDisponibles(int usuarioId, int limite = 10);
    bool ActualizarCreditos(int usuarioId);
    bool TieneCreditos(int usuarioId);
    void AgregarIntereses(int usuarioId, List<string> intereses);
    List<string> ObtenerIntereses(int usuarioId);
    List<Userss> ObtenerTodosLosUsuarios();
}