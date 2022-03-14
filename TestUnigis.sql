CREATE DATABASE TestUnigis
GO

USE TestUnigis
GO

CREATE TABLE dbo.tb_Razas 
   (idRaza int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
   Raza varchar(25) NOT NULL,  
   Activo bit NOT NULL DEFAULT 1)  
GO  

CREATE TABLE dbo.tb_Imagenes 
   (idImagen int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
   Imagen varchar(25) NOT NULL,
   RazaId int NOT NULL,
   Activo bit NOT NULL DEFAULT 1
   CONSTRAINT fk_Imagenes FOREIGN KEY (RazaId) REFERENCES tb_Razas(idRaza))  
GO  

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT count(*) FROM tb_Razas
union all
SELECT count(*) FROM tb_Imagenes