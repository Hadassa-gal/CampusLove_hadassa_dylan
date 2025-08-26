INSERT INTO usuarios (tipo_documento, numero_documento, nombre, edad, genero, carrera, frase_perfil)
VALUES
('CC', '1001234567', 'Juan Pérez', 22, 'Masculino', 'Ingeniería de Sistemas', 'Amante del café y el código ☕💻'),
('TI', '1023456789', 'Laura Gómez', 19, 'Femenino', 'Psicología', 'Escucho más de lo que hablo ✨'),
('CC', '1019876543', 'Carlos Ramírez', 24, 'Masculino', 'Administración de Empresas', 'Siempre buscando nuevos retos 📈'),
('Pasaporte', 'P123456', 'Emily Torres', 21, 'Femenino', 'Diseño Gráfico', 'Los colores son mi lenguaje favorito 🎨'),
('CC', '1007654321', 'Andrés Morales', 27, 'Masculino', 'Medicina', 'Conectar con las personas es mi pasión ❤️');

INSERT INTO usuario_contrasenas (usuario_id, contrasena, salt)
VALUES
(1, '$2a$10$hashJuan', 'saltJuan'),
(2, '$2a$10$hashLaura', 'saltLaura'),
(3, '$2a$10$hashCarlos', 'saltCarlos'),
(4, '$2a$10$hashEmily', 'saltEmily'),
(5, '$2a$10$hashAndres', 'saltAndres');

INSERT INTO usuario_intereses (usuario_id, interes)
VALUES
(1, 'Programación'),
(1, 'Música'),
(2, 'Lectura'),
(2, 'Yoga'),
(3, 'Negocios'),
(4, 'Arte'),
(4, 'Fotografía'),
(5, 'Deporte'),
(5, 'Ciencia');

INSERT INTO interacciones (usuario_id, usuario_objetivo_id, tipo_interaccion)
VALUES
(1, 2, 'like'),
(2, 1, 'like'),
(1, 3, 'dislike'),
(3, 4, 'like'),
(4, 1, 'like'),
(5, 2, 'like');

INSERT INTO matches (usuario1_id, usuario2_id, porcentaje_compatibilidad)
VALUES
(1, 2, 85),  -- Juan ❤️ Laura
(4, 1, 70);  -- Emily ❤️ Juan
