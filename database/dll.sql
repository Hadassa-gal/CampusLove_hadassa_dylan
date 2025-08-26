-- Creación de la base de datos
DROP DATABASE IF EXISTS campus_love;
CREATE DATABASE IF NOT EXISTS campus_love;
USE campus_love;

-- Tabla de usuarios (CON AUTO_INCREMENT)
CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    tipo_documento ENUM('CC', 'CE', 'TI', 'Pasaporte', 'Otro') NOT NULL,
    numero_documento VARCHAR(20) NOT NULL UNIQUE,
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

-- Tabla de usuarios y contraseñas (YA TIENE AUTO_INCREMENT)
CREATE TABLE usuario_contrasenas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    salt VARCHAR(255), 
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_actualizacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE
);

-- Tabla de intereses de usuarios (CORREGIDA - estaba duplicada)
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
    UNIQUE KEY unique_interaction (usuario_id, usuario_objetivo_id),
    CHECK (usuario_id != usuario_objetivo_id) 
    UNIQUE KEY unique_interaction (usuario_id, usuario_objetivo_id),
    CHECK (usuario_id != usuario_objetivo_id) 
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

-- Índices adicionales para mejorar performance
CREATE INDEX idx_usuarios_activo ON usuarios(activo);
CREATE INDEX idx_usuarios_carrera ON usuarios(carrera);
CREATE INDEX idx_usuarios_edad ON usuarios(edad);
CREATE INDEX idx_interacciones_fecha ON interacciones(fecha_interaccion);
CREATE INDEX idx_matches_fecha ON matches(fecha_match);
CREATE INDEX idx_matches_activo ON matches(activo);