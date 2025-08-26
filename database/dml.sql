INSERT INTO usuarios (tipo_documento, numero_documento, nombre, edad, genero, carrera, frase_perfil)
VALUES
('CC', '1001', 'Juan Pérez', 22, 'Masculino', 'Ingeniería de Sistemas', 'Amante de la tecnología y el café'),
('TI', '1002', 'Laura Gómez', 19, 'Femenino', 'Psicología', 'Me encanta leer y escuchar música indie'),
('CC', '1003', 'Carlos Ramírez', 24, 'Masculino', 'Medicina', 'Apasionado por ayudar a los demás'),
('Pasaporte', '1004', 'Emily Torres', 21, 'Femenino', 'Administración de Empresas', 'Viajar y conocer culturas es mi pasión'),
('CC', '1005', 'Andrés López', 23, 'Masculino', 'Arquitectura', 'Me gusta diseñar y crear espacios únicos');


INSERT INTO usuario_contrasenas (usuario_id, contrasena)
VALUES
(1, '1234'),
(2, '1234'),
(3, '1234'),
(4, '1234'),
(5, '1234');

-- ===== INSERTAR INTERESES DE USUARIOS =====
INSERT INTO usuario_intereses (usuario_id, interes) VALUES
-- Ana María (1) - Ingeniería de Sistemas
(1, 'Programación'), (1, 'Gaming'), (1, 'Café'), (1, 'Películas de Sci-Fi'), (1, 'Lectura'),

-- Carlos Eduardo (2) - Ingeniería Industrial
(2, 'Deportes extremos'), (2, 'Viajes'), (2, 'Fotografía'), (2, 'Montañismo'), (2, 'Música Rock'),

-- Sofía (3) - Ingeniería Electrónica
(3, 'Robótica'), (3, 'Música'), (3, 'Baile'), (3, 'Cocina'), (3, 'Yoga'),

-- Miguel Ángel (4) - Ingeniería Civil
(4, 'Arquitectura'), (4, 'Construcción'), (4, 'Deportes'), (4, 'Historia'), (4, 'Café'),

-- Isabella (5) - Ingeniería de Sistemas
(5, 'Programación'), (5, 'Inteligencia Artificial'), (5, 'Café'), (5, 'Anime'), (5, 'Cocina asiática'),

-- Daniel (6) - Medicina
(6, 'Medicina'), (6, 'Fitness'), (6, 'Lectura'), (6, 'Voluntariado'), (6, 'Música clásica'),

-- Valentina (7) - Enfermería
(7, 'Cuidado de la salud'), (7, 'Yoga'), (7, 'Jardinería'), (7, 'Cocina'), (7, 'Mascotas'),

-- Sebastián (8) - Fisioterapia
(8, 'Deportes'), (8, 'Fitness'), (8, 'Rehabilitación'), (8, 'Natación'), (8, 'Música'),

-- Camila (9) - Psicología
(9, 'Psicología'), (9, 'Lectura'), (9, 'Meditación'), (9, 'Arte terapia'), (9, 'Yoga'),

-- Alejandro (10) - Diseño Gráfico
(10, 'Diseño'), (10, 'Arte digital'), (10, 'Fotografía'), (10, 'Gaming'), (10, 'Cine'),

-- Luna (11) - Literatura
(11, 'Lectura'), (11, 'Escritura'), (11, 'Poesía'), (11, 'Teatro'), (11, 'Café'),

-- Emma (12) - Artes Visuales
(12, 'Pintura'), (12, 'Fotografía'), (12, 'Viajes'), (12, 'Museos'), (12, 'Diseño'),

-- Andrés (13) - Música
(13, 'Música'), (13, 'Guitarra'), (13, 'Composición'), (13, 'Conciertos'), (13, 'Grabación'),

-- Gabriela (14) - Administración
(14, 'Negocios'), (14, 'Liderazgo'), (14, 'Networking'), (14, 'Lectura'), (14, 'Fitness'),

-- Mateo (15) - Contaduría
(15, 'Finanzas'), (15, 'Inversiones'), (15, 'Análisis de datos'), (15, 'Lectura'), (15, 'Chess'),

-- Salomé (16) - Economía
(16, 'Economía'), (16, 'Política'), (16, 'Análisis financiero'), (16, 'Debates'), (16, 'Café'),

-- Nicolás (17) - Derecho
(17, 'Derecho'), (17, 'Debates'), (17, 'Historia'), (17, 'Lectura'), (17, 'Política'),

-- Mariana (18) - Comunicación Social
(18, 'Periodismo'), (18, 'Redes sociales'), (18, 'Fotografía'), (18, 'Viajes'), (18, 'Cine'),

-- Diego (19) - Trabajo Social
(19, 'Trabajo social'), (19, 'Voluntariado'), (19, 'Comunidad'), (19, 'Deportes'), (19, 'Música'),

-- Valeria (20) - Biología
(20, 'Biología'), (20, 'Naturaleza'), (20, 'Conservación'), (20, 'Senderismo'), (20, 'Fotografía'),

