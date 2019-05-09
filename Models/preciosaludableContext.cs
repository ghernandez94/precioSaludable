using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace preciosaludable.Models
{
    public partial class preciosaludableContext : DbContext
    {
        public preciosaludableContext()
        {
        }

        public preciosaludableContext(DbContextOptions<preciosaludableContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoriaterapeutica> Categoriaterapeutica { get; set; }
        public virtual DbSet<Comuna> Comuna { get; set; }
        public virtual DbSet<Concentracion> Concentracion { get; set; }
        public virtual DbSet<Detallecategoriaterapeutica> Detallecategoriaterapeutica { get; set; }
        public virtual DbSet<Detalleprecio> Detalleprecio { get; set; }
        public virtual DbSet<Farmacia> Farmacia { get; set; }
        public virtual DbSet<Farmaco> Farmaco { get; set; }
        public virtual DbSet<Laboratorio> Laboratorio { get; set; }
        public virtual DbSet<Presentacion> Presentacion { get; set; }
        public virtual DbSet<Principioactivo> Principioactivo { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<Tipousuario> Tipousuario { get; set; }
        public virtual DbSet<Unidadmedida> Unidadmedida { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Categoriaterapeutica>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaTerapeutica)
                    .HasName("PK_categoriaprincipioactivo");

                entity.ToTable("categoriaterapeutica");

                entity.HasIndex(e => e.NombreCategoriaTerapeutica)
                    .HasName("UQ_categoriaterapeutica_nombreCategoriaTerapeutica")
                    .IsUnique();

                entity.Property(e => e.IdCategoriaTerapeutica).HasColumnName("idCategoriaTerapeutica");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreCategoriaTerapeutica)
                    .IsRequired()
                    .HasColumnName("nombreCategoriaTerapeutica")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Comuna>(entity =>
            {
                entity.HasKey(e => e.IdComuna)
                    .HasName("PK_comuna_idComuna");

                entity.ToTable("comuna");

                entity.Property(e => e.IdComuna)
                    .HasColumnName("idComuna")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreComuna)
                    .IsRequired()
                    .HasColumnName("nombreComuna")
                    .HasMaxLength(45);

                entity.Property(e => e.ProvinciaIdProvincia).HasColumnName("Provincia_idProvincia");

                entity.HasOne(d => d.ProvinciaIdProvinciaNavigation)
                    .WithMany(p => p.Comuna)
                    .HasForeignKey(d => d.ProvinciaIdProvincia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comuna_provincia");
            });

            modelBuilder.Entity<Concentracion>(entity =>
            {
                entity.HasKey(e => e.IdConcentracion);

                entity.ToTable("concentracion");

                entity.Property(e => e.IdConcentracion).HasColumnName("idConcentracion");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FarmacoIdFarmaco).HasColumnName("Farmaco_idFarmaco");

                entity.Property(e => e.PrincipioActivoIdPrincipioActivo).HasColumnName("PrincipioActivo_idPrincipioActivo");

                entity.Property(e => e.UnidadMedidaIdUnidadMedida).HasColumnName("UnidadMedida_IdUnidadMedida");

                entity.HasOne(d => d.FarmacoIdFarmacoNavigation)
                    .WithMany(p => p.Concentracion)
                    .HasForeignKey(d => d.FarmacoIdFarmaco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_concentracion_farmaco");

                entity.HasOne(d => d.PrincipioActivoIdPrincipioActivoNavigation)
                    .WithMany(p => p.Concentracion)
                    .HasForeignKey(d => d.PrincipioActivoIdPrincipioActivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_concentracion_principioactivo");

                entity.HasOne(d => d.UnidadMedidaIdUnidadMedidaNavigation)
                    .WithMany(p => p.Concentracion)
                    .HasForeignKey(d => d.UnidadMedidaIdUnidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_concentracion_unidadmedida");
            });

            modelBuilder.Entity<Detallecategoriaterapeutica>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCategoria);

                entity.ToTable("detallecategoriaterapeutica");

                entity.HasIndex(e => new { e.CategoriaTerapeuticaIdCategoriaTerapeutica, e.PrincpioActivoIdPrincipioActivo })
                    .HasName("UQ_detallecategoriaterapeutica")
                    .IsUnique();

                entity.Property(e => e.IdDetalleCategoria).HasColumnName("idDetalleCategoria");

                entity.Property(e => e.CategoriaTerapeuticaIdCategoriaTerapeutica).HasColumnName("CategoriaTerapeutica_idCategoriaTerapeutica");

                entity.Property(e => e.PrincpioActivoIdPrincipioActivo).HasColumnName("PrincpioActivo_idPrincipioActivo");

                entity.HasOne(d => d.CategoriaTerapeuticaIdCategoriaTerapeuticaNavigation)
                    .WithMany(p => p.Detallecategoriaterapeutica)
                    .HasForeignKey(d => d.CategoriaTerapeuticaIdCategoriaTerapeutica)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detallecategoriaterapeutica_categoriaterapeutica");

                entity.HasOne(d => d.PrincpioActivoIdPrincipioActivoNavigation)
                    .WithMany(p => p.Detallecategoriaterapeutica)
                    .HasForeignKey(d => d.PrincpioActivoIdPrincipioActivo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detallecategoriaterapeutica_principioactivo");
            });

            modelBuilder.Entity<Detalleprecio>(entity =>
            {
                entity.HasKey(e => e.IdDetallePrecio);

                entity.ToTable("detalleprecio");

                entity.Property(e => e.IdDetallePrecio).HasColumnName("idDetallePrecio");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaHoraDetalle)
                    .HasColumnName("fechaHoraDetalle")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.PrecioFarmaco).HasColumnName("precioFarmaco");

                entity.Property(e => e.ProductoIdProducto).HasColumnName("Producto_idProducto");

                entity.Property(e => e.SucursalIdSucursal).HasColumnName("Sucursal_idSucursal");

                entity.Property(e => e.UsuarioIdUsuario).HasColumnName("Usuario_idUsuario");

                entity.HasOne(d => d.ProductoIdProductoNavigation)
                    .WithMany(p => p.Detalleprecio)
                    .HasForeignKey(d => d.ProductoIdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detalleprecio_producto");

                entity.HasOne(d => d.SucursalIdSucursalNavigation)
                    .WithMany(p => p.Detalleprecio)
                    .HasForeignKey(d => d.SucursalIdSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detalleprecio_sucursal");

                entity.HasOne(d => d.UsuarioIdUsuarioNavigation)
                    .WithMany(p => p.Detalleprecio)
                    .HasForeignKey(d => d.UsuarioIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detalleprecio_usuario");
            });

            modelBuilder.Entity<Farmacia>(entity =>
            {
                entity.HasKey(e => e.IdFarmacia)
                    .HasName("PK_farmacia_idFarmacia");

                entity.ToTable("farmacia");

                entity.HasIndex(e => e.Rutfarmacia)
                    .HasName("UQ_farmacia_RUTFarmacia")
                    .IsUnique();

                entity.Property(e => e.IdFarmacia).HasColumnName("idFarmacia");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreFarmacia)
                    .IsRequired()
                    .HasColumnName("nombreFarmacia")
                    .HasMaxLength(45);

                entity.Property(e => e.Rutfarmacia)
                    .IsRequired()
                    .HasColumnName("RUTFarmacia")
                    .HasMaxLength(10);

                entity.Property(e => e.TelefonoFarmacia)
                    .IsRequired()
                    .HasColumnName("telefonoFarmacia")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Farmaco>(entity =>
            {
                entity.HasKey(e => e.IdFarmaco)
                    .HasName("PK_farmaco_idFarmaco");

                entity.ToTable("farmaco");

                entity.Property(e => e.IdFarmaco).HasColumnName("idFarmaco");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PresentacionIdPresentacion).HasColumnName("Presentacion_idPresentacion");

                entity.HasOne(d => d.PresentacionIdPresentacionNavigation)
                    .WithMany(p => p.Farmaco)
                    .HasForeignKey(d => d.PresentacionIdPresentacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_farmaco_presentacion");
            });

            modelBuilder.Entity<Laboratorio>(entity =>
            {
                entity.HasKey(e => e.IdLaboratorio)
                    .HasName("PK_laboratorio_idLaboratorio");

                entity.ToTable("laboratorio");

                entity.HasIndex(e => e.Rutlaboratorio)
                    .HasName("UQ_laboratorio_RUTLaboratorio")
                    .IsUnique();

                entity.Property(e => e.IdLaboratorio).HasColumnName("idLaboratorio");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreLaboratorio)
                    .IsRequired()
                    .HasColumnName("nombreLaboratorio")
                    .HasMaxLength(45);

                entity.Property(e => e.Rutlaboratorio)
                    .IsRequired()
                    .HasColumnName("RUTLaboratorio")
                    .HasMaxLength(10);

                entity.Property(e => e.TelefonoLaboratorio)
                    .IsRequired()
                    .HasColumnName("telefonoLaboratorio")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Presentacion>(entity =>
            {
                entity.HasKey(e => e.IdPresentacion)
                    .HasName("PK_presentacion_idPresentacion");

                entity.ToTable("presentacion");

                entity.Property(e => e.IdPresentacion).HasColumnName("idPresentacion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombrePresentacion)
                    .IsRequired()
                    .HasColumnName("nombrePresentacion")
                    .HasMaxLength(45);

                entity.Property(e => e.UnidadMedidaIdUnidadMedida).HasColumnName("UnidadMedida_idUnidadMedida");

                entity.HasOne(d => d.UnidadMedidaIdUnidadMedidaNavigation)
                    .WithMany(p => p.Presentacion)
                    .HasForeignKey(d => d.UnidadMedidaIdUnidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_presentacion_unidadmedida");
            });

            modelBuilder.Entity<Principioactivo>(entity =>
            {
                entity.HasKey(e => e.IdPrincipioActivo);

                entity.ToTable("principioactivo");

                entity.HasIndex(e => e.NombrePrincipioActivo)
                    .HasName("UQ_principioactivo_nombrePrincipioActivo")
                    .IsUnique();

                entity.Property(e => e.IdPrincipioActivo).HasColumnName("idPrincipioActivo");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombrePrincipioActivo)
                    .IsRequired()
                    .HasColumnName("nombrePrincipioActivo")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("producto");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.CantidadPresentacion)
                    .HasColumnName("cantidadPresentacion")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FarmacoIdFarmaco).HasColumnName("Farmaco_idFarmaco");

                entity.Property(e => e.LaboratorioIdLaboratorio).HasColumnName("Laboratorio_idLaboratorio");

                entity.Property(e => e.NombreComercialProducto)
                    .IsRequired()
                    .HasColumnName("nombreComercialProducto")
                    .HasMaxLength(45);

                entity.Property(e => e.ProductoBioequivalente).HasColumnName("Producto_Bioequivalente");

                entity.HasOne(d => d.FarmacoIdFarmacoNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.FarmacoIdFarmaco)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_farmaco");

                entity.HasOne(d => d.LaboratorioIdLaboratorioNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.LaboratorioIdLaboratorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_laboratorio");

                entity.HasOne(d => d.ProductoBioequivalenteNavigation)
                    .WithMany(p => p.InverseProductoBioequivalenteNavigation)
                    .HasForeignKey(d => d.ProductoBioequivalente)
                    .HasConstraintName("FK_producto_producto");
            });

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.HasKey(e => e.IdProvincia)
                    .HasName("PK_ciudad_idCiudad");

                entity.ToTable("provincia");

                entity.Property(e => e.IdProvincia)
                    .HasColumnName("idProvincia")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreProvincia)
                    .IsRequired()
                    .HasColumnName("nombreProvincia")
                    .HasMaxLength(45);

                entity.Property(e => e.RegionIdRegion).HasColumnName("Region_idRegion");

                entity.HasOne(d => d.RegionIdRegionNavigation)
                    .WithMany(p => p.Provincia)
                    .HasForeignKey(d => d.RegionIdRegion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_provincia_region");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.IdRegion);

                entity.ToTable("region");

                entity.Property(e => e.IdRegion)
                    .HasColumnName("idRegion")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreRegion)
                    .IsRequired()
                    .HasColumnName("nombreRegion")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.IdSucursal)
                    .HasName("PK_sucursal_idSucursal");

                entity.ToTable("sucursal");

                entity.Property(e => e.IdSucursal).HasColumnName("idSucursal");

                entity.Property(e => e.ComunaIdComuna).HasColumnName("Comuna_idComuna");

                entity.Property(e => e.DireccionSucursal)
                    .IsRequired()
                    .HasColumnName("direccionSucursal")
                    .HasMaxLength(255);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FarmaciaIdFarmacia).HasColumnName("Farmacia_idFarmacia");

                entity.Property(e => e.Latitud).HasColumnName("latitud");

                entity.Property(e => e.Longitud).HasColumnName("longitud");

                entity.Property(e => e.TelefonoSucursal)
                    .IsRequired()
                    .HasColumnName("telefonoSucursal")
                    .HasMaxLength(45);

                entity.HasOne(d => d.ComunaIdComunaNavigation)
                    .WithMany(p => p.Sucursal)
                    .HasForeignKey(d => d.ComunaIdComuna)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sucursal_comuna");

                entity.HasOne(d => d.FarmaciaIdFarmaciaNavigation)
                    .WithMany(p => p.Sucursal)
                    .HasForeignKey(d => d.FarmaciaIdFarmacia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sucursal_farmacia");
            });

            modelBuilder.Entity<Tipousuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK_tipousuario_idTipoUsuario");

                entity.ToTable("tipousuario");

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnName("idTipoUsuario")
                    .ValueGeneratedNever();

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreTipoUsuario)
                    .IsRequired()
                    .HasColumnName("nombreTipoUsuario")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Unidadmedida>(entity =>
            {
                entity.HasKey(e => e.IdUnidadMedida);

                entity.ToTable("unidadmedida");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreUnidadMedida)
                    .IsRequired()
                    .HasColumnName("nombreUnidadMedida")
                    .HasMaxLength(45);

                entity.Property(e => e.SimboloUnidadMedida)
                    .IsRequired()
                    .HasColumnName("simboloUnidadMedida")
                    .HasMaxLength(5)
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK_usuario_idUsuario");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.EmailUsuario)
                    .HasName("UQ_usuario_emailUsuario")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.Property(e => e.ApMaternoUsuario)
                    .HasColumnName("apMaternoUsuario")
                    .HasMaxLength(45);

                entity.Property(e => e.ApPaternoUsuario)
                    .IsRequired()
                    .HasColumnName("apPaternoUsuario")
                    .HasMaxLength(45);

                entity.Property(e => e.EmailUsuario)
                    .IsRequired()
                    .HasColumnName("emailUsuario")
                    .HasMaxLength(45);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombresUsuario)
                    .IsRequired()
                    .HasColumnName("nombresUsuario")
                    .HasMaxLength(45);

                entity.Property(e => e.PasswordUsuario)
                    .IsRequired()
                    .HasColumnName("passwordUsuario")
                    .HasMaxLength(45);

                entity.Property(e => e.TipoUsuarioIdTipoUsuario).HasColumnName("TipoUsuario_idTipoUsuario");

                entity.HasOne(d => d.TipoUsuarioIdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.TipoUsuarioIdTipoUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_tipoUsuario");
            });
        }
    }
}
