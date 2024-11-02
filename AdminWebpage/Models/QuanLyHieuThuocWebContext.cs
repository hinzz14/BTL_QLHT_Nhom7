using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdminWebpage.Models;

public partial class QuanLyHieuThuocWebContext : DbContext
{
    public QuanLyHieuThuocWebContext()
    {
    }

    public QuanLyHieuThuocWebContext(DbContextOptions<QuanLyHieuThuocWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; }

    public virtual DbSet<TChiTietHdn> TChiTietHdns { get; set; }

    public virtual DbSet<THoaDonBan> THoaDonBans { get; set; }

    public virtual DbSet<THoaDonNhap> THoaDonNhaps { get; set; }

    public virtual DbSet<TKhachHang> TKhachHangs { get; set; }

    public virtual DbSet<TLoaiThuoc> TLoaiThuocs { get; set; }

    public virtual DbSet<TNhaCungCap> TNhaCungCaps { get; set; }

    public virtual DbSet<TNhanVien> TNhanViens { get; set; }

    public virtual DbSet<TThuoc> TThuocs { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LOU1S\\SQLEXPRESS;Initial Catalog=QuanLyHieuThuoc;Persist Security Info=True;User ID=sa;Password=abc123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Account);

            entity.ToTable("Login");

            entity.Property(e => e.Account).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(255);
        });

        modelBuilder.Entity<TChiTietHdb>(entity =>
        {
            entity.HasKey(e => new { e.SoHdb, e.MaThuoc });

            entity.ToTable("tChiTietHDB");

            entity.Property(e => e.SoHdb)
                .HasMaxLength(10)
                .HasColumnName("SoHDB");
            entity.Property(e => e.MaThuoc).HasMaxLength(10);
            entity.Property(e => e.KhuyenMai).HasMaxLength(10);
            entity.Property(e => e.Slban).HasColumnName("SLBan");

            entity.HasOne(d => d.MaThuocNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.MaThuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_MaThuoc");

            entity.HasOne(d => d.SoHdbNavigation).WithMany(p => p.TChiTietHdbs)
                .HasForeignKey(d => d.SoHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDB_SoHDB");
        });

        modelBuilder.Entity<TChiTietHdn>(entity =>
        {
            entity.HasKey(e => new { e.SoHdn, e.MaThuoc });

            entity.ToTable("tChiTietHDN");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(10)
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaThuoc).HasMaxLength(10);
            entity.Property(e => e.KhuyenMai).HasMaxLength(10);
            entity.Property(e => e.Slnhap).HasColumnName("SLNhap");

            entity.HasOne(d => d.MaThuocNavigation).WithMany(p => p.TChiTietHdns)
                .HasForeignKey(d => d.MaThuoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDN_MaThuoc");

            entity.HasOne(d => d.SoHdnNavigation).WithMany(p => p.TChiTietHdns)
                .HasForeignKey(d => d.SoHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tChiTietHDN_SoHDN");
        });

        modelBuilder.Entity<THoaDonBan>(entity =>
        {
            entity.HasKey(e => e.SoHdb);

            entity.ToTable("tHoaDonBan");

            entity.Property(e => e.SoHdb)
                .HasMaxLength(10)
                .HasColumnName("SoHDB");
            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tHoaDonBan_MaKH");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.THoaDonBans)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tHoaDonBan_MaNV");
        });

        modelBuilder.Entity<THoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.SoHdn);

            entity.ToTable("tHoaDonNhap");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(10)
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tHoaDonNhap_MaNCC");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.THoaDonNhaps)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tHoaDonNhap_MaNV");
        });

        modelBuilder.Entity<TKhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("tKhachHang");

            entity.Property(e => e.MaKh)
                .HasMaxLength(10)
                .HasColumnName("MaKH");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(12)
                .HasColumnName("SDT");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .HasColumnName("GioiTinh");
            entity.Property(e => e.TenKh)
                .HasMaxLength(50)
                .HasColumnName("TenKH");
        });

        modelBuilder.Entity<TLoaiThuoc>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("tLoaiThuoc");

            entity.Property(e => e.MaLoai).HasMaxLength(10);
            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<TNhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc);

            entity.ToTable("tNhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(10)
                .HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(20);
            entity.Property(e => e.Sdt)
                .HasMaxLength(12)
                .HasColumnName("SDT");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(50)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<TNhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("tNhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(12)
                .HasColumnName("SDT");
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .HasColumnName("GioiTinh");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("TenNV");
        });

        modelBuilder.Entity<TThuoc>(entity =>
        {
            entity.HasKey(e => e.MaThuoc);

            entity.ToTable("tThuoc");

            entity.Property(e => e.MaThuoc).HasMaxLength(10);
            entity.Property(e => e.Anh).HasMaxLength(15);
            entity.Property(e => e.MaLoai).HasMaxLength(10);
            entity.Property(e => e.NgayHh).HasColumnName("NgayHH");
            entity.Property(e => e.NgaySx).HasColumnName("NgaySX");
            entity.Property(e => e.TenThuoc).HasMaxLength(50);
            entity.Property(e => e.ThanhPhan).HasMaxLength(50);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.TThuocs)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_tThuoc_MaLoai");
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
