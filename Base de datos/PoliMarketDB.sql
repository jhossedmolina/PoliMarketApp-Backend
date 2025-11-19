------------------------------------------------------------
-- 1. Crear Base de Datos
------------------------------------------------------------
IF DB_ID('PoliMarketDB') IS NULL
BEGIN
    CREATE DATABASE PoliMarketDB;
END
GO

USE PoliMarketDB;
GO

------------------------------------------------------------
-- 2. Limpieza previa (DROP en orden por dependencias)
------------------------------------------------------------
IF OBJECT_ID('dbo.OrdenesEntrega', 'U') IS NOT NULL DROP TABLE dbo.OrdenesEntrega;
IF OBJECT_ID('dbo.MovimientosBodega', 'U') IS NOT NULL DROP TABLE dbo.MovimientosBodega;
IF OBJECT_ID('dbo.DetalleComprasProveedor', 'U') IS NOT NULL DROP TABLE dbo.DetalleComprasProveedor;
IF OBJECT_ID('dbo.ComprasProveedor', 'U') IS NOT NULL DROP TABLE dbo.ComprasProveedor;
IF OBJECT_ID('dbo.DetallePedidosVenta', 'U') IS NOT NULL DROP TABLE dbo.DetallePedidosVenta;
IF OBJECT_ID('dbo.PedidosVenta', 'U') IS NOT NULL DROP TABLE dbo.PedidosVenta;

IF OBJECT_ID('dbo.EstadosOrdenEntrega', 'U') IS NOT NULL DROP TABLE dbo.EstadosOrdenEntrega;
IF OBJECT_ID('dbo.EstadosPedidoVenta', 'U') IS NOT NULL DROP TABLE dbo.EstadosPedidoVenta;
IF OBJECT_ID('dbo.EstadosCompraProveedor', 'U') IS NOT NULL DROP TABLE dbo.EstadosCompraProveedor;

IF OBJECT_ID('dbo.Productos', 'U') IS NOT NULL DROP TABLE dbo.Productos;
IF OBJECT_ID('dbo.Proveedores', 'U') IS NOT NULL DROP TABLE dbo.Proveedores;
IF OBJECT_ID('dbo.Clientes', 'U') IS NOT NULL DROP TABLE dbo.Clientes;
IF OBJECT_ID('dbo.Vendedores', 'U') IS NOT NULL DROP TABLE dbo.Vendedores;
GO

------------------------------------------------------------
-- 3. Tablas base
------------------------------------------------------------

-------------------------
-- 3.1 Vendedores (RRHH)
-------------------------
CREATE TABLE dbo.Vendedores
(
    VendedorId            INT IDENTITY(1,1)      NOT NULL CONSTRAINT PK_Vendedores PRIMARY KEY,
    Nombre                VARCHAR(100)           NOT NULL,
    Apellido              VARCHAR(100)           NOT NULL,
    Documento             VARCHAR(20)            NOT NULL,
    Email                 VARCHAR(150)           NOT NULL,
    Activo                BIT                    NOT NULL CONSTRAINT DF_Vendedores_Activo DEFAULT(1),
    AutorizadoParaOperar  BIT                    NOT NULL CONSTRAINT DF_Vendedores_Autorizado DEFAULT(0),
    FechaIngreso          DATETIME2              NOT NULL CONSTRAINT DF_Vendedores_FechaIngreso DEFAULT(SYSDATETIME())
);
GO

CREATE UNIQUE INDEX UX_Vendedores_Documento
    ON dbo.Vendedores (Documento);
GO

-------------------------
-- 3.2 Clientes (Ventas)
-------------------------
CREATE TABLE dbo.Clientes
(
    ClienteId     INT IDENTITY(1,1)      NOT NULL CONSTRAINT PK_Clientes PRIMARY KEY,
    Nombre        VARCHAR(150)           NOT NULL,
    Documento     VARCHAR(20)            NOT NULL,
    Direccion     VARCHAR(250)           NULL,
    Telefono      VARCHAR(50)            NULL,
    Email         VARCHAR(150)           NULL,
    Activo        BIT                    NOT NULL CONSTRAINT DF_Clientes_Activo DEFAULT(1)
);
GO

CREATE UNIQUE INDEX UX_Clientes_Documento
    ON dbo.Clientes (Documento);
GO

-------------------------
-- 3.3 Productos (Bodega/Ventas)
-------------------------
CREATE TABLE dbo.Productos
(
    ProductoId      INT IDENTITY(1,1)       NOT NULL CONSTRAINT PK_Productos PRIMARY KEY,
    Nombre          VARCHAR(150)            NOT NULL,
    Descripcion     VARCHAR(500)            NULL,
    PrecioUnitario  DECIMAL(18,2)           NOT NULL CONSTRAINT DF_Productos_Precio DEFAULT(0),
    CodigoBarras    VARCHAR(50)             NULL,
    StockActual     INT                     NOT NULL CONSTRAINT DF_Productos_StockActual DEFAULT(0),
    StockMinimo     INT                     NOT NULL CONSTRAINT DF_Productos_StockMinimo DEFAULT(0),
    Activo          BIT                     NOT NULL CONSTRAINT DF_Productos_Activo DEFAULT(1)
);
GO

