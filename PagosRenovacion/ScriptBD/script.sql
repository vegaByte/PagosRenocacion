USE [master]
GO
/****** Object:  Database [dbpagoscontratos]    Script Date: 14/11/2014 14:47:46 ******/
CREATE DATABASE [dbpagoscontratos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbpagoscontratos', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbpagoscontratos.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbpagoscontratos_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbpagoscontratos_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbpagoscontratos] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbpagoscontratos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbpagoscontratos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [dbpagoscontratos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbpagoscontratos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbpagoscontratos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbpagoscontratos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbpagoscontratos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET RECOVERY FULL 
GO
ALTER DATABASE [dbpagoscontratos] SET  MULTI_USER 
GO
ALTER DATABASE [dbpagoscontratos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbpagoscontratos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbpagoscontratos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbpagoscontratos] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [dbpagoscontratos]
GO
/****** Object:  StoredProcedure [dbo].[prc_sp_update_pagos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[prc_sp_update_pagos]

AS
BEGIN

UPDATE [dbo].[prc_pagos]
   SET [activo] = 0
 WHERE [date_final] < GETDATE()
 AND [activo] = 1

END

GO
/****** Object:  Table [dbo].[prc_actividades]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_actividades](
	[id_actividades] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](45) NOT NULL,
 CONSTRAINT [PK_prc_actividades] PRIMARY KEY CLUSTERED 
(
	[id_actividades] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_conceptos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[prc_conceptos](
	[id_conceptos] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [text] NOT NULL,
 CONSTRAINT [PK_prc_conceptos] PRIMARY KEY CLUSTERED 
(
	[id_conceptos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[prc_contratos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_contratos](
	[id_contratos] [int] IDENTITY(1,1) NOT NULL,
	[concepto] [varchar](200) NOT NULL,
	[fk_id_actividades] [int] NOT NULL,
	[fk_id_usuarios] [varchar](15) NOT NULL,
 CONSTRAINT [PK_prc_contratos] PRIMARY KEY CLUSTERED 
(
	[id_contratos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_date_contratos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_date_contratos](
	[id_date_contratos] [int] IDENTITY(1,1) NOT NULL,
	[fecha_nota] [datetime] NOT NULL,
	[nota] [varchar](120) NULL,
	[fk_id_contratos] [int] NOT NULL,
 CONSTRAINT [PK_prc_date_contratos] PRIMARY KEY CLUSTERED 
(
	[id_date_contratos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_date_pagos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_date_pagos](
	[id_date_pagos] [int] IDENTITY(1,1) NOT NULL,
	[fecha_nota] [datetime] NOT NULL,
	[nota] [varchar](120) NULL,
	[fk_id_pagos] [int] NOT NULL,
	[fk_id_status] [int] NOT NULL,
	[monto] [float] NULL,
 CONSTRAINT [PK_prc_date_pagos] PRIMARY KEY CLUSTERED 
(
	[id_date_pagos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_pagos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_pagos](
	[id_pagos] [int] IDENTITY(1,1) NOT NULL,
	[date_inicio] [datetime] NOT NULL,
	[date_final] [datetime] NOT NULL,
	[fk_id_conceptos] [int] NOT NULL,
	[fk_id_tipopagos] [int] NOT NULL,
	[fk_id_usuarios] [varchar](15) NOT NULL,
	[activo] [bit] NULL,
	[fk_id_actividades] [int] NOT NULL,
 CONSTRAINT [PK_prc_pagos] PRIMARY KEY CLUSTERED 
(
	[id_pagos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_recargos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_recargos](
	[id_recargos] [int] IDENTITY(1,1) NOT NULL,
	[fk_id_date_pagos] [int] NOT NULL,
	[nota] [text] NULL,
	[monto] [float] NOT NULL,
	[path_evidencia] [varchar](150) NULL,
 CONSTRAINT [PK_prc_recargos] PRIMARY KEY CLUSTERED 
(
	[id_recargos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_status]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_status](
	[id_status] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](45) NOT NULL,
 CONSTRAINT [PK_prc_status] PRIMARY KEY CLUSTERED 
(
	[id_status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_tipopagos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_tipopagos](
	[id_tipopagos] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](45) NOT NULL,
 CONSTRAINT [PK_prc_tipopagos] PRIMARY KEY CLUSTERED 
(
	[id_tipopagos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prc_usuarios]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prc_usuarios](
	[id_usuarios] [varchar](15) NOT NULL,
	[password] [varchar](45) NOT NULL,
	[nivel] [varchar](45) NOT NULL,
	[nombre] [varchar](120) NOT NULL,
	[puesto] [varchar](45) NULL,
 CONSTRAINT [PK_prc_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuarios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[prc_view_date_contratos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[prc_view_date_contratos]
AS
SELECT     A.concepto, C.nombre, B.fecha_nota, B.nota
FROM         dbo.prc_contratos AS A INNER JOIN
                      dbo.prc_date_contratos AS B ON A.id_contratos = B.fk_id_contratos INNER JOIN
                      dbo.prc_actividades AS C ON A.fk_id_actividades = C.id_actividades


GO
/****** Object:  View [dbo].[prc_view_date_pagos]    Script Date: 14/11/2014 14:47:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[prc_view_date_pagos] AS 
SELECT     A.nombre, B.fecha_nota, D.nombre AS Expr1, B.nota
FROM         dbo.prc_pagos AS C INNER JOIN
                      dbo.prc_date_pagos AS B ON C.id_pagos = B.fk_id_pagos INNER JOIN
                      dbo.prc_status AS D ON B.fk_id_status = D.id_status INNER JOIN
                      dbo.prc_conceptos AS A ON C.fk_id_conceptos = A.id_conceptos

GO
ALTER TABLE [dbo].[prc_pagos] ADD  CONSTRAINT [DF_prc_pagos_fk_id_actividades]  DEFAULT ((1)) FOR [fk_id_actividades]
GO
ALTER TABLE [dbo].[prc_contratos]  WITH CHECK ADD  CONSTRAINT [FK_prc_contratos_prc_actividades] FOREIGN KEY([fk_id_actividades])
REFERENCES [dbo].[prc_actividades] ([id_actividades])
GO
ALTER TABLE [dbo].[prc_contratos] CHECK CONSTRAINT [FK_prc_contratos_prc_actividades]
GO
ALTER TABLE [dbo].[prc_contratos]  WITH CHECK ADD  CONSTRAINT [FK_prc_contratos_prc_usuarios] FOREIGN KEY([fk_id_usuarios])
REFERENCES [dbo].[prc_usuarios] ([id_usuarios])
GO
ALTER TABLE [dbo].[prc_contratos] CHECK CONSTRAINT [FK_prc_contratos_prc_usuarios]
GO
ALTER TABLE [dbo].[prc_date_contratos]  WITH CHECK ADD  CONSTRAINT [FK_prc_date_contratos_prc_contratos] FOREIGN KEY([fk_id_contratos])
REFERENCES [dbo].[prc_contratos] ([id_contratos])
GO
ALTER TABLE [dbo].[prc_date_contratos] CHECK CONSTRAINT [FK_prc_date_contratos_prc_contratos]
GO
ALTER TABLE [dbo].[prc_date_pagos]  WITH CHECK ADD  CONSTRAINT [FK_prc_date_pagos_prc_pagos] FOREIGN KEY([fk_id_pagos])
REFERENCES [dbo].[prc_pagos] ([id_pagos])
GO
ALTER TABLE [dbo].[prc_date_pagos] CHECK CONSTRAINT [FK_prc_date_pagos_prc_pagos]
GO
ALTER TABLE [dbo].[prc_date_pagos]  WITH CHECK ADD  CONSTRAINT [FK_prc_date_pagos_prc_status] FOREIGN KEY([fk_id_status])
REFERENCES [dbo].[prc_status] ([id_status])
GO
ALTER TABLE [dbo].[prc_date_pagos] CHECK CONSTRAINT [FK_prc_date_pagos_prc_status]
GO
ALTER TABLE [dbo].[prc_pagos]  WITH CHECK ADD  CONSTRAINT [FK_prc_pagos_prc_actividades] FOREIGN KEY([fk_id_actividades])
REFERENCES [dbo].[prc_actividades] ([id_actividades])
GO
ALTER TABLE [dbo].[prc_pagos] CHECK CONSTRAINT [FK_prc_pagos_prc_actividades]
GO
ALTER TABLE [dbo].[prc_pagos]  WITH CHECK ADD  CONSTRAINT [FK_prc_pagos_prc_conceptos] FOREIGN KEY([fk_id_conceptos])
REFERENCES [dbo].[prc_conceptos] ([id_conceptos])
GO
ALTER TABLE [dbo].[prc_pagos] CHECK CONSTRAINT [FK_prc_pagos_prc_conceptos]
GO
ALTER TABLE [dbo].[prc_pagos]  WITH CHECK ADD  CONSTRAINT [FK_prc_pagos_prc_tipopagos] FOREIGN KEY([fk_id_tipopagos])
REFERENCES [dbo].[prc_tipopagos] ([id_tipopagos])
GO
ALTER TABLE [dbo].[prc_pagos] CHECK CONSTRAINT [FK_prc_pagos_prc_tipopagos]
GO
ALTER TABLE [dbo].[prc_pagos]  WITH CHECK ADD  CONSTRAINT [FK_prc_pagos_prc_usuarios] FOREIGN KEY([fk_id_usuarios])
REFERENCES [dbo].[prc_usuarios] ([id_usuarios])
GO
ALTER TABLE [dbo].[prc_pagos] CHECK CONSTRAINT [FK_prc_pagos_prc_usuarios]
GO
ALTER TABLE [dbo].[prc_recargos]  WITH CHECK ADD  CONSTRAINT [fk_id_date_pagos] FOREIGN KEY([fk_id_date_pagos])
REFERENCES [dbo].[prc_date_pagos] ([id_date_pagos])
GO
ALTER TABLE [dbo].[prc_recargos] CHECK CONSTRAINT [fk_id_date_pagos]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 96
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 274
               Bottom = 126
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 510
               Bottom = 126
               Right = 708
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'prc_view_date_contratos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'prc_view_date_contratos'
GO
USE [master]
GO
ALTER DATABASE [dbpagoscontratos] SET  READ_WRITE 
GO
