
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/22/2024 22:45:14
-- Generated from EDMX file: C:\Users\Pablo\Desktop\ProyectoFinalProgaI\HotelProyectoFinal\HotelProyectoFinal\Models\MHotel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBHOTEL];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ASIGNACION_HABITACION]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ASIGNACION] DROP CONSTRAINT [FK_ASIGNACION_HABITACION];
GO
IF OBJECT_ID(N'[dbo].[FK_ASIGNACION_REGISTRO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ASIGNACION] DROP CONSTRAINT [FK_ASIGNACION_REGISTRO];
GO
IF OBJECT_ID(N'[dbo].[FK_HABITACION_TIPO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HABITACION] DROP CONSTRAINT [FK_HABITACION_TIPO];
GO
IF OBJECT_ID(N'[dbo].[FK_REGISTRO_HUESPED]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[REGISTRO] DROP CONSTRAINT [FK_REGISTRO_HUESPED];
GO
IF OBJECT_ID(N'[dbo].[FK_SERVICIO_TipoServicio]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SERVICIO] DROP CONSTRAINT [FK_SERVICIO_TipoServicio];
GO
IF OBJECT_ID(N'[dbo].[FK_TRANSACCION_ASIGNACION]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACCION] DROP CONSTRAINT [FK_TRANSACCION_ASIGNACION];
GO
IF OBJECT_ID(N'[dbo].[FK_TRANSACCION_DETALLE_SERVICIO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACCION_DETALLE] DROP CONSTRAINT [FK_TRANSACCION_DETALLE_SERVICIO];
GO
IF OBJECT_ID(N'[dbo].[FK_TRANSACCION_DETALLE_TIPO_DOCUMENTO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACCION_DETALLE] DROP CONSTRAINT [FK_TRANSACCION_DETALLE_TIPO_DOCUMENTO];
GO
IF OBJECT_ID(N'[dbo].[FK_TRANSACCION_TRANSACCION_DETALLE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRANSACCION] DROP CONSTRAINT [FK_TRANSACCION_TRANSACCION_DETALLE];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ASIGNACION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ASIGNACION];
GO
IF OBJECT_ID(N'[dbo].[HABITACION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HABITACION];
GO
IF OBJECT_ID(N'[dbo].[HUESPED]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HUESPED];
GO
IF OBJECT_ID(N'[dbo].[REGISTRO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[REGISTRO];
GO
IF OBJECT_ID(N'[dbo].[SERVICIO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SERVICIO];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TIPO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TIPO];
GO
IF OBJECT_ID(N'[dbo].[TIPO_DOCUMENTO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TIPO_DOCUMENTO];
GO
IF OBJECT_ID(N'[dbo].[TipoServicio]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoServicio];
GO
IF OBJECT_ID(N'[dbo].[TRANSACCION]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TRANSACCION];
GO
IF OBJECT_ID(N'[dbo].[TRANSACCION_DETALLE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TRANSACCION_DETALLE];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ASIGNACIONs'
CREATE TABLE [dbo].[ASIGNACIONs] (
    [IdRegistro] int  NOT NULL,
    [IdHuesped] nvarchar(50)  NOT NULL,
    [IdHabitacion] int  NOT NULL
);
GO

-- Creating table 'HABITACIONs'
CREATE TABLE [dbo].[HABITACIONs] (
    [IdHabitacion] int IDENTITY(1,1) NOT NULL,
    [IdTipo] int  NOT NULL,
    [Nivel] nvarchar(50)  NOT NULL,
    [Comentario] nvarchar(50)  NULL,
    [Precio] decimal(10,2)  NOT NULL,
    [Disponible] nchar(1)  NULL
);
GO

-- Creating table 'HUESPEDs'
CREATE TABLE [dbo].[HUESPEDs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DPI] nvarchar(50)  NOT NULL,
    [Nombres] nvarchar(50)  NOT NULL,
    [Apellidos] nvarchar(50)  NOT NULL,
    [Direccion] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'REGISTROes'
CREATE TABLE [dbo].[REGISTROes] (
    [IdRegistro] int IDENTITY(1,1) NOT NULL,
    [IdHuesped] nvarchar(50)  NOT NULL,
    [FechaHoraReserva] datetime  NULL,
    [FechaHoraIngreso] datetime  NULL,
    [FechaHoraFinRegistro] datetime  NULL,
    [TotalPago] varchar(50)  NULL,
    [ReservaActiva] nchar(1)  NULL,
    [FechaReservaInicio] datetime  NULL,
    [FechaReservaFinal] datetime  NULL
);
GO

-- Creating table 'SERVICIOs'
CREATE TABLE [dbo].[SERVICIOs] (
    [IdServicio] int IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(50)  NULL,
    [Precio] decimal(10,2)  NULL,
    [IdTipoServicio] int  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TIPOes'
CREATE TABLE [dbo].[TIPOes] (
    [IdTipo] int IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'TIPO_DOCUMENTO'
CREATE TABLE [dbo].[TIPO_DOCUMENTO] (
    [IdTipoDoc] int IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(50)  NULL,
    [Factor] nchar(1)  NULL
);
GO

-- Creating table 'TipoServicios'
CREATE TABLE [dbo].[TipoServicios] (
    [IdTipoServicio] int IDENTITY(1,1) NOT NULL,
    [Descripcion] nvarchar(50)  NULL
);
GO

-- Creating table 'TRANSACCIONs'
CREATE TABLE [dbo].[TRANSACCIONs] (
    [IdRegistro] int  NOT NULL,
    [IdHuesped] nvarchar(50)  NOT NULL,
    [IdHabitacion] int  NULL,
    [IdTipoDoc] int  NULL,
    [Fecha] datetime  NULL,
    [Total] decimal(10,2)  NULL,
    [IdTransaccion] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'TRANSACCION_DETALLE'
CREATE TABLE [dbo].[TRANSACCION_DETALLE] (
    [Documento] int  NOT NULL,
    [IdServicio] int  NOT NULL,
    [IDTipoDoc] int  NOT NULL,
    [Cantidad] int  NULL,
    [Total] decimal(10,2)  NULL,
    [IdHuesped] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdRegistro] in table 'ASIGNACIONs'
ALTER TABLE [dbo].[ASIGNACIONs]
ADD CONSTRAINT [PK_ASIGNACIONs]
    PRIMARY KEY CLUSTERED ([IdRegistro] ASC);
GO

-- Creating primary key on [IdHabitacion] in table 'HABITACIONs'
ALTER TABLE [dbo].[HABITACIONs]
ADD CONSTRAINT [PK_HABITACIONs]
    PRIMARY KEY CLUSTERED ([IdHabitacion] ASC);
GO

-- Creating primary key on [DPI] in table 'HUESPEDs'
ALTER TABLE [dbo].[HUESPEDs]
ADD CONSTRAINT [PK_HUESPEDs]
    PRIMARY KEY CLUSTERED ([DPI] ASC);
GO

-- Creating primary key on [IdRegistro] in table 'REGISTROes'
ALTER TABLE [dbo].[REGISTROes]
ADD CONSTRAINT [PK_REGISTROes]
    PRIMARY KEY CLUSTERED ([IdRegistro] ASC);
GO

-- Creating primary key on [IdServicio] in table 'SERVICIOs'
ALTER TABLE [dbo].[SERVICIOs]
ADD CONSTRAINT [PK_SERVICIOs]
    PRIMARY KEY CLUSTERED ([IdServicio] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [IdTipo] in table 'TIPOes'
ALTER TABLE [dbo].[TIPOes]
ADD CONSTRAINT [PK_TIPOes]
    PRIMARY KEY CLUSTERED ([IdTipo] ASC);
GO

-- Creating primary key on [IdTipoDoc] in table 'TIPO_DOCUMENTO'
ALTER TABLE [dbo].[TIPO_DOCUMENTO]
ADD CONSTRAINT [PK_TIPO_DOCUMENTO]
    PRIMARY KEY CLUSTERED ([IdTipoDoc] ASC);
GO

-- Creating primary key on [IdTipoServicio] in table 'TipoServicios'
ALTER TABLE [dbo].[TipoServicios]
ADD CONSTRAINT [PK_TipoServicios]
    PRIMARY KEY CLUSTERED ([IdTipoServicio] ASC);
GO

-- Creating primary key on [IdTransaccion] in table 'TRANSACCIONs'
ALTER TABLE [dbo].[TRANSACCIONs]
ADD CONSTRAINT [PK_TRANSACCIONs]
    PRIMARY KEY CLUSTERED ([IdTransaccion] ASC);
GO

-- Creating primary key on [IdHuesped] in table 'TRANSACCION_DETALLE'
ALTER TABLE [dbo].[TRANSACCION_DETALLE]
ADD CONSTRAINT [PK_TRANSACCION_DETALLE]
    PRIMARY KEY CLUSTERED ([IdHuesped] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdHabitacion] in table 'ASIGNACIONs'
ALTER TABLE [dbo].[ASIGNACIONs]
ADD CONSTRAINT [FK_ASIGNACION_HABITACION]
    FOREIGN KEY ([IdHabitacion])
    REFERENCES [dbo].[HABITACIONs]
        ([IdHabitacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ASIGNACION_HABITACION'
CREATE INDEX [IX_FK_ASIGNACION_HABITACION]
ON [dbo].[ASIGNACIONs]
    ([IdHabitacion]);
GO

-- Creating foreign key on [IdRegistro] in table 'ASIGNACIONs'
ALTER TABLE [dbo].[ASIGNACIONs]
ADD CONSTRAINT [FK_ASIGNACION_REGISTRO]
    FOREIGN KEY ([IdRegistro])
    REFERENCES [dbo].[REGISTROes]
        ([IdRegistro])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdRegistro] in table 'TRANSACCIONs'
ALTER TABLE [dbo].[TRANSACCIONs]
ADD CONSTRAINT [FK_TRANSACCION_ASIGNACION]
    FOREIGN KEY ([IdRegistro])
    REFERENCES [dbo].[ASIGNACIONs]
        ([IdRegistro])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TRANSACCION_ASIGNACION'
CREATE INDEX [IX_FK_TRANSACCION_ASIGNACION]
ON [dbo].[TRANSACCIONs]
    ([IdRegistro]);
GO

-- Creating foreign key on [IdTipo] in table 'HABITACIONs'
ALTER TABLE [dbo].[HABITACIONs]
ADD CONSTRAINT [FK_HABITACION_TIPO]
    FOREIGN KEY ([IdTipo])
    REFERENCES [dbo].[TIPOes]
        ([IdTipo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HABITACION_TIPO'
CREATE INDEX [IX_FK_HABITACION_TIPO]
ON [dbo].[HABITACIONs]
    ([IdTipo]);
GO

-- Creating foreign key on [IdHuesped] in table 'REGISTROes'
ALTER TABLE [dbo].[REGISTROes]
ADD CONSTRAINT [FK_REGISTRO_HUESPED]
    FOREIGN KEY ([IdHuesped])
    REFERENCES [dbo].[HUESPEDs]
        ([DPI])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_REGISTRO_HUESPED'
CREATE INDEX [IX_FK_REGISTRO_HUESPED]
ON [dbo].[REGISTROes]
    ([IdHuesped]);
GO

-- Creating foreign key on [IdTipoServicio] in table 'SERVICIOs'
ALTER TABLE [dbo].[SERVICIOs]
ADD CONSTRAINT [FK_SERVICIO_TipoServicio]
    FOREIGN KEY ([IdTipoServicio])
    REFERENCES [dbo].[TipoServicios]
        ([IdTipoServicio])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SERVICIO_TipoServicio'
CREATE INDEX [IX_FK_SERVICIO_TipoServicio]
ON [dbo].[SERVICIOs]
    ([IdTipoServicio]);
GO

-- Creating foreign key on [IdServicio] in table 'TRANSACCION_DETALLE'
ALTER TABLE [dbo].[TRANSACCION_DETALLE]
ADD CONSTRAINT [FK_TRANSACCION_DETALLE_SERVICIO]
    FOREIGN KEY ([IdServicio])
    REFERENCES [dbo].[SERVICIOs]
        ([IdServicio])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TRANSACCION_DETALLE_SERVICIO'
CREATE INDEX [IX_FK_TRANSACCION_DETALLE_SERVICIO]
ON [dbo].[TRANSACCION_DETALLE]
    ([IdServicio]);
GO

-- Creating foreign key on [IDTipoDoc] in table 'TRANSACCION_DETALLE'
ALTER TABLE [dbo].[TRANSACCION_DETALLE]
ADD CONSTRAINT [FK_TRANSACCION_DETALLE_TIPO_DOCUMENTO]
    FOREIGN KEY ([IDTipoDoc])
    REFERENCES [dbo].[TIPO_DOCUMENTO]
        ([IdTipoDoc])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TRANSACCION_DETALLE_TIPO_DOCUMENTO'
CREATE INDEX [IX_FK_TRANSACCION_DETALLE_TIPO_DOCUMENTO]
ON [dbo].[TRANSACCION_DETALLE]
    ([IDTipoDoc]);
GO

-- Creating foreign key on [IdHuesped] in table 'TRANSACCIONs'
ALTER TABLE [dbo].[TRANSACCIONs]
ADD CONSTRAINT [FK_TRANSACCION_TRANSACCION_DETALLE]
    FOREIGN KEY ([IdHuesped])
    REFERENCES [dbo].[TRANSACCION_DETALLE]
        ([IdHuesped])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TRANSACCION_TRANSACCION_DETALLE'
CREATE INDEX [IX_FK_TRANSACCION_TRANSACCION_DETALLE]
ON [dbo].[TRANSACCIONs]
    ([IdHuesped]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------