-- Creación de la base de datos
CREATE DATABASE IF NOT EXISTS campus_love;
USE campus_love;

-- Tabla de usuarios
CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    edad INT NOT NULL CHECK (edad >= 15 AND edad <= 45),
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