-- Juan Pablo (21) - Química
(21, 'Química'), (21, 'Investigación'), (21, 'Experimentos'), (21, 'Ciencia ficción'), (21, 'Gaming');

-- ===== INSERTAR INTERACCIONES =====
INSERT INTO interacciones (usuario_id, usuario_objetivo_id, tipo_interaccion) VALUES
-- Ana María (1) interacciones
(1, 2, 'like'), (1, 5, 'like'), (1, 10, 'like'), (1, 13, 'dislike'), (1, 15, 'like'),

-- Carlos Eduardo (2) interacciones
(2, 1, 'like'), (2, 3, 'like'), (2, 8, 'like'), (2, 18, 'dislike'), (2, 20, 'like'),

-- Sofía (3) interacciones  
(3, 2, 'dislike'), (3, 4, 'like'), (3, 13, 'like'), (3, 7, 'like'), (3, 16, 'like'),

-- Miguel Ángel (4) interacciones
(4, 3, 'like'), (4, 7, 'like'), (4, 11, 'like'), (4, 14, 'dislike'), (4, 20, 'like'),

-- Isabella (5) interacciones
(5, 1, 'like'), (5, 10, 'like'), (5, 21, 'like'), (5, 12, 'dislike'), (5, 6, 'like'),

-- Daniel (6) interacciones
(6, 7, 'like'), (6, 9, 'like'), (6, 5, 'like'), (6, 14, 'like'), (6, 18, 'dislike'),

-- Valentina (7) interacciones
(7, 6, 'like'), (7, 3, 'like'), (7, 4, 'like'), (7, 9, 'like'), (7, 19, 'like'),

-- Sebastián (8) interacciones
(8, 2, 'like'), (8, 19, 'like'), (8, 13, 'like'), (8, 20, 'dislike'), (8, 7, 'like'),

-- Camila (9) interacciones
(9, 6, 'like'), (9, 7, 'like'), (9, 11, 'like'), (9, 17, 'like'), (9, 19, 'dislike'),

-- Alejandro (10) interacciones
(10, 1, 'like'), (10, 5, 'like'), (10, 12, 'like'), (10, 18, 'like'), (10, 21, 'dislike'),

-- Más interacciones para crear matches potenciales...
(11, 4, 'like'), (11, 9, 'like'), (12, 10, 'like'), (13, 3, 'like'), (13, 8, 'like'),
(14, 6, 'like'), (15, 1, 'like'), (16, 3, 'like'), (17, 9, 'like'), (18, 10, 'like'),
(19, 7, 'like'), (19, 8, 'like'), (19, 9, 'like'), (20, 2, 'like'), (20, 4, 'like'),
(21, 5, 'like'), (21, 10, 'like');

-- ===== INSERTAR MATCHES (basados en likes mutuos) =====
INSERT INTO matches (usuario1_id, usuario2_id, porcentaje_compatibilidad) VALUES
-- Ana María y Carlos Eduardo (ambos se dieron like)
(1, 2, 45),

-- Isabella y Ana María (ambas programadoras, les gusta el café)
(1, 5, 78),

-- Alejandro y Ana María (ambos en tech/design, gaming)
(1, 10, 62),

-- Sofía y Miguel Ángel (ingenieros)
(3, 4, 55),

-- Sofía y Andrés (ambos músicos)
(3, 13, 71),

-- Daniel y Valentina (ambos en salud)
(6, 7, 89),

-- Isabella y Daniel (compatibilidad de intereses diversos)
(5, 6, 43),

-- Camila y Valentina (ambas en cuidado/bienestar)
(7, 9, 67),

-- Sebastián y Valentina (ambos en salud/fitness)
(7, 8, 82),

-- Alejandro y Emma (ambos artistas)
(10, 12, 76),

-- Camila y Luna (ambas lectoras, introspectivas)
(9, 11, 58),

-- Diego y Sebastián (ambos deportistas/sociales)
(8, 19, 64);

-- ===== CONSULTAS DE VERIFICACIÓN =====
-- Verificar usuarios creados
-- SELECT COUNT(*) as total_usuarios FROM usuarios;

-- Verificar matches creados
-- SELECT u1.nombre as usuario1, u2.nombre as usuario2, m.porcentaje_compatibilidad, m.fecha_match 
-- FROM matches m 
-- JOIN usuarios u1 ON m.usuario1_id = u1.id 
-- JOIN usuarios u2 ON m.usuario2_id = u2.id 
-- ORDER BY m.porcentaje_compatibilidad DESC;

-- Verificar intereses por usuario
-- SELECT u.nombre, GROUP_CONCAT(ui.interes SEPARATOR ', ') as intereses
-- FROM usuarios u 
-- LEFT JOIN usuario_intereses ui ON u.id = ui.usuario_id 
-- GROUP BY u.id, u.nombre 
-- ORDER BY u.nombre;