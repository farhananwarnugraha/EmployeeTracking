using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace employeetrackingData.Models;

public partial class EmployeeTrackingContext : DbContext
{
    public EmployeeTrackingContext()
    {
    }

    public EmployeeTrackingContext(DbContextOptions<EmployeeTrackingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cabang> Cabangs { get; set; }

    public virtual DbSet<Jabatan> Jabatans { get; set; }

    public virtual DbSet<Pegawai> Pegawais { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=employee_tracking; Trusted_Connection=True;User=sa;Password=indocyber; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabang>(entity =>
        {
            entity.HasKey(e => e.Idcabang).HasName("PK__cabang__83802E7A4DF8B05C");

            entity.ToTable("cabang");

            entity.HasIndex(e => e.Kodecabang, "UQ__cabang__0E67484A40DC6813").IsUnique();

            entity.Property(e => e.Idcabang).HasColumnName("idcabang");
            entity.Property(e => e.Alamatcabang)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("alamatcabang");
            entity.Property(e => e.Kodecabang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("kodecabang");
            entity.Property(e => e.Namacabang)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("namacabang");
        });

        modelBuilder.Entity<Jabatan>(entity =>
        {
            entity.HasKey(e => e.Idjabatan).HasName("PK__jabatan__636974D1A67ADF2E");

            entity.ToTable("jabatan");

            entity.HasIndex(e => e.Kodejabatan, "UQ__jabatan__3649CCD490E8CC1D").IsUnique();

            entity.Property(e => e.Idjabatan).HasColumnName("idjabatan");
            entity.Property(e => e.Kodejabatan)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("kodejabatan");
            entity.Property(e => e.NamaJabatan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nama_jabatan");
        });

        modelBuilder.Entity<Pegawai>(entity =>
        {
            entity.HasKey(e => e.Idpegawai).HasName("PK__pegawai__2A2DF3C912D646D1");

            entity.ToTable("pegawai");

            entity.HasIndex(e => e.KodePegawai, "UQ__pegawai__0D6A9AF5717E7082").IsUnique();

            entity.Property(e => e.Idpegawai).HasColumnName("idpegawai");
            entity.Property(e => e.Idcabang).HasColumnName("idcabang");
            entity.Property(e => e.Idjabatan).HasColumnName("idjabatan");
            entity.Property(e => e.KodePegawai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("kodePegawai");
            entity.Property(e => e.Namapegawai)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("namapegawai");
            entity.Property(e => e.Tanggalberakhir).HasColumnName("tanggalberakhir");
            entity.Property(e => e.Tanggalmasuk).HasColumnName("tanggalmasuk");

            entity.HasOne(d => d.IdcabangNavigation).WithMany(p => p.Pegawais)
                .HasForeignKey(d => d.Idcabang)
                .HasConstraintName("FK__pegawai__idjabat__3B75D760");

            entity.HasOne(d => d.IdjabatanNavigation).WithMany(p => p.Pegawais)
                .HasForeignKey(d => d.Idjabatan)
                .HasConstraintName("FK__pegawai__idjabat__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
