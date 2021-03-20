USE tempdb
go

-- Crear la base de datos
CREATE DATABASE Reservaciones
GO

-- Utilizar la base de datos
USE Reservaciones
GO

-- Crear los schema de la base de datos
CREATE SCHEMA Habitaciones
GO

CREATE SCHEMA Usuarios
GO

-- Crear las tablas de Habitación, Reservación y Usuario
CREATE TABLE Habitaciones.Habitacion (
	id INT NOT NULL IDENTITY(1,1),
	descripcion VARCHAR(255) NOT NULL,
	numero INT NOT NULL,
	estado VARCHAR(20) NOT NULL,
	CONSTRAINT PK_Habitacion_id
		PRIMARY KEY CLUSTERED (id)
)
GO

CREATE TABLE Habitaciones.Reservacion (
	id INT NOT NULL IDENTITY(1000,1),
	nombreCompleto VARCHAR(255) NOT NULL,
	habitacion INT NOT NULL,
	fechaIngreso DATETIME NOT NULL,
	fechaSalida DATETIME NOT NULL,
	estado BIT NOT NULL,
	CONSTRAINT PK_Reservacion_id
		PRIMARY KEY CLUSTERED (id)
)
GO

CREATE TABLE Usuarios.Usuario (
	id INT NOT NULL IDENTITY (500, 1),
	nombreCompleto VARCHAR(255) NOT NULL,
	username VARCHAR(100) NOT NULL,
	password VARCHAR(100) NOT NULL,
	estado BIT NOT NULL,
	CONSTRAINT PK_Usuario_id
		PRIMARY KEY CLUSTERED (id)
)
GO

-- Restricciones de las tablas

-- El estado de las habitaciones es: ocupada, disponible, mantenimiento y fuera de servicio.
ALTER TABLE Habitaciones.Habitacion WITH CHECK
	ADD CONSTRAINT CHK_Habitaciones_Habitacion$EstadoHabitaciones
	CHECK (estado IN('OCUPADA', 'DISPONIBLE', 'MANTENIMIENTO', 'FUERADESERVICIO'))
GO

-- Llave foránea para las habitaciones
ALTER TABLE Habitaciones.Reservacion
	ADD CONSTRAINT FK_Habitaciones_Reservacion$TieneUna$Habitaciones_Habitaciones
	FOREIGN KEY (habitacion) REFERENCES Habitaciones.Habitacion(id)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

-- La fecha de ingreso no puede ser menor que la fecha actual
ALTER TABLE Habitaciones.Reservacion WITH CHECK
	ADD CONSTRAINT CHK_Habitaciones_Habitacion$VerificarFechaIngreso
	CHECK (fechaIngreso >= GETDATE())
GO

-- La fecha de salida no puede ser menor o igual a la fecha de ingreso
ALTER TABLE Habitaciones.Reservacion WITH CHECK
	ADD CONSTRAINT CHK_Habitaciones_Habitacion$VerificarFechaSalida
	CHECK (fechaSalida > fechaIngreso)
GO

-- No puede existir nombres de usuarios repetidos
ALTER TABLE Usuarios.Usuario
	ADD CONSTRAINT AK_Usuarios_Usuario_username
	UNIQUE NONCLUSTERED (username)
GO

-- La contraseña debe contener al menos 6 caracteres
ALTER TABLE Usuarios.Usuario WITH CHECK
	ADD CONSTRAINT CHK_Usuarios_Usuario$VerificarLongitudContraseña
	CHECK (LEN(password) >= 6)
GO