CREATE UNIQUE INDEX UX_Productos_CodigoBarras
    ON dbo.Productos (CodigoBarras)
    WHERE CodigoBarras IS NOT NULL;
GO

-------------------------
-- 3.4 Proveedores
-------------------------
CREATE TABLE dbo.Proveedores
(
    ProveedorId  INT IDENTITY(1,1)      NOT NULL CONSTRAINT PK_Proveedores PRIMARY KEY,
    Nombre       VARCHAR(200)           NOT NULL,
    Nit          VARCHAR(20)            NOT NULL,
    Direccion    VARCHAR(250)           NULL,
    Telefono     VARCHAR(50)            NULL,
    Email        VARCHAR(150)           NULL,
    Activo       BIT                    NOT NULL CONSTRAINT DF_Proveedores_Activo DEFAULT(1)
);
GO

CREATE UNIQUE INDEX UX_Proveedores_Nit
    ON dbo.Proveedores (Nit);
GO

------------------------------------------------------------
-- 4. Catálogos de estados
------------------------------------------------------------

-------------------------
-- 4.1 EstadosCompraProveedor
-------------------------
CREATE TABLE dbo.EstadosCompraProveedor
(
    EstadoCompraProveedorId INT IDENTITY(1,1) NOT NULL
        CONSTRAINT PK_EstadosCompraProveedor PRIMARY KEY,
    Codigo       VARCHAR(30)  NOT NULL,   -- Ej: 'Registrada', 'Recibida'
    Descripcion  VARCHAR(100) NOT NULL,
    EsActivo     BIT          NOT NULL CONSTRAINT DF_EstadosCompraProveedor_Activo DEFAULT(1)
);
GO

CREATE UNIQUE INDEX UX_EstadosCompraProveedor_Codigo
    ON dbo.EstadosCompraProveedor (Codigo);
GO

INSERT INTO dbo.EstadosCompraProveedor (Codigo, Descripcion)
VALUES 
    ('Registrada', 'Compra registrada en el sistema'),
    ('Recibida',   'Compra recibida en bodega'),
    ('Cancelada',  'Compra anulada/cancelada');
GO

-------------------------
-- 4.2 EstadosPedidoVenta
-------------------------
CREATE TABLE dbo.EstadosPedidoVenta
(
    EstadoPedidoVentaId INT IDENTITY(1,1) NOT NULL
        CONSTRAINT PK_EstadosPedidoVenta PRIMARY KEY,
    Codigo       VARCHAR(30)  NOT NULL,   -- Ej: 'Registrado', 'Confirmado', 'Facturado', 'Cancelado'
    Descripcion  VARCHAR(100) NOT NULL,
    EsActivo     BIT          NOT NULL CONSTRAINT DF_EstadosPedidoVenta_Activo DEFAULT(1)
);
GO

CREATE UNIQUE INDEX UX_EstadosPedidoVenta_Codigo
    ON dbo.EstadosPedidoVenta (Codigo);
GO

INSERT INTO dbo.EstadosPedidoVenta (Codigo, Descripcion)
VALUES 
    ('Registrado', 'Pedido registrado en el sistema'),
    ('Confirmado', 'Pedido confirmado para preparación'),
    ('Facturado',  'Pedido facturado al cliente'),
    ('Cancelado',  'Pedido cancelado');
GO

-------------------------
-- 4.3 EstadosOrdenEntrega
-------------------------
CREATE TABLE dbo.EstadosOrdenEntrega
(
    EstadoOrdenEntregaId INT IDENTITY(1,1) NOT NULL
        CONSTRAINT PK_EstadosOrdenEntrega PRIMARY KEY,
    Codigo       VARCHAR(30)  NOT NULL,   -- Ej: 'Pendiente', 'EnRuta', 'Entregado', 'Cancelado'
    Descripcion  VARCHAR(100) NOT NULL,
    EsActivo     BIT          NOT NULL CONSTRAINT DF_EstadosOrdenEntrega_Activo DEFAULT(1)
);
GO

CREATE UNIQUE INDEX UX_EstadosOrdenEntrega_Codigo
    ON dbo.EstadosOrdenEntrega (Codigo);
GO

INSERT INTO dbo.EstadosOrdenEntrega (Codigo, Descripcion)
VALUES 
    ('Pendiente', 'Orden pendiente de despacho'),
    ('EnRuta',    'Orden en proceso de entrega'),
    ('Entregado', 'Orden entregada al cliente'),
    ('Cancelado', 'Orden de entrega cancelada');
