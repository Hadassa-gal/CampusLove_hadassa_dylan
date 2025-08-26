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


INSERT INTO usuario_intereses (usuario_id, interes)
VALUES
(1, 'Programación'),
(1, 'Videojuegos'),
(2, 'Psicología'),
(2, 'Libros'),
(3, 'Deportes'),
(3, 'Voluntariado'),
(4, 'Viajes'),
(4, 'Cultura'),
(5, 'Diseño'),
(5, 'Arte');

INSERT INTO matches (usuario1_id, usuario2_id, porcentaje_compatibilidad, activo)
VALUES
(1, 2, 85, TRUE),   -- Juan & Laura
(1, 4, 70, TRUE),   -- Juan & Emily
(2, 3, 65, TRUE),   -- Laura & Carlos
(3, 5, 90, TRUE),   -- Carlos & Andrés
(4, 5, 75, TRUE);   -- Emily & Andrés