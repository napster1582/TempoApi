using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using tempo_api.Models.Account;

namespace tempo_api.Models
{
    public partial class TEMPOContext : IdentityDbContext<ApplicationUser>
    {
        public TEMPOContext()
        {
        }

        public TEMPOContext(DbContextOptions<TEMPOContext> options)
            : base(options)
        {
        }



        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<DetalleActividad> DetalleActividad { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Empleados>(entity =>
            {
                entity.HasIndex(e => e.IdUser)
                    .IsUnique();

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Documento)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad);

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK_Actividad_Empleados");
            });


            modelBuilder.Entity<DetalleActividad>(entity =>
            {
                entity.HasKey(e => e.IdDetalleActividad);

                entity.Property(e => e.Fecha).HasColumnType("smalldatetime");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.DetalleActividad)
                    .HasForeignKey(d => d.IdActividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleActividad_Actividad");
            });
        }
    }
}
