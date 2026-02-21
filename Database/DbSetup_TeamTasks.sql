/*
*******************************************************************************
Autor:          Yeisson Rodriguez
Fecha:          18/02/2026
Descripción:    Script principal para la creación de la base de datos y sus tablas.
Proyecto:       TeamTasksSample
Versión:        1.0
*******************************************************************************
*/
USE [master]
GO

/****** Object:  Database [TeamTasksSample]    Script Date: 17/02/2026 17:18:48 ******/
CREATE DATABASE [TeamTasksSample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TeamTasksSample', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TeamTasksSample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TeamTasksSample_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TeamTasksSample_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TeamTasksSample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO


--Creación de schemas
CREATE SCHEMA Core;
GO

--Creación tabla Developers
CREATE TABLE Core.Developers 
(
	DeveloperId UNIQUEIDENTIFIER PRIMARY  KEY DEFAULT NEWID(),
	FirstName NVARCHAR(100) NOT NULL, 
	LastName NVARCHAR(100) NOT NULL, 
	Email NVARCHAR(100) NOT NULL, 
	IsActive BIT DEFAULT 1,
	CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- Insertar los 20 registros
INSERT INTO Core.Developers(FirstName, LastName, Email, IsActive)
VALUES 
('Alejandro', 'García', 'alejandro.garcia@dev.com', 1),
('Beatriz', 'López', 'beatriz.lopez@dev.com', 1),
('Carlos', 'Martínez', 'carlos.martinez@dev.com', 1),
('Diana', 'Rodríguez', 'diana.rodriguez@dev.com', 1),
('Eduardo', 'Sánchez', 'eduardo.sanchez@dev.com', 1),
('Fernanda', 'Pérez', 'fernanda.perez@dev.com', 1),
('Gabriel', 'Gómez', 'gabriel.gomez@dev.com', 1),
('Helena', 'Díaz', 'helena.diaz@dev.com', 1),
('Iván', 'Hernández', 'ivan.hernandez@dev.com', 1),
('Julia', 'Álvarez', 'julia.alvarez@dev.com', 1),
('Kevin', 'Torres', 'kevin.torres@dev.com', 1),
('Laura', 'Ruiz', 'laura.ruiz@dev.com', 1),
('Mario', 'Vázquez', 'mario.vazquez@dev.com', 0),
('Natalia', 'Castro', 'natalia.castro@dev.com', 1),
('Oscar', 'Morales', 'oscar.morales@dev.com', 1),
('Patricia', 'Jiménez', 'patricia.jimenez@dev.com', 0),
('Ricardo', 'Reyes', 'ricardo.reyes@dev.com', 1),
('Sofía', 'Ortiz', 'sofia.ortiz@dev.com', 1),
('Tomás', 'Gutiérrez', 'tomas.gutierrez@dev.com', 1),
('Valeria', 'Ramos', 'valeria.ramos@dev.com', 0);

--Creacion tabla ProjectStatus
CREATE TABLE Core.ProjectStatuses
(
	StatusId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Name NVARCHAR(100) NOT NULL, 
	IsActive BIT NOT NULL DEFAULT 1, 
	CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

--Insertar valores Tabla ProjectStatuses
INSERT INTO Core.ProjectStatuses (Name)
VALUES
('Planned'), ('InProgress'), ('Completed')

--Creacion tabla Projects
CREATE TABLE Core.Projects
(
	ProjectId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
	Name NVARCHAR(100) NOT NULL, 
	ClientName NVARCHAR(100) NOT NULL,
	StartDate DATETIME2 NOT NULL, 
	EndDate DATETIME2 NOT NULL, 
	StatudId UNIQUEIDENTIFIER NOT NULL REFERENCES  Core.ProjectStatuses (StatusId), 

);

--Creación tabla TaskStatuses
CREATE TABLE Core.TaskStatuses
(
	StatusId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
	Name NVARCHAR(100) NOT NULL, 
	IsActive BIT NOT NULL DEFAULT 1, 
	CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

--Insertar valores tabla TaskStatuses
INSERT INTO  Core.TaskStatuses (Name)
VALUES 
('ToDo'), ('InProgress'), ('Blocked'), ('Completed')


--Creación tabla Priority
CREATE TABLE Core.Priority
(
	PriorityId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
	Name NVARCHAR(100) NOT NULL, 
	IsActive BIT NOT NULL DEFAULT 1, 
	CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

--Insertar valores tabla Priority
INSERT INTO Core.Priority (Name)
VALUES 
('Low'), ('Medium'), ('High')


--Creaciín tabla Task
CREATE TABLE Core.Tasks
(
	TaskId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
	ProjectId UNIQUEIDENTIFIER NOT NULL REFERENCES Core.Projects(ProjectId),
	Title NVARCHAR(100) NOT NULL, 
	Description NVARCHAR(MAX),
	AssignedId UNIQUEIDENTIFIER NOT NULL REFERENCES Core.Developers (DeveloperId), 
	StatusId UNIQUEIDENTIFIER NOT NULL REFERENCES Core.TaskStatuses (StatusId),
	PriorityId UNIQUEIDENTIFIER NOT NULL REFERENCES Core.Priority (PriorityId),
	EstimatedComplexity TINYINT NOT NULL CONSTRAINT CK_Task_Complexity CHECK (EstimatedComplexity >= 1 AND EstimatedComplexity <= 5),
	DueDate DATETIME2 NOT NULL, 
	CompletionDate DATETIME2,
	CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);