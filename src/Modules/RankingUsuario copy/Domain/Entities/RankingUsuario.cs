using System.Data;

namespace CampusLove_hadassa_dylan.src.Modules.RankingUsuario.Domain.Entities
{
    

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public int CrearUsuario(Usuario usuario)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = @"INSERT INTO usuarios (nombre, edad, genero, carrera, frase_perfil) 
                         VALUES (@nombre, @edad, @genero, @carrera, @frasePerfil);
                         SELECT LAST_INSERT_ID();";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@edad", usuario.Edad);
            command.Parameters.AddWithValue("@genero", usuario.Genero);
            command.Parameters.AddWithValue("@carrera", usuario.Carrera);
            command.Parameters.AddWithValue("@frasePerfil", usuario.FrasePerfil ?? "");

            var userId = Convert.ToInt32(command.ExecuteScalar());
            
            // Agregar intereses si existen
            if (usuario.Intereses.Any())
            {
                AgregarIntereses(userId, usuario.Intereses);
            }

            return userId;
        }

        public Usuario ObtenerUsuario(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = @"SELECT id, nombre, edad, genero, carrera, frase_perfil, 
                                creditos_interaccion, ultima_recarga_creditos, 
                                likes_recibidos, fecha_registro, activo 
                         FROM usuarios WHERE id = @id AND activo = TRUE";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var usuario = new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Edad = reader.GetInt32("edad"),
                    Genero = reader.GetString("genero"),
                    Carrera = reader.GetString("carrera"),
                    FrasePerfil = reader.GetString("frase_perfil"),
                    CreditosInteraccion = reader.GetInt32("creditos_interaccion"),
                    UltimaRecargaCreditos = reader.GetDateTime("ultima_recarga_creditos"),
                    LikesRecibidos = reader.GetInt32("likes_recibidos"),
                    FechaRegistro = reader.GetDateTime("fecha_registro"),
                    Activo = reader.GetBoolean("activo")
                };
                
                reader.Close();
                usuario.Intereses = ObtenerIntereses(id);
                return usuario;
            }

            return null;
        }

        public List<Usuario> ObtenerUsuariosDisponibles(int usuarioId, int limite = 10)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            using var command = new MySqlCommand("ObtenerUsuariosDisponibles", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("p_usuario_id", usuarioId);
            command.Parameters.AddWithValue("p_limite", limite);

            var usuarios = new List<Usuario>();
            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                var usuario = new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Edad = reader.GetInt32("edad"),
                    Genero = reader.GetString("genero"),
                    Carrera = reader.GetString("carrera"),
                    FrasePerfil = reader.GetString("frase_perfil"),
                    CreditosInteraccion = reader.GetInt32("creditos_interaccion"),
                    LikesRecibidos = reader.GetInt32("likes_recibidos")
                };
                usuarios.Add(usuario);
            }
            
            reader.Close();
            
            // Obtener intereses para cada usuario
            foreach (var usuario in usuarios)
            {
                usuario.Intereses = ObtenerIntereses(usuario.Id);
            }

            return usuarios;
        }

        public bool ActualizarCreditos(int usuarioId)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            using var command = new MySqlCommand("ActualizarCreditosDiarios", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("p_usuario_id", usuarioId);

            command.ExecuteNonQuery();
            return true;
        }

        public bool TieneCreditos(int usuarioId)
        {
            ActualizarCreditos(usuarioId);
            
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT creditos_interaccion FROM usuarios WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", usuarioId);

            var creditos = Convert.ToInt32(command.ExecuteScalar());
            return creditos > 0;
        }

        public void AgregarIntereses(int usuarioId, List<string> intereses)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            foreach (var interes in intereses)
            {
                var query = @"INSERT IGNORE INTO usuario_intereses (usuario_id, interes) 
                             VALUES (@usuarioId, @interes)";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@usuarioId", usuarioId);
                command.Parameters.AddWithValue("@interes", interes.Trim().ToLower());
                command.ExecuteNonQuery();
            }
        }

        public List<string> ObtenerIntereses(int usuarioId)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT interes FROM usuario_intereses WHERE usuario_id = @usuarioId";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@usuarioId", usuarioId);

            var intereses = new List<string>();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                intereses.Add(reader.GetString("interes"));
            }

            return intereses;
        }

        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = @"SELECT id, nombre, edad, genero, carrera, frase_perfil, 
                                creditos_interaccion, likes_recibidos 
                         FROM usuarios WHERE activo = TRUE ORDER BY nombre";

            using var command = new MySqlCommand(query, connection);
            var usuarios = new List<Usuario>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                usuarios.Add(new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Edad = reader.GetInt32("edad"),
                    Genero = reader.GetString("genero"),
                    Carrera = reader.GetString("carrera"),
                    FrasePerfil = reader.GetString("frase_perfil"),
                    CreditosInteraccion = reader.GetInt32("creditos_interaccion"),
                    LikesRecibidos = reader.GetInt32("likes_recibidos")
                });
            }

            return usuarios;
        }
    }

    public interface IInteraccionRepository
    {
        bool ProcesarInteraccion(int usuarioId, int objetivoId, TipoInteraccion tipo);
        List<Match> ObtenerMatches(int usuarioId);
        bool YaInteractuo(int usuarioId, int objetivoId);
    }

    public class InteraccionRepository : IInteraccionRepository
    {
        private readonly string _connectionString;

        public InteraccionRepository()
        {
            _connectionString = ConfiguracionDB.CadenaConexion;
        }

        public bool ProcesarInteraccion(int usuarioId, int objetivoId, TipoInteraccion tipo)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            
            try
            {
                // Verificar que el usuario tenga créditos
                var queryCreditos = "SELECT creditos_interaccion FROM usuarios WHERE id = @usuarioId FOR UPDATE";
                using var commandCreditos = new MySqlCommand(queryCreditos, connection, transaction);
                commandCreditos.Parameters.AddWithValue("@usuarioId", usuarioId);
                var creditos = Convert.ToInt32(commandCreditos.ExecuteScalar());

                if (creditos <= 0)
                {
                    transaction.Rollback();
                    return false;
                }

                // Reducir créditos
                var queryReducirCreditos = "UPDATE usuarios SET creditos_interaccion = creditos_interaccion - 1 WHERE id = @usuarioId";
                using var commandReducir = new MySqlCommand(queryReducirCreditos, connection, transaction);
                commandReducir.Parameters.AddWithValue("@usuarioId", usuarioId);
                commandReducir.ExecuteNonQuery();

                // Registrar interacción
                var queryInteraccion = @"INSERT INTO interacciones (usuario_id, usuario_objetivo_id, tipo_interaccion) 
                                        VALUES (@usuarioId, @objetivoId, @tipo)
                                        ON DUPLICATE KEY UPDATE tipo_interaccion = @tipo";
                
                using var commandInteraccion = new MySqlCommand(queryInteraccion, connection, transaction);
                commandInteraccion.Parameters.AddWithValue("@usuarioId", usuarioId);
                commandInteraccion.Parameters.AddWithValue("@objetivoId", objetivoId);
                commandInteraccion.Parameters.AddWithValue("@tipo", tipo.ToString().ToLower());
                
                commandInteraccion.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public List<Match> ObtenerMatches(int usuarioId)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = @"SELECT m.id, m.usuario1_id, m.usuario2_id, m.porcentaje_compatibilidad, 
                                m.fecha_match, m.activo,
                                u1.nombre as nombre_usuario1, u2.nombre as nombre_usuario2
                         FROM matches m
                         JOIN usuarios u1 ON m.usuario1_id = u1.id
                         JOIN usuarios u2 ON m.usuario2_id = u2.id
                         WHERE (m.usuario1_id = @usuarioId OR m.usuario2_id = @usuarioId) 
                           AND m.activo = TRUE
                         ORDER BY m.fecha_match DESC";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@usuarioId", usuarioId);

            var matches = new List<Match>();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                matches.Add(new Match
                {
                    Id = reader.GetInt32("id"),
                    Usuario1Id = reader.GetInt32("usuario1_id"),
                    Usuario2Id = reader.GetInt32("usuario2_id"),
                    PorcentajeCompatibilidad = reader.GetInt32("porcentaje_compatibilidad"),
                    FechaMatch = reader.GetDateTime("fecha_match"),
                    Activo = reader.GetBoolean("activo"),
                    NombreUsuario1 = reader.GetString("nombre_usuario1"),
                    NombreUsuario2 = reader.GetString("nombre_usuario2")
                });
            }

            return matches;
        }

        public bool YaInteractuo(int usuarioId, int objetivoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = @"SELECT COUNT(*) FROM interacciones 
                         WHERE usuario_id = @usuarioId AND usuario_objetivo_id = @objetivoId";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@usuarioId", usuarioId);
            command.Parameters.AddWithValue("@objetivoId", objetivoId);

            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }
    }

    public interface IEstadisticasRepository
    {
        EstadisticasGenerales ObtenerEstadisticasGenerales();
        List<RankingUsuario> ObtenerRankingPopularidad(int limite = 10);
        Dictionary<string, int> ObtenerEstadistica