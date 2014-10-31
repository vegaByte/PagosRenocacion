USE [dbpagoscontratos]
GO

INSERT INTO [dbo].[prc_usuarios]
           ([id_usuarios]
           ,[password]
           ,[nivel]
           ,[nombre]
           ,[puesto])
     VALUES
           (<id_usuarios, varchar(15),>
           ,<password, varchar(45),>
           ,<nivel, varchar(45),>
           ,<nombre, varchar(120),>
           ,<puesto, varchar(45),>)
GO

