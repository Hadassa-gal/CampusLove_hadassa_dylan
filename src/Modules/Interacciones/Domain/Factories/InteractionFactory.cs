using CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Interacciones.Domain.Factories
{
    public static class InteractionFactory
    {
        public static Interaccion Create(int usuarioId, int targetId, TipoInteraccion tipo)
        {
            return new Interaccion { UsuarioId = usuarioId, TargetId = targetId, Tipo = tipo };
        }
    }
}
