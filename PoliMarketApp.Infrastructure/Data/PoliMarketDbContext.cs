using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PoliMarketApp.Domain.Entities;

namespace PoliMarketApp.Infrastructure.Data;

public partial class PoliMarketDbContext : DbContext
{
    public PoliMarketDbContext()
    {
    }

    public PoliMarketDbContext(DbContextOptions<PoliMarketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CompraProveedor> ComprasProveedors { get; set; }

    public virtual DbSet<DetalleCompraProveedor> DetalleComprasProveedors { get; set; }

    public virtual DbSet<DetallePedidoVenta> DetallePedidosVenta { get; set; }

    public virtual DbSet<EstadoCompraProveedor> EstadosCompraProveedors { get; set; }

    public virtual DbSet<EstadoOrdenEntrega> EstadosOrdenEntregas { get; set; }

    public virtual DbSet<EstadoPedidoVenta> EstadosPedidoVenta { get; set; }

    public virtual DbSet<MovimientoBodega> MovimientosBodegas { get; set; }

    public virtual DbSet<OrdenEntrega> OrdenesEntregas { get; set; }

    public virtual DbSet<PedidoVenta> PedidosVenta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedores { get; set; }

    public virtual DbSet<Vendedor> Vendedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(e => e.Documento, "UX_Clientes_Documento").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Clientes_Activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CompraProveedor>(entity =>
        {
            entity.HasKey(e => e.CompraProveedorId);

            entity.ToTable("CompraProveedor");

            entity.HasIndex(e => e.EstadoCompraProveedorId, "IX_ComprasProveedor_Estado");

            entity.HasIndex(e => e.ProveedorId, "IX_ComprasProveedor_Proveedor");

            entity.Property(e => e.EstadoCompraProveedorId).HasDefaultValue(1, "DF_ComprasProveedor_EstadoId");
            entity.Property(e => e.FechaCompra).HasDefaultValueSql("(sysdatetime())", "DF_ComprasProveedor_FechaCompra");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.EstadoCompraProveedor).WithMany(p => p.CompraProveedores)
                .HasForeignKey(d => d.EstadoCompraProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComprasProveedor_EstadosCompraProveedor");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.ComprasProveedores)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComprasProveedor_Proveedores");
        });

        modelBuilder.Entity<DetalleCompraProveedor>(entity =>
        {
            entity.HasKey(e => e.DetalleCompraProveedorId);

            entity.ToTable("DetalleCompraProveedor");

            entity.HasIndex(e => e.CompraProveedorId, "IX_DetalleComprasProveedor_Compra");

            entity.HasIndex(e => e.ProductoId, "IX_DetalleComprasProveedor_Producto");

            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CompraProveedor).WithMany(p => p.DetalleComprasProveedores)
                .HasForeignKey(d => d.CompraProveedorId)
                .HasConstraintName("FK_DetalleComprasProveedor_ComprasProveedor");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetalleComprasProveedors)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleComprasProveedor_Productos");
        });

        modelBuilder.Entity<DetallePedidoVenta>(entity =>
        {
            entity.HasKey(e => e.DetallePedidoVentaId);

            entity.HasIndex(e => e.PedidoVentaId, "IX_DetallePedidosVenta_Pedido");

            entity.HasIndex(e => e.ProductoId, "IX_DetallePedidosVenta_Producto");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.PedidoVenta).WithMany(p => p.DetallePedidosVenta)
                .HasForeignKey(d => d.PedidoVentaId)
                .HasConstraintName("FK_DetallePedidosVenta_PedidosVenta");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallePedidosVenta)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedidosVenta_Productos");
        });

        modelBuilder.Entity<EstadoCompraProveedor>(entity =>
        {
            entity.HasKey(e => e.EstadoCompraProveedorId);

            entity.ToTable("EstadoCompraProveedor");

            entity.HasIndex(e => e.Codigo, "UX_EstadosCompraProveedor_Codigo").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EsActivo).HasDefaultValue(true, "DF_EstadosCompraProveedor_Activo");
        });

        modelBuilder.Entity<EstadoOrdenEntrega>(entity =>
        {
            entity.HasKey(e => e.EstadoOrdenEntregaId);

            entity.ToTable("EstadoOrdenEntrega");

            entity.HasIndex(e => e.Codigo, "UX_EstadosOrdenEntrega_Codigo").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EsActivo).HasDefaultValue(true, "DF_EstadosOrdenEntrega_Activo");
        });

        modelBuilder.Entity<EstadoPedidoVenta>(entity =>
        {
            entity.HasKey(e => e.EstadoPedidoVentaId);

            entity.HasIndex(e => e.Codigo, "UX_EstadosPedidoVenta_Codigo").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EsActivo).HasDefaultValue(true, "DF_EstadosPedidoVenta_Activo");
        });

        modelBuilder.Entity<MovimientoBodega>(entity =>
        {
            entity.HasKey(e => e.MovimientoBodegaId);

            entity.ToTable("MovimientoBodega");

            entity.HasIndex(e => e.ProductoId, "IX_MovimientosBodega_Producto");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysdatetime())", "DF_MovimientosBodega_Fecha");
            entity.Property(e => e.Origen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.MovimientosBodegas)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientosBodega_Productos");
        });

        modelBuilder.Entity<OrdenEntrega>(entity =>
        {
            entity.HasKey(e => e.OrdenEntregaId);

            entity.ToTable("OrdenEntrega");

            entity.HasIndex(e => e.EstadoOrdenEntregaId, "IX_OrdenesEntrega_Estado");

            entity.HasIndex(e => e.PedidoVentaId, "IX_OrdenesEntrega_Pedido");

            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EstadoOrdenEntregaId).HasDefaultValue(1, "DF_OrdenesEntrega_EstadoId");

            entity.HasOne(d => d.EstadoOrdenEntrega).WithMany(p => p.OrdenesEntregas)
                .HasForeignKey(d => d.EstadoOrdenEntregaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenesEntrega_EstadosOrdenEntrega");

            entity.HasOne(d => d.PedidoVenta).WithMany(p => p.OrdenesEntregas)
                .HasForeignKey(d => d.PedidoVentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenesEntrega_PedidosVenta");
        });

        modelBuilder.Entity<PedidoVenta>(entity =>
        {
            entity.HasKey(e => e.PedidoVentaId);

            entity.HasIndex(e => e.ClienteId, "IX_PedidosVenta_Cliente");

            entity.HasIndex(e => e.EstadoPedidoVentaId, "IX_PedidosVenta_Estado");

            entity.Property(e => e.EstadoPedidoVentaId).HasDefaultValue(1, "DF_PedidosVenta_EstadoId");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())", "DF_PedidosVenta_FechaCreacion");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.PedidosVenta)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidosVenta_Clientes");

            entity.HasOne(d => d.EstadoPedidoVenta).WithMany(p => p.PedidosVenta)
                .HasForeignKey(d => d.EstadoPedidoVentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidosVenta_EstadosPedidoVenta");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.PedidosVenta)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidosVenta_Vendedores");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasIndex(e => e.CodigoBarras, "UX_Productos_CodigoBarras")
                .IsUnique()
                .HasFilter("([CodigoBarras] IS NOT NULL)");

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Productos_Activo");
            entity.Property(e => e.CodigoBarras)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.ProveedorId);

            entity.HasIndex(e => e.Nit, "UX_Proveedores_Nit").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Proveedores_Activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.VendedorId);

            entity.HasIndex(e => e.Documento, "UX_Vendedores_Documento").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Vendedores_Activo");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasDefaultValueSql("(sysdatetime())", "DF_Vendedores_FechaIngreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