GO

------------------------------------------------------------
-- 5. Ventas
------------------------------------------------------------

-------------------------
-- 5.1 PedidosVenta
-------------------------
CREATE TABLE dbo.PedidosVenta
(
    PedidoVentaId       INT IDENTITY(1,1)       NOT NULL CONSTRAINT PK_PedidosVenta PRIMARY KEY,
    VendedorId          INT                     NOT NULL,
    ClienteId           INT                     NOT NULL,
    EstadoPedidoVentaId INT                     NOT NULL CONSTRAINT DF_PedidosVenta_EstadoId DEFAULT(1), -- 1 = Registrado
    FechaCreacion       DATETIME2               NOT NULL CONSTRAINT DF_PedidosVenta_FechaCreacion DEFAULT(SYSDATETIME()),
    Total               DECIMAL(18,2)           NOT NULL CONSTRAINT DF_PedidosVenta_Total DEFAULT(0)
);
GO

ALTER TABLE dbo.PedidosVenta
ADD CONSTRAINT FK_PedidosVenta_Vendedores
    FOREIGN KEY (VendedorId) REFERENCES dbo.Vendedores (VendedorId);

ALTER TABLE dbo.PedidosVenta
ADD CONSTRAINT FK_PedidosVenta_Clientes
    FOREIGN KEY (ClienteId) REFERENCES dbo.Clientes (ClienteId);

ALTER TABLE dbo.PedidosVenta
ADD CONSTRAINT FK_PedidosVenta_EstadosPedidoVenta
    FOREIGN KEY (EstadoPedidoVentaId) REFERENCES dbo.EstadosPedidoVenta (EstadoPedidoVentaId);
GO

CREATE INDEX IX_PedidosVenta_Cliente
    ON dbo.PedidosVenta (ClienteId);

CREATE INDEX IX_PedidosVenta_Estado
    ON dbo.PedidosVenta (EstadoPedidoVentaId);
GO

-------------------------
-- 5.2 DetallePedidosVenta
-------------------------
CREATE TABLE dbo.DetallePedidosVenta
(
    DetallePedidoVentaId INT IDENTITY(1,1)   NOT NULL CONSTRAINT PK_DetallePedidosVenta PRIMARY KEY,
    PedidoVentaId        INT                 NOT NULL,
    ProductoId           INT                 NOT NULL,
    Cantidad             INT                 NOT NULL,
    PrecioUnitario       DECIMAL(18,2)       NOT NULL,
    Subtotal             DECIMAL(18,2)       NOT NULL
);
GO

ALTER TABLE dbo.DetallePedidosVenta
ADD CONSTRAINT FK_DetallePedidosVenta_PedidosVenta
    FOREIGN KEY (PedidoVentaId) REFERENCES dbo.PedidosVenta (PedidoVentaId)
    ON DELETE CASCADE;

ALTER TABLE dbo.DetallePedidosVenta
ADD CONSTRAINT FK_DetallePedidosVenta_Productos
    FOREIGN KEY (ProductoId) REFERENCES dbo.Productos (ProductoId);
GO

CREATE INDEX IX_DetallePedidosVenta_Pedido
    ON dbo.DetallePedidosVenta (PedidoVentaId);

CREATE INDEX IX_DetallePedidosVenta_Producto
    ON dbo.DetallePedidosVenta (ProductoId);
GO

------------------------------------------------------------
-- 6. Compras a Proveedores
------------------------------------------------------------

-------------------------
-- 6.1 ComprasProveedor
-------------------------
CREATE TABLE dbo.ComprasProveedor
(
    CompraProveedorId       INT IDENTITY(1,1)    NOT NULL 
        CONSTRAINT PK_ComprasProveedor PRIMARY KEY,
    ProveedorId             INT                  NOT NULL,
    EstadoCompraProveedorId INT                  NOT NULL 
        CONSTRAINT DF_ComprasProveedor_EstadoId DEFAULT(1), -- 1 = Registrada
    FechaCompra             DATETIME2            NOT NULL 
        CONSTRAINT DF_ComprasProveedor_FechaCompra DEFAULT(SYSDATETIME()),
    Total                   DECIMAL(18,2)        NOT NULL 
        CONSTRAINT DF_ComprasProveedor_Total DEFAULT(0)
);
GO

ALTER TABLE dbo.ComprasProveedor
ADD CONSTRAINT FK_ComprasProveedor_Proveedores
    FOREIGN KEY (ProveedorId) REFERENCES dbo.Proveedores (ProveedorId);
GO

ALTER TABLE dbo.ComprasProveedor
ADD CONSTRAINT FK_ComprasProveedor_EstadosCompraProveedor
    FOREIGN KEY (EstadoCompraProveedorId) REFERENCES dbo.EstadosCompraProveedor (EstadoCompraProveedorId);
