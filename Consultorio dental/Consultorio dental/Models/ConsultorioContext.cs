using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Consultorio_dental.Models;

public partial class ConsultorioContext : DbContext
{
    public ConsultorioContext()
    {
    }

    public ConsultorioContext(DbContextOptions<ConsultorioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Citum> Cita { get; set; }

    public virtual DbSet<Dentistum> Dentista { get; set; }

    public virtual DbSet<Motivo> Motivos { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=clasefinal;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Citum>(entity =>
        {
            entity.HasKey(e => e.CitaId).HasName("PK__Cita__F0E2D9F282CC0525");

            entity.Property(e => e.DiasRestantes).HasComputedColumnSql("(datediff(day,getdate(),[Fecha]))", false);
            entity.Property(e => e.Estado).HasComputedColumnSql("(case when getdate()<(CONVERT([datetime],[Fecha])+CONVERT([datetime],[Hora])) then 'Vigente' when getdate()>=(CONVERT([datetime],[Fecha])+CONVERT([datetime],[Hora])) AND getdate()<=dateadd(minute,[Duracion],CONVERT([datetime],[Fecha])+CONVERT([datetime],[Hora])) then 'En proceso' else 'Finalizado' end)", false);
            entity.Property(e => e.HorasRestantes).HasComputedColumnSql("(datediff(hour,getdate(),CONVERT([datetime],[Fecha])+CONVERT([datetime],[Hora])))", false);

            entity.HasOne(d => d.Dentista).WithMany(p => p.Cita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__DentistaID__5165187F");

            entity.HasOne(d => d.Motivo).WithMany(p => p.Cita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__MotivoID__52593CB8");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Cita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cita__PacienteID__5070F446");
        });

        modelBuilder.Entity<Dentistum>(entity =>
        {
            entity.HasKey(e => e.DentistaId).HasName("PK__Dentista__9B90A47FA027A878");
        });

        modelBuilder.Entity<Motivo>(entity =>
        {
            entity.HasKey(e => e.MotivoId).HasName("PK__Motivo__AE78D257906BD46E");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.PacienteId).HasName("PK__Paciente__9353C07FE7EDD0EA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
