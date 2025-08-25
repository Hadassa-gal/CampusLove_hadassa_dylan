-- Creación de la base de datos
CREATE DATABASE IF NOT EXISTS campus_love;
USE campus_love;

-- Tabla de usuarios
CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    edad INT NOT NULL CHECK (edad >= 18 AND edad <= 100),
    genero ENUM('Masculino','Femenino','Otro') NOT NULL,
    carrera VARCHAR(100) NOT NULL,
    frase_perfil TEXT,
    creditos_interaccion INT DEFAULT 10,
    ultima_recarga_creditos DATE DEFAULT (CURRENT_DATE),
    likes_recibidos INT DEFAULT 0,
    fecha_registro TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    activo BOOLEAN DEFAULT TRUE
);

-- Tabla de intereses de usuarios
CREATE TABLE usuario_intereses (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    interes VARCHAR(50) NOT NULL,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    UNIQUE KEY unique_user_interest (usuario_id, interes)
);

-- Tabla de interacciones (likes y dislikes)
CREATE TABLE interacciones (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    usuario_objetivo_id INT NOT NULL,
    tipo_interaccion ENUM('like', 'dislike') NOT NULL,
    fecha_interaccion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (usuario_objetivo_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    UNIQUE KEY unique_interaction (usuario_id, usuario_objetivo_id)
);

-- Tabla de matches
CREATE TABLE matches (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario1_id INT NOT NULL,
    usuario2_id INT NOT NULL,
    porcentaje_compatibilidad INT CHECK(porcentaje_compatibilidad BETWEEN 0 AND 100),
    fecha_match TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (usuario1_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    FOREIGN KEY (usuario2_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    UNIQUE KEY unique_match (usuario1_id, usuario2_id),
    CHECK (usuario1_id != usuario2_id)
);

-- Índices para mejorar rendimiento
CREATE INDEX idx_usuarios_edad ON usuarios(edad);
CREATE INDEX idx_usuarios_carrera ON usuarios(carrera);
CREATE INDEX idx_interacciones_usuario ON interacciones(usuario_id);
CREATE INDEX idx_interacciones_objetivo ON interacciones(usuario_objetivo_id);
CREATE INDEX idx_interacciones_tipo ON interacciones(tipo_interaccion);
CREATE INDEX idx_matches_usuario1 ON matches(usuario1_id);
CREATE INDEX idx_matches_usuario2 ON matches(usuario2_id);

-- Datos de ejemplo - Usuarios
INSERT INTO usuarios (nombre, edad, genero, carrera, frase_perfil) VALUES
('Ana García López', 22, 'Femenino', 'Ingeniería de Software', 'Amante de la tecnología y los libros 📚'),
('Carlos Rodríguez Silva', 24, 'Masculino', 'Diseño Gráfico', 'Creativo por naturaleza 🎨'),
('Laura Martínez Torres', 21, 'Femenino', 'Administración de Empresas', 'Exploradora del mundo 🌍'),
('David López Hernández', 23, 'Masculino', 'Ingeniería de Sistemas', 'Código y café ☕️'),
('Sofía Hernández Ruiz', 25, 'Femenino', 'Psicología', 'Ayudando a otros a crecer 🌱'),
('Miguel Ángel Pérez', 26, 'Masculino', 'Arquitectura', 'Construyendo el futuro 🏗️'),
('Isabella González', 20, 'Femenino', 'Medicina', 'Salvando vidas 🏥'),
('Alejandro Jiménez', 27, 'Masculino', 'Ingeniería Civil', 'Moviendo montañas ⛰️'),
('Valentina Castro', 23, 'Femenino', 'Comunicación Social', 'Contando historias que importan 📰'),
('Santiago Morales', 22, 'Masculino', 'Economía', 'Números y estrategias 📊'),
('Camila Vargas', 24, 'Femenino', 'Derecho', 'Luchando por la justicia ⚖️'),
('Andrés Felipe Ruiz', 25, 'Masculino', 'Ingeniería Industrial', 'Optimizando procesos 🔧'),
('Daniela Ospina', 21, 'Femenino', 'Biología', 'Explorando la vida 🔬'),
('Juan Pablo Torres', 26, 'Masculino', 'Música', 'Creando melodías 🎵'),
('María José Ramírez', 20, 'Femenino', 'Arte y Diseño', 'Pintando emociones 🎭');

-- Intereses de ejemplo
INSERT INTO usuario_intereses (usuario_id, interes) VALUES
-- Ana (ID: 1)
(1, 'tecnología'), (1, 'lectura'), (1, 'música'), (1, 'cine'), (1, 'programación'),
-- Carlos (ID: 2)
(2, 'arte'), (2, 'fotografía'), (2, 'música'), (2, 'viajes'), (2, 'diseño'),
-- Laura (ID: 3)
(3, 'viajes'), (3, 'cocina'), (3, 'fotografía'), (3, 'deportes'), (3, 'administración'),
-- David (ID: 4)
(4, 'programación'), (4, 'música'), (4, 'lectura'), (4, 'videojuegos'), (4, 'tecnología'),
-- Sofía (ID: 5)
(5, 'psicología'), (5, 'baile'), (5, 'cine'), (5, 'voluntariado'), (5, 'lectura'),
-- Miguel (ID: 6)
(6, 'arquitectura'), (6, 'arte'), (6, 'fotografía'), (6, 'viajes'), (6, 'diseño'),
-- Isabella (ID: 7)
(7, 'medicina'), (7, 'deportes'), (7, 'lectura'), (7, 'voluntariado'), (7, 'ciencias'),
-- Alejandro (ID: 8)
(8, 'ingeniería'), (8, 'deportes'), (8, 'música'), (8, 'tecnología'), (8, 'construcción'),
-- Valentina (ID: 9)
(9, 'comunicación'), (9, 'escritura'), (9, 'cine'), (9, 'fotografía'), (9, 'periodismo'),
-- Santiago (ID: 10)
(10, 'economía'), (10, 'finanzas'), (10, 'deportes'), (10, 'lectura'), (10, 'análisis'),
-- Camila (ID: 11)
(11, 'derecho'), (11, 'debate'), (11, 'lectura'), (11, 'justicia'), (11, 'política'),
-- Andrés Felipe (ID: 12)
(12, 'ingeniería'), (12, 'optimización'), (12, 'deportes'), (12, 'tecnología'), (12, 'análisis'),
-- Daniela (ID: 13)
(13, 'biología'), (13, 'ciencias'), (13, 'investigación'), (13, 'naturaleza'), (13, 'laboratorio'),
-- Juan Pablo (ID: 14)
(14, 'música'), (14, 'composición'), (14, 'guitarra'), (14, 'piano'), (14, 'arte'),
-- María José (ID: 15)
(15, 'arte'), (15, 'pintura'), (15, 'diseño'), (15, 'creatividad'), (15, 'exposiciones');

-- Interacciones de ejemplo (likes y dislikes)
INSERT INTO interacciones (usuario_id, usuario_objetivo_id, tipo_interaccion) VALUES
-- Likes mutuos que generarán matches
(1, 4, 'like'),  -- Ana -> David (ambos programación, tecnología)
(4, 1, 'like'),  -- David -> Ana = MATCH
(2, 6, 'like'),  -- Carlos -> Miguel (ambos arte, diseño)
(6, 2, 'like'),  -- Miguel -> Carlos = MATCH
(7, 13, 'like'), -- Isabella -> Daniela (ambas ciencias)
(13, 7, 'like'), -- Daniela -> Isabella = MATCH
(9, 11, 'like'), -- Valentina -> Camila (ambas humanidades)
(11, 9, 'like'), -- Camila -> Valentina = MATCH
(14, 15, 'like'), -- Juan Pablo -> María José (ambos arte)
(15, 14, 'like'), -- María José -> Juan Pablo = MATCH

-- Likes unilaterales
(3, 8, 'like'),  -- Laura -> Alejandro
(5, 1, 'like'),  -- Sofía -> Ana
(10, 12, 'like'), -- Santiago -> Andrés Felipe
(8, 3, 'like'),  -- Alejandro -> Laura
(12, 10, 'like'), -- Andrés Felipe -> Santiago = MATCH con el anterior

-- Algunos dislikes
(1, 2, 'dislike'), -- Ana no le gusta Carlos
(3, 4, 'dislike'), -- Laura no le gusta David
(5, 6, 'dislike'), -- Sofía no le gusta Miguel
(7, 8, 'dislike'), -- Isabella no le gusta Alejandro
(9, 10, 'dislike'); -- Valentina no le gusta Santiago

-- Matches generados manualmente (los triggers automáticos pueden no estar habilitados)
INSERT INTO matches (usuario1_id, usuario2_id, porcentaje_compatibilidad) VALUES
(1, 4, 85),   -- Ana y David
(2, 6, 78),   -- Carlos y Miguel  
(7, 13, 82),  -- Isabella y Daniela
(9, 11, 75),  -- Valentina y Camila
(14, 15, 88), -- Juan Pablo y María José
(10, 12, 72); -- Santiago y Andrés Felipe

-- Actualizar algunos likes_recibidos manualmente
UPDATE usuarios SET likes_recibidos = 3 WHERE id = 1;  -- Ana
UPDATE usuarios SET likes_recibidos = 2 WHERE id = 4;  -- David
UPDATE usuarios SET likes_recibidos = 2 WHERE id = 6;  -- Miguel
UPDATE usuarios SET likes_recibidos = 2 WHERE id = 2;  -- Carlos
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 13; -- Daniela
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 7;  -- Isabella
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 11; -- Camila
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 9;  -- Valentina
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 15; -- María José
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 14; -- Juan Pablo
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 8;  -- Alejandro
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 3;  -- Laura
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 12; -- Andrés Felipe
UPDATE usuarios SET likes_recibidos = 1 WHERE id = 10; -- Santiago

