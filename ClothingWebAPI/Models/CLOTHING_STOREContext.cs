using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClothingWebAPI.Models
{
    public partial class CLOTHING_STOREContext : DbContext
    {
        public CLOTHING_STOREContext()
        {
        }

        public CLOTHING_STOREContext(DbContextOptions<CLOTHING_STOREContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BangMauPendingDelete> BangMauPendingDelete { get; set; }
        public virtual DbSet<BangSize> BangSize { get; set; }
        public virtual DbSet<ChiTietDonDatHangPendingDelete> ChiTietDonDatHangPendingDelete { get; set; }
        public virtual DbSet<ChiTietGioHang> ChiTietGioHang { get; set; }
        public virtual DbSet<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        public virtual DbSet<ChiTietPhieuTra> ChiTietPhieuTra { get; set; }
        public virtual DbSet<ChiTietSanPham> ChiTietSanPham { get; set; }
        public virtual DbSet<DonDatHangPendingDelete> DonDatHangPendingDelete { get; set; }
        public virtual DbSet<GioHang> GioHang { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMai { get; set; }
        public virtual DbSet<NhaCungCapPendingDelete> NhaCungCapPendingDelete { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhap { get; set; }
        public virtual DbSet<PhieuTra> PhieuTra { get; set; }
        public virtual DbSet<Quyen> Quyen { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=CLOTHING_STORE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangMauPendingDelete>(entity =>
            {
                entity.HasKey(e => e.MaMau);

                entity.ToTable("BANG_MAU_PENDING_DELETE");

                entity.Property(e => e.MaMau)
                    .HasColumnName("MA_MAU")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenMau)
                    .HasColumnName("TEN_MAU")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BangSize>(entity =>
            {
                entity.HasKey(e => e.MaSize);

                entity.ToTable("BANG_SIZE");

                entity.Property(e => e.MaSize)
                    .HasColumnName("MA_SIZE")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenSize)
                    .HasColumnName("TEN_SIZE")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ChiTietDonDatHangPendingDelete>(entity =>
            {
                entity.HasKey(e => new { e.MaDdh, e.MaCtSp });

                entity.ToTable("CHI_TIET_DON_DAT_HANG_PENDING_DELETE");

                entity.Property(e => e.MaDdh)
                    .HasColumnName("MA_DDH")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaCtSp)
                    .HasColumnName("MA_CT_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gia).HasColumnName("GIA");

                entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");
            });

            modelBuilder.Entity<ChiTietGioHang>(entity =>
            {
                entity.HasKey(e => new { e.IdGh, e.MaCtSp });

                entity.ToTable("CHI_TIET_GIO_HANG");

                entity.Property(e => e.IdGh).HasColumnName("ID_GH");

                entity.Property(e => e.MaCtSp)
                    .HasColumnName("MA_CT_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gia).HasColumnName("GIA");

                entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");

                entity.HasOne(d => d.IdGhNavigation)
                    .WithMany(p => p.ChiTietGioHang)
                    .HasForeignKey(d => d.IdGh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_GIO_HANG_GIO_HANG");

                entity.HasOne(d => d.MaCtSpNavigation)
                    .WithMany(p => p.ChiTietGioHang)
                    .HasForeignKey(d => d.MaCtSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_GIO_HANG_CHI_TIET_SAN_PHAM");
            });

            modelBuilder.Entity<ChiTietKhuyenMai>(entity =>
            {
                entity.HasKey(e => new { e.MaKm, e.MaCtSp });

                entity.ToTable("CHI_TIET_KHUYEN_MAI");

                entity.Property(e => e.MaKm)
                    .HasColumnName("MA_KM")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaCtSp)
                    .HasColumnName("MA_CT_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PhanTramGiam).HasColumnName("PHAN_TRAM_GIAM");

                entity.HasOne(d => d.MaCtSpNavigation)
                    .WithMany(p => p.ChiTietKhuyenMai)
                    .HasForeignKey(d => d.MaCtSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_KHUYEN_MAI_CHI_TIET_SAN_PHAM");

                entity.HasOne(d => d.MaKmNavigation)
                    .WithMany(p => p.ChiTietKhuyenMai)
                    .HasForeignKey(d => d.MaKm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_KHUYEN_MAI_KHUYEN_MAI");
            });

            modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
            {
                entity.HasKey(e => new { e.MaPn, e.MaCtSp });

                entity.ToTable("CHI_TIET_PHIEU_NHAP");

                entity.Property(e => e.MaPn)
                    .HasColumnName("MA_PN")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaCtSp)
                    .HasColumnName("MA_CT_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gia).HasColumnName("GIA");

                entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");

                entity.HasOne(d => d.MaCtSpNavigation)
                    .WithMany(p => p.ChiTietPhieuNhap)
                    .HasForeignKey(d => d.MaCtSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_NHAP_CHI_TIET_SAN_PHAM");

                entity.HasOne(d => d.MaPnNavigation)
                    .WithMany(p => p.ChiTietPhieuNhap)
                    .HasForeignKey(d => d.MaPn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_NHAP_PHIEU_NHAP");
            });

            modelBuilder.Entity<ChiTietPhieuTra>(entity =>
            {
                entity.HasKey(e => new { e.MaPt, e.MaCtSp });

                entity.ToTable("CHI_TIET_PHIEU_TRA");

                entity.Property(e => e.MaPt)
                    .HasColumnName("MA_PT")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaCtSp)
                    .HasColumnName("MA_CT_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SoLuong).HasColumnName("SO_LUONG");

                entity.HasOne(d => d.MaCtSpNavigation)
                    .WithMany(p => p.ChiTietPhieuTra)
                    .HasForeignKey(d => d.MaCtSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_TRA_CHI_TIET_SAN_PHAM");

                entity.HasOne(d => d.MaPtNavigation)
                    .WithMany(p => p.ChiTietPhieuTra)
                    .HasForeignKey(d => d.MaPt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_TRA_PHIEU_TRA");
            });

            modelBuilder.Entity<ChiTietSanPham>(entity =>
            {
                entity.HasKey(e => e.MaCtSp);

                entity.ToTable("CHI_TIET_SAN_PHAM");

                entity.Property(e => e.MaCtSp)
                    .HasColumnName("MA_CT_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Gia).HasColumnName("GIA");

                entity.Property(e => e.MaSize)
                    .IsRequired()
                    .HasColumnName("MA_SIZE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaSp)
                    .IsRequired()
                    .HasColumnName("MA_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SlTon).HasColumnName("SL_TON");

                entity.HasOne(d => d.MaSizeNavigation)
                    .WithMany(p => p.ChiTietSanPham)
                    .HasForeignKey(d => d.MaSize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_SAN_PHAM_BANG_SIZE");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.ChiTietSanPham)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_SAN_PHAM_SAN_PHAM");
            });

            modelBuilder.Entity<DonDatHangPendingDelete>(entity =>
            {
                entity.HasKey(e => e.MaDdh);

                entity.ToTable("DON_DAT_HANG_PENDING_DELETE");

                entity.Property(e => e.MaDdh)
                    .HasColumnName("MA_DDH")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MaNcc)
                    .IsRequired()
                    .HasColumnName("MA_NCC")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaNv)
                    .HasColumnName("MA_NV")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao)
                    .HasColumnName("NGAY_TAO")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<GioHang>(entity =>
            {
                entity.HasKey(e => e.IdGh);

                entity.ToTable("GIO_HANG");

                entity.Property(e => e.IdGh).HasColumnName("ID_GH");

                entity.Property(e => e.DiaChi)
                    .HasColumnName("DIA_CHI")
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .HasColumnName("HO_TEN")
                    .HasMaxLength(60);

                entity.Property(e => e.MaKh)
                    .HasColumnName("MA_KH")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaNvDuyet)
                    .HasColumnName("MA_NV_DUYET")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaNvGiao)
                    .HasColumnName("MA_NV_GIAO")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao)
                    .HasColumnName("NGAY_TAO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TrangThai).HasColumnName("TRANG_THAI");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.GioHang)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_GIO_HANG_KHACH_HANG");

                entity.HasOne(d => d.MaNvDuyetNavigation)
                    .WithMany(p => p.GioHangMaNvDuyetNavigation)
                    .HasForeignKey(d => d.MaNvDuyet)
                    .HasConstraintName("FK_GIO_HANG_NHAN_VIEN");

                entity.HasOne(d => d.MaNvGiaoNavigation)
                    .WithMany(p => p.GioHangMaNvGiaoNavigation)
                    .HasForeignKey(d => d.MaNvGiao)
                    .HasConstraintName("FK_GIO_HANG_NHAN_VIEN1");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HOA_DON");

                entity.HasIndex(e => e.IdGh)
                    .HasName("UQ_ID_GH_HOA_DON")
                    .IsUnique();

                entity.Property(e => e.MaHd)
                    .HasColumnName("MA_HD")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.IdGh).HasColumnName("ID_GH");

                entity.Property(e => e.NgayTao)
                    .HasColumnName("NGAY_TAO")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdGhNavigation)
                    .WithOne(p => p.HoaDon)
                    .HasForeignKey<HoaDon>(d => d.IdGh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOA_DON_GIO_HANG");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("KHACH_HANG");

                entity.HasIndex(e => e.MaTk)
                    .HasName("UQ_MA_TK_KHACH_HANG")
                    .IsUnique();

                entity.Property(e => e.MaKh)
                    .HasColumnName("MA_KH")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DiaChi)
                    .HasColumnName("DIA_CHI")
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .HasColumnName("HO_TEN")
                    .HasMaxLength(60);

                entity.Property(e => e.MaSoThue)
                    .HasColumnName("MA_SO_THUE")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaTk)
                    .HasColumnName("MA_TK")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaTkNavigation)
                    .WithOne(p => p.KhachHang)
                    .HasForeignKey<KhachHang>(d => d.MaTk)
                    .HasConstraintName("FK_KHACH_HANG_TAI_KHOAN");
            });

            modelBuilder.Entity<KhuyenMai>(entity =>
            {
                entity.HasKey(e => e.MaKm);

                entity.ToTable("KHUYEN_MAI");

                entity.Property(e => e.MaKm)
                    .HasColumnName("MA_KM")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MaNv)
                    .HasColumnName("MA_NV")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NgayApDung)
                    .HasColumnName("NGAY_AP_DUNG")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.KhuyenMai)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_KHUYEN_MAI_NHAN_VIEN");
            });

            modelBuilder.Entity<NhaCungCapPendingDelete>(entity =>
            {
                entity.HasKey(e => e.MaNcc);

                entity.ToTable("NHA_CUNG_CAP_PENDING_DELETE");

                entity.Property(e => e.MaNcc)
                    .HasColumnName("MA_NCC")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DiaChi)
                    .HasColumnName("DIA_CHI")
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenNcc)
                    .IsRequired()
                    .HasColumnName("TEN_NCC")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNv);

                entity.ToTable("NHAN_VIEN");

                entity.HasIndex(e => e.MaTk)
                    .HasName("UQ_MA_TK")
                    .IsUnique();

                entity.Property(e => e.MaNv)
                    .HasColumnName("MA_NV")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Cmnd)
                    .HasColumnName("CMND")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi)
                    .HasColumnName("DIA_CHI")
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen)
                    .HasColumnName("HO_TEN")
                    .HasMaxLength(60);

                entity.Property(e => e.MaTk)
                    .HasColumnName("MA_TK")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaTkNavigation)
                    .WithOne(p => p.NhanVien)
                    .HasForeignKey<NhanVien>(d => d.MaTk)
                    .HasConstraintName("FK_NHAN_VIEN_TAI_KHOAN");
            });

            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.HasKey(e => e.MaPn);

                entity.ToTable("PHIEU_NHAP");

                entity.Property(e => e.MaPn)
                    .HasColumnName("MA_PN")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MaNv)
                    .HasColumnName("MA_NV")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao)
                    .HasColumnName("NGAY_TAO")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.PhieuNhap)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_PHIEU_NHAP_NHAN_VIEN");
            });

            modelBuilder.Entity<PhieuTra>(entity =>
            {
                entity.HasKey(e => e.MaPt);

                entity.ToTable("PHIEU_TRA");

                entity.Property(e => e.MaPt)
                    .HasColumnName("MA_PT")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MaHd)
                    .IsRequired()
                    .HasColumnName("MA_HD")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MaNv)
                    .HasColumnName("MA_NV")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao)
                    .HasColumnName("NGAY_TAO")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.PhieuTra)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEU_TRA_HOA_DON");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.PhieuTra)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_PHIEU_TRA_NHAN_VIEN");
            });

            modelBuilder.Entity<Quyen>(entity =>
            {
                entity.HasKey(e => e.MaQuyen);

                entity.ToTable("QUYEN");

                entity.Property(e => e.MaQuyen)
                    .HasColumnName("MA_QUYEN")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TenQuyen)
                    .HasColumnName("TEN_QUYEN")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("SAN_PHAM");

                entity.Property(e => e.MaSp)
                    .HasColumnName("MA_SP")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.HinhAnh)
                    .HasColumnName("HINH_ANH")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LuotXem).HasColumnName("LUOT_XEM");

                entity.Property(e => e.MaTl)
                    .HasColumnName("MA_TL")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NgayTao)
                    .HasColumnName("NGAY_TAO")
                    .HasColumnType("datetime");

                entity.Property(e => e.TenSp)
                    .HasColumnName("TEN_SP")
                    .HasMaxLength(150);

                entity.HasOne(d => d.MaTlNavigation)
                    .WithMany(p => p.SanPham)
                    .HasForeignKey(d => d.MaTl)
                    .HasConstraintName("FK_SAN_PHAM_THE_LOAI");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk);

                entity.ToTable("TAI_KHOAN");

                entity.Property(e => e.MaTk)
                    .HasColumnName("MA_TK")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MaQuyen)
                    .IsRequired()
                    .HasColumnName("MA_QUYEN")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .HasColumnName("MAT_KHAU")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaQuyenNavigation)
                    .WithMany(p => p.TaiKhoan)
                    .HasForeignKey(d => d.MaQuyen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TAI_KHOAN_QUYEN");
            });

            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.HasKey(e => e.MaTl);

                entity.ToTable("THE_LOAI");

                entity.Property(e => e.MaTl)
                    .HasColumnName("MA_TL")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CapTl).HasColumnName("CAP_TL");

                entity.Property(e => e.MaTlCha)
                    .HasColumnName("MA_TL_CHA")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TenTl)
                    .HasColumnName("TEN_TL")
                    .HasMaxLength(50);
            });
        }
    }
}
