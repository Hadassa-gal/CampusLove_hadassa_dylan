# Campus Love 💕

Una aplicación de consola en C# que simula un sistema completo de emparejamiento para estudiantes universitarios. Este proyecto implementa arquitectura limpia, principios SOLID y patrones de diseño para crear una simulación interactiva de citas.

## 🎯 Descripción

Campus Love es una aplicación de consola integral que simula un sistema de citas universitario donde los usuarios pueden registrarse, explorar perfiles, dar likes/dislikes y encontrar coincidencias. El sistema incluye características avanzadas como créditos de interacción diarios, algoritmos de compatibilidad y estadísticas completas.

## ✨ Características

### Funcionalidad Principal
- **Registro de Usuarios**: Creación completa de perfil con nombre, edad, género, intereses, carrera y frase de perfil
- **Exploración de Perfiles**: Ver perfiles disponibles uno por uno con opciones de like/dislike
- **Sistema de Matches**: Emparejamiento automático cuando dos usuarios se dan like mutuamente
- **Lista de Matches**: Ver todas las coincidencias actuales de un usuario
- **Créditos Diarios**: Sistema de likes limitados con renovación automática de créditos
- **Panel de Estadísticas**: Analíticas potenciadas por LINQ mostrando usuarios destacados, tasas de match y más

### Características Avanzadas
- **Simulación Multi-usuario**: Soporte para múltiples usuarios concurrentes
- **Algoritmo de Compatibilidad**: Emparejamiento inteligente basado en intereses, edad y carrera
- **Gestión de Créditos**: Lógica matemática para límites de interacción diarios
- **Formato de Datos**: CultureInfo y NumberFormat para presentación adecuada de datos
- **Validación de Entrada**: Validación integral para todas las entradas del usuario

## 🛠️ Tecnologías

- **Lenguaje**: C# (.NET 8.0)
- **Base de Datos**: MySQL
- **IDE**: Visual Studio Code
- **Frameworks**: 
  - Entity Framework Core (para acceso a datos)
  - FluentValidation (para validación de entrada)
- **Librerías**:
  - System.Data
  - System.Globalization
  - System.Linq

## 🗄️ Diseño de Base de Datos

### Diagrama de Entidad-Relación

El sistema utiliza cuatro tablas principales:

#### Tabla de Usuarios (`usuarios`)
- Almacena información del perfil del usuario
- Incluye sistema de créditos y seguimiento de actividad
- Aplica restricciones de edad y validación de género

#### Tabla de Intereses de Usuario (`usuario_intereses`)
- Relación muchos-a-muchos para intereses del usuario
- Diseño normalizado para gestión flexible de intereses

#### Tabla de Interacciones (`interacciones`)
- Registra todas las interacciones de like/dislike
- Previene interacciones duplicadas entre usuarios
- Marca de tiempo para analíticas

#### Tabla de Matches (`matches`)
- Almacena matches confirmados entre usuarios
- Incluye cálculo de porcentaje de compatibilidad
- Soporta desactivación de matches

### Esquema de Base de Datos

```sql
-- Tabla de usuarios con datos completos de perfil
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

-- Intereses de usuario con restricciones de clave foránea
CREATE TABLE usuario_intereses (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    interes VARCHAR(50) NOT NULL,
    FOREIGN KEY (usuario_id) REFERENCES usuarios(id) ON DELETE CASCADE,
    UNIQUE KEY unique_user_interest (usuario_id, interes)
);

-- Sistema de seguimiento de interacciones
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

-- Matches con puntuación de compatibilidad
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
```

## 🚀 Instalación

### Prerrequisitos
- .NET 8.0 SDK
- MySQL Server 8.0+
- Visual Studio Code (recomendado)

### Instrucciones de Configuración

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/Hadassa-gal/CampusLove_hadassa_dylan.git
   cd CampusLove_hadassa_dylan
   ```

2. **Configuración de Base de Datos**
   ```bash
   # Crear base de datos y tablas
   mysql -u campus2023 -pcampus2023
   ```

3. **Instalar Dependencias**
   ```bash
   dotnet restore
   ```

4. **Configurar Cadena de Conexión**
   ```bash
   # Actualizar appsettings.json con tu conexión MySQL
   ```

5. **Ejecutar la Aplicación**
   ```bash
   dotnet run
   ```
## 🎨 Patrones de Diseño

### Patrón Factory
- **UserFactory**: Crea instancias de usuario con validación adecuada
- **InteractionFactory**: Genera objetos de interacción con marca de tiempo

### Patrón Strategy
- **MatchingStrategy**: Diferentes algoritmos para cálculo de compatibilidad
  - `InterestBasedMatching`: Matches basados en intereses comunes
  - `AgeBasedMatching`: Emparejamiento por proximidad de edad
  - `CareerBasedMatching`: Mismo campo de carrera

### Patrón Repository
- **IUserRepository**: Abstrae el acceso a datos de usuario
- **IInteractionRepository**: Gestiona datos de interacción
- **IMatchRepository**: Maneja persistencia de matches

### Patrón Singleton
- **DatabaseContext**: Instancia única de conexión a base de datos
- **StatisticsService**: Cálculo global de estadísticas

## 🏛️ Implementación de Principios SOLID

### Principio de Responsabilidad Única (SRP)
- **UserService**: Solo maneja operaciones relacionadas con usuarios
- **MatchService**: Exclusivamente gestiona lógica de emparejamiento
- **StatisticsService**: Dedicado a analíticas y reportes

### Principio Abierto/Cerrado (OCP)
- **IMatchingStrategy**: Abierto para extensión con nuevos algoritmos de matching
- **IValidator**: Framework de validación extensible

### Principio de Sustitución de Liskov (LSP)
- Todas las implementaciones de strategy son intercambiables
- Las implementaciones de repository se pueden intercambiar sin afectar la lógica de negocio

### Principio de Segregación de Interfaces (ISP)
- **IUserOperations**: Operaciones específicas de usuario
- **IMatchOperations**: Operaciones específicas de match
- **IStatisticsOperations**: Operaciones de analíticas

### Principio de Inversión de Dependencias (DIP)
- Los servicios dependen de abstracciones (interfaces), no de implementaciones concretas
- Inyección de dependencias para todas las dependencias de servicio

### Guías de Estilo de Código
- Seguir convenciones de nomenclatura de C#
- Usar nombres significativos para variables y métodos

## 👥 Autores

- **Hadassa** - *Trabajo inicial y estructuracion*
- **Dylan** - *Mysql-database*

## 🙏 Agradecimientos

- Principios de Arquitectura Limpia por Robert C. Martin
- Inspiración de Patrones de Diseño de Gang of Four
- Guías de implementación de principios SOLID
- Comunidad .NET por mejores prácticas

---
