using CampusLove_hadassa_dylan.src.Modules.Users.Domain.Entities;

namespace CampusLove_hadassa_dylan.src.Modules.Users.Domain.Factories
{
    public static class UserFactory
    {
        public static Usuario Create(string nombre, int edad, string genero)
        {
            return new Usuario
            {
                Nombre = nombre,
                Edad = edad,
                Genero = (Genero)Enum.Parse(typeof(Genero), genero, true)
            };
        }
    }
}
