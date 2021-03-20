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