GO

CREATE INDEX IX_ComprasProveedor_Proveedor
    ON dbo.ComprasProveedor (ProveedorId);

CREATE INDEX IX_ComprasProveedor_Estado
    ON dbo.ComprasProveedor (EstadoCompraProveedorId);
GO

-------------------------
-- 6.2 DetalleComprasProveedor
-------------------------
CREATE TABLE dbo.DetalleComprasProveedor
(
    DetalleCompraProveedorId INT IDENTITY(1,1)  NOT NULL CONSTRAINT PK_DetalleComprasProveedor PRIMARY KEY,
    CompraProveedorId        INT                NOT NULL,
    ProductoId               INT                NOT NULL,
    Cantidad                 INT                NOT NULL,
    CostoUnitario            DECIMAL(18,2)      NOT NULL,
    Subtotal                 DECIMAL(18,2)      NOT NULL
);
GO

ALTER TABLE dbo.DetalleComprasProveedor
ADD CONSTRAINT FK_DetalleComprasProveedor_ComprasProveedor
    FOREIGN KEY (CompraProveedorId) REFERENCES dbo.ComprasProveedor (CompraProveedorId)
    ON DELETE CASCADE;

ALTER TABLE dbo.DetalleComprasProveedor
ADD CONSTRAINT FK_DetalleComprasProveedor_Productos
    FOREIGN KEY (ProductoId) REFERENCES dbo.Productos (ProductoId);
GO

CREATE INDEX IX_DetalleComprasProveedor_Compra
    ON dbo.DetalleComprasProveedor (CompraProveedorId);

CREATE INDEX IX_DetalleComprasProveedor_Producto
    ON dbo.DetalleComprasProveedor (ProductoId);
GO

------------------------------------------------------------
-- 7. Bodega - Movimientos
------------------------------------------------------------

CREATE TABLE dbo.MovimientosBodega
(
    MovimientoBodegaId INT IDENTITY(1,1)      NOT NULL CONSTRAINT PK_MovimientosBodega PRIMARY KEY,
    ProductoId         INT                    NOT NULL,
    TipoMovimiento     VARCHAR(20)            NOT NULL, -- Entrada / Salida
    Cantidad           INT                    NOT NULL,
    Fecha              DATETIME2              NOT NULL CONSTRAINT DF_MovimientosBodega_Fecha DEFAULT(SYSDATETIME()),
    Origen             VARCHAR(50)            NULL,     -- CompraProveedor, Venta, AjusteInventario, etc.
    Referencia         INT                    NULL      -- Id del documento origen (PedidoVentaId, CompraProveedorId, etc.)
);
GO

ALTER TABLE dbo.MovimientosBodega
ADD CONSTRAINT FK_MovimientosBodega_Productos
    FOREIGN KEY (ProductoId) REFERENCES dbo.Productos (ProductoId);
GO

ALTER TABLE dbo.MovimientosBodega
ADD CONSTRAINT CK_MovimientosBodega_TipoMovimiento
    CHECK (TipoMovimiento IN ('Entrada', 'Salida'));
GO

CREATE INDEX IX_MovimientosBodega_Producto
    ON dbo.MovimientosBodega (ProductoId);
GO

------------------------------------------------------------
-- 8. Entregas
------------------------------------------------------------

CREATE TABLE dbo.OrdenesEntrega
(
    OrdenEntregaId      INT IDENTITY(1,1)     NOT NULL CONSTRAINT PK_OrdenesEntrega PRIMARY KEY,
    PedidoVentaId       INT                   NOT NULL,
    EstadoOrdenEntregaId INT                  NOT NULL CONSTRAINT DF_OrdenesEntrega_EstadoId DEFAULT(1), -- 1 = Pendiente
    FechaProgramada     DATETIME2             NOT NULL,
    FechaEntregaReal    DATETIME2             NULL,
    DireccionEntrega    VARCHAR(250)          NULL
);
GO

ALTER TABLE dbo.OrdenesEntrega
ADD CONSTRAINT FK_OrdenesEntrega_PedidosVenta
    FOREIGN KEY (PedidoVentaId) REFERENCES dbo.PedidosVenta (PedidoVentaId);
GO

ALTER TABLE dbo.OrdenesEntrega
ADD CONSTRAINT FK_OrdenesEntrega_EstadosOrdenEntrega
    FOREIGN KEY (EstadoOrdenEntregaId) REFERENCES dbo.EstadosOrdenEntrega (EstadoOrdenEntregaId);
GO

CREATE INDEX IX_OrdenesEntrega_Pedido
    ON dbo.OrdenesEntrega (PedidoVentaId);

CREATE INDEX IX_OrdenesEntrega_Estado
    ON dbo.OrdenesEntrega (EstadoOrdenEntregaId);
GO
