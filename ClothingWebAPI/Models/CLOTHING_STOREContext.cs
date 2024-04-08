using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<BANG_MAU> BANG_MAU { get; set; }

        public virtual DbSet<BANG_SIZE> BANG_SIZE { get; set; }

        public virtual DbSet<CHI_TIET_GIO_HANG> CHI_TIET_GIO_HANG { get; set; }

        public virtual DbSet<CHI_TIET_KHUYEN_MAI> CHI_TIET_KHUYEN_MAI { get; set; }

        public virtual DbSet<CHI_TIET_PHIEU_NHAP> CHI_TIET_PHIEU_NHAP { get; set; }

        public virtual DbSet<CHI_TIET_PHIEU_TRA> CHI_TIET_PHIEU_TRA { get; set; }

        public virtual DbSet<CHI_TIET_SAN_PHAM> CHI_TIET_SAN_PHAM { get; set; }

        public virtual DbSet<DANH_GIA_SAN_PHAM> DANH_GIA_SAN_PHAM { get; set; }

        public virtual DbSet<GIO_HANG> GIO_HANG { get; set; }

        public virtual DbSet<HINH_ANH_SAN_PHAM> HINH_ANH_SAN_PHAM { get; set; }

        public virtual DbSet<HOA_DON> HOA_DON { get; set; }

        public virtual DbSet<KHACH_HANG> KHACH_HANG { get; set; }

        public virtual DbSet<KHUYEN_MAI> KHUYEN_MAI { get; set; }

        public virtual DbSet<NHA_CUNG_CAP_PENDING_DELETE> NHA_CUNG_CAP_PENDING_DELETE { get; set; }

        public virtual DbSet<NHAN_VIEN> NHAN_VIEN { get; set; }

        public virtual DbSet<PHIEU_NHAP> PHIEU_NHAP { get; set; }

        public virtual DbSet<PHIEU_TRA> PHIEU_TRA { get; set; }

        public virtual DbSet<QUYEN> QUYEN { get; set; }

        public virtual DbSet<SAN_PHAM> SAN_PHAM { get; set; }

        public virtual DbSet<TAI_KHOAN> TAI_KHOAN { get; set; }

        public virtual DbSet<THAY_DOI_GIA> THAY_DOI_GIA { get; set; }

        public virtual DbSet<THE_LOAI> THE_LOAI { get; set; }

        public virtual DbSet<TI_GIA> TI_GIA { get; set; }

        public virtual DbSet<YEU_THICH_SAN_PHAM> YEU_THICH_SAN_PHAM { get; set; }

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
            modelBuilder.Entity<BANG_MAU>(entity =>
            {
                entity.HasKey(e => e.MA_MAU);

                entity.Property(e => e.MA_MAU)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TEN_MAU).HasMaxLength(50);

                entity.Property(e => e.TEN_TIENG_ANH)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BANG_SIZE>(entity =>
            {
                entity.HasKey(e => e.MA_SIZE);

                entity.Property(e => e.MA_SIZE)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TEN_SIZE)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CHI_TIET_GIO_HANG>(entity =>
            {
                entity.HasKey(e => new { e.ID_GH, e.MA_CT_SP });

                entity.HasOne(d => d.ID_GHNavigation)
                    .WithMany(p => p.CHI_TIET_GIO_HANG)
                    .HasForeignKey(d => d.ID_GH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_GIO_HANG_GIO_HANG");

                entity.HasOne(d => d.MA_CT_SPNavigation)
                    .WithMany(p => p.CHI_TIET_GIO_HANG)
                    .HasForeignKey(d => d.MA_CT_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_GIO_HANG_CHI_TIET_SAN_PHAM");
            });

            modelBuilder.Entity<CHI_TIET_KHUYEN_MAI>(entity =>
            {
                entity.HasKey(e => new { e.MA_KM, e.MA_SP });

                entity.Property(e => e.MA_KM)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_SP)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_KMNavigation)
                    .WithMany(p => p.CHI_TIET_KHUYEN_MAI)
                    .HasForeignKey(d => d.MA_KM)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_KHUYEN_MAI_KHUYEN_MAI");

                entity.HasOne(d => d.MA_SPNavigation)
                    .WithMany(p => p.CHI_TIET_KHUYEN_MAI)
                    .HasForeignKey(d => d.MA_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_KHUYEN_MAI_SAN_PHAM");
            });

            modelBuilder.Entity<CHI_TIET_PHIEU_NHAP>(entity =>
            {
                entity.HasKey(e => new { e.MA_PN, e.MA_CT_SP });

                entity.Property(e => e.MA_PN)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_CT_SPNavigation)
                    .WithMany(p => p.CHI_TIET_PHIEU_NHAP)
                    .HasForeignKey(d => d.MA_CT_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_NHAP_CHI_TIET_SAN_PHAM");

                entity.HasOne(d => d.MA_PNNavigation)
                    .WithMany(p => p.CHI_TIET_PHIEU_NHAP)
                    .HasForeignKey(d => d.MA_PN)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_NHAP_PHIEU_NHAP");
            });

            modelBuilder.Entity<CHI_TIET_PHIEU_TRA>(entity =>
            {
                entity.HasKey(e => new { e.MA_PT, e.MA_CT_SP });

                entity.Property(e => e.MA_PT)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_CT_SPNavigation)
                    .WithMany(p => p.CHI_TIET_PHIEU_TRA)
                    .HasForeignKey(d => d.MA_CT_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_TRA_CHI_TIET_SAN_PHAM");

                entity.HasOne(d => d.MA_PTNavigation)
                    .WithMany(p => p.CHI_TIET_PHIEU_TRA)
                    .HasForeignKey(d => d.MA_PT)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_PHIEU_TRA_PHIEU_TRA");
            });

            modelBuilder.Entity<CHI_TIET_SAN_PHAM>(entity =>
            {
                entity.HasKey(e => e.MA_CT_SP);

                entity.HasIndex(e => new { e.MA_SP, e.MA_SIZE, e.MA_MAU })
                    .HasName("UK_CHI_TIET_SAN_PHAM")
                    .IsUnique();

                entity.Property(e => e.MA_MAU)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_SIZE)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_SP)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_MAUNavigation)
                    .WithMany(p => p.CHI_TIET_SAN_PHAM)
                    .HasForeignKey(d => d.MA_MAU)
                    .HasConstraintName("FK_CHI_TIET_SAN_PHAM_BANG_MAU");

                entity.HasOne(d => d.MA_SIZENavigation)
                    .WithMany(p => p.CHI_TIET_SAN_PHAM)
                    .HasForeignKey(d => d.MA_SIZE)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_SAN_PHAM_BANG_SIZE");

                entity.HasOne(d => d.MA_SPNavigation)
                    .WithMany(p => p.CHI_TIET_SAN_PHAM)
                    .HasForeignKey(d => d.MA_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHI_TIET_SAN_PHAM_SAN_PHAM");
            });

            modelBuilder.Entity<DANH_GIA_SAN_PHAM>(entity =>
            {
                entity.HasKey(e => new { e.MA_KH, e.MA_SP });

                entity.Property(e => e.MA_KH)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_SP)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_DANH_GIA).HasColumnType("datetime");

                entity.Property(e => e.NOI_DUNG).HasMaxLength(1000);

                entity.HasOne(d => d.MA_KHNavigation)
                    .WithMany(p => p.DANH_GIA_SAN_PHAM)
                    .HasForeignKey(d => d.MA_KH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DANH_GIA_SAN_PHAM_KHACH_HANG");

                entity.HasOne(d => d.MA_SPNavigation)
                    .WithMany(p => p.DANH_GIA_SAN_PHAM)
                    .HasForeignKey(d => d.MA_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DANH_GIA_SAN_PHAM_SAN_PHAM");
            });

            modelBuilder.Entity<GIO_HANG>(entity =>
            {
                entity.HasKey(e => e.ID_GH);

                entity.Property(e => e.DIA_CHI).HasMaxLength(200);

                entity.Property(e => e.EMAIL)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GHI_CHU).HasMaxLength(500);

                entity.Property(e => e.HO_TEN).HasMaxLength(60);

                entity.Property(e => e.MA_KH)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_NV_DUYET)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_NV_GIAO)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_GIAO).HasColumnType("datetime");

                entity.Property(e => e.NGAY_TAO).HasColumnType("datetime");

                entity.Property(e => e.SDT)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_KHNavigation)
                    .WithMany(p => p.GIO_HANG)
                    .HasForeignKey(d => d.MA_KH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GIO_HANG_KHACH_HANG");

                entity.HasOne(d => d.MA_NV_DUYETNavigation)
                    .WithMany(p => p.GIO_HANGMA_NV_DUYETNavigation)
                    .HasForeignKey(d => d.MA_NV_DUYET)
                    .HasConstraintName("FK_GIO_HANG_NHAN_VIEN");

                entity.HasOne(d => d.MA_NV_GIAONavigation)
                    .WithMany(p => p.GIO_HANGMA_NV_GIAONavigation)
                    .HasForeignKey(d => d.MA_NV_GIAO)
                    .HasConstraintName("FK_GIO_HANG_NHAN_VIEN1");
            });

            modelBuilder.Entity<HINH_ANH_SAN_PHAM>(entity =>
            {
                entity.HasKey(e => new { e.MA_SP, e.MA_MAU });

                entity.Property(e => e.MA_SP)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_MAU)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.HINH_ANH).HasMaxLength(400);

                entity.HasOne(d => d.MA_MAUNavigation)
                    .WithMany(p => p.HINH_ANH_SAN_PHAM)
                    .HasForeignKey(d => d.MA_MAU)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HINH_ANH_SAN_PHAM_BANG_MAU");

                entity.HasOne(d => d.MA_SPNavigation)
                    .WithMany(p => p.HINH_ANH_SAN_PHAM)
                    .HasForeignKey(d => d.MA_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HINH_ANH_SAN_PHAM_SAN_PHAM");
            });

            modelBuilder.Entity<HOA_DON>(entity =>
            {
                entity.HasKey(e => e.MA_HD);

                entity.HasIndex(e => e.ID_GH)
                    .HasName("UQ_ID_GH_HOA_DON")
                    .IsUnique();

                entity.Property(e => e.MA_HD)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MA_NV)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_TAO).HasColumnType("datetime");

                entity.HasOne(d => d.ID_GHNavigation)
                    .WithOne(p => p.HOA_DON)
                    .HasForeignKey<HOA_DON>(d => d.ID_GH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOA_DON_GIO_HANG");

                entity.HasOne(d => d.MA_NVNavigation)
                    .WithMany(p => p.HOA_DON)
                    .HasForeignKey(d => d.MA_NV)
                    .HasConstraintName("FK_HOA_DON_NHAN_VIEN");
            });

            modelBuilder.Entity<KHACH_HANG>(entity =>
            {
                entity.HasKey(e => e.MA_KH);

                entity.HasIndex(e => e.EMAIL)
                    .HasName("UQ_EMAIL_KH")
                    .IsUnique();

                entity.HasIndex(e => e.MA_TK)
                    .HasName("UQ_MA_TK_KHACH_HANG")
                    .IsUnique();

                entity.Property(e => e.MA_KH)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DIA_CHI).HasMaxLength(200);

                entity.Property(e => e.EMAIL)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HO_TEN)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.MA_SO_THUE)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_TK)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SDT)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_TKNavigation)
                    .WithOne(p => p.KHACH_HANG)
                    .HasForeignKey<KHACH_HANG>(d => d.MA_TK)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KHACH_HANG_TAI_KHOAN");
            });

            modelBuilder.Entity<KHUYEN_MAI>(entity =>
            {
                entity.HasKey(e => e.MA_KM);

                entity.Property(e => e.MA_KM)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MA_NV)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MO_TA).HasMaxLength(250);

                entity.Property(e => e.NGAY_AP_DUNG).HasColumnType("datetime");

                entity.HasOne(d => d.MA_NVNavigation)
                    .WithMany(p => p.KHUYEN_MAI)
                    .HasForeignKey(d => d.MA_NV)
                    .HasConstraintName("FK_KHUYEN_MAI_NHAN_VIEN");
            });

            modelBuilder.Entity<NHA_CUNG_CAP_PENDING_DELETE>(entity =>
            {
                entity.HasKey(e => e.MA_NCC);

                entity.Property(e => e.MA_NCC)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DIA_CHI).HasMaxLength(200);

                entity.Property(e => e.EMAIL)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SDT)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TEN_NCC)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NHAN_VIEN>(entity =>
            {
                entity.HasKey(e => e.MA_NV);

                entity.HasIndex(e => e.EMAIL)
                    .HasName("UQ_EMAIL")
                    .IsUnique();

                entity.HasIndex(e => e.MA_TK)
                    .HasName("UQ_MA_TK")
                    .IsUnique();

                entity.Property(e => e.MA_NV)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CMND)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DIA_CHI).HasMaxLength(200);

                entity.Property(e => e.EMAIL)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HO_TEN)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.MA_TK)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SDT)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_TKNavigation)
                    .WithOne(p => p.NHAN_VIEN)
                    .HasForeignKey<NHAN_VIEN>(d => d.MA_TK)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NHAN_VIEN_TAI_KHOAN");
            });

            modelBuilder.Entity<PHIEU_NHAP>(entity =>
            {
                entity.HasKey(e => e.MA_PN);

                entity.Property(e => e.MA_PN)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MA_NV)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_TAO).HasColumnType("datetime");

                entity.HasOne(d => d.MA_NVNavigation)
                    .WithMany(p => p.PHIEU_NHAP)
                    .HasForeignKey(d => d.MA_NV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEU_NHAP_NHAN_VIEN");
            });

            modelBuilder.Entity<PHIEU_TRA>(entity =>
            {
                entity.HasKey(e => e.MA_PT);

                entity.Property(e => e.MA_PT)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MA_HD)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_NV)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_TAO).HasColumnType("datetime");

                entity.HasOne(d => d.MA_HDNavigation)
                    .WithMany(p => p.PHIEU_TRA)
                    .HasForeignKey(d => d.MA_HD)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEU_TRA_HOA_DON");

                entity.HasOne(d => d.MA_NVNavigation)
                    .WithMany(p => p.PHIEU_TRA)
                    .HasForeignKey(d => d.MA_NV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEU_TRA_NHAN_VIEN");
            });

            modelBuilder.Entity<QUYEN>(entity =>
            {
                entity.HasKey(e => e.MA_QUYEN);

                entity.Property(e => e.MA_QUYEN)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TEN_QUYEN)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SAN_PHAM>(entity =>
            {
                entity.HasKey(e => e.MA_SP);

                entity.Property(e => e.MA_SP)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.HINH_ANH)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.MA_TL)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MO_TA).HasMaxLength(500);

                entity.Property(e => e.NGAY_TAO).HasColumnType("datetime");

                entity.Property(e => e.TEN_SP)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.MA_TLNavigation)
                    .WithMany(p => p.SAN_PHAM)
                    .HasForeignKey(d => d.MA_TL)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SAN_PHAM_THE_LOAI");
            });

            modelBuilder.Entity<TAI_KHOAN>(entity =>
            {
                entity.HasKey(e => e.MA_TK);

                entity.Property(e => e.MA_TK)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MAT_KHAU)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MA_QUYEN)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MA_QUYENNavigation)
                    .WithMany(p => p.TAI_KHOAN)
                    .HasForeignKey(d => d.MA_QUYEN)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TAI_KHOAN_QUYEN");
            });

            modelBuilder.Entity<THAY_DOI_GIA>(entity =>
            {
                entity.HasKey(e => new { e.MA_NV, e.MA_CT_SP, e.NGAY_THAY_DOI });

                entity.Property(e => e.MA_NV)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_THAY_DOI).HasColumnType("datetime");

                entity.HasOne(d => d.MA_CT_SPNavigation)
                    .WithMany(p => p.THAY_DOI_GIA)
                    .HasForeignKey(d => d.MA_CT_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_THAY_DOI_GIA_CHI_TIET_SAN_PHAM");

                entity.HasOne(d => d.MA_NVNavigation)
                    .WithMany(p => p.THAY_DOI_GIA)
                    .HasForeignKey(d => d.MA_NV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_THAY_DOI_GIA_NHAN_VIEN");
            });

            modelBuilder.Entity<THE_LOAI>(entity =>
            {
                entity.HasKey(e => e.MA_TL);

                entity.Property(e => e.MA_TL)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MA_TL_CHA)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TEN_TL)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TI_GIA>(entity =>
            {
                entity.HasKey(e => new { e.MA_NV, e.NGAY_AP_DUNG });

                entity.Property(e => e.MA_NV)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_AP_DUNG).HasColumnType("datetime");

                entity.Property(e => e.TI_GIA1).HasColumnName("TI_GIA");

                entity.HasOne(d => d.MA_NVNavigation)
                    .WithMany(p => p.TI_GIA)
                    .HasForeignKey(d => d.MA_NV)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TI_GIA_NHAN_VIEN");
            });

            modelBuilder.Entity<YEU_THICH_SAN_PHAM>(entity =>
            {
                entity.HasKey(e => new { e.MA_KH, e.MA_SP });

                entity.Property(e => e.MA_KH)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MA_SP)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.NGAY_THEM).HasColumnType("datetime");

                entity.HasOne(d => d.MA_KHNavigation)
                    .WithMany(p => p.YEU_THICH_SAN_PHAM)
                    .HasForeignKey(d => d.MA_KH)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_YEU_THICH_SAN_PHAM_KHACH_HANG");

                entity.HasOne(d => d.MA_SPNavigation)
                    .WithMany(p => p.YEU_THICH_SAN_PHAM)
                    .HasForeignKey(d => d.MA_SP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_YEU_THICH_SAN_PHAM_SAN_PHAM");
            });
        }
    }
}
