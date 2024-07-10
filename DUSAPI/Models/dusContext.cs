using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DUSAPI.Models
{
    public partial class dusContext : DbContext
    {
        public dusContext()
        {
        }

        public dusContext(DbContextOptions<dusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Branching> Branchings { get; set; } = null!;
        public virtual DbSet<Button> Buttons { get; set; } = null!;
        public virtual DbSet<Documents> Documentss { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Menu1> Menus1 { get; set; } = null!;
        public virtual DbSet<PrmtAttachment> PrmtAttachments { get; set; } = null!;
        public virtual DbSet<PrmtDetail> PrmtDetails { get; set; } = null!;
        public virtual DbSet<UserList> UserLists { get; set; } = null!;
        public virtual DbSet<VBranch> VBranches { get; set; } = null!;
        public virtual DbSet<VBranching> VBranchings { get; set; } = null!;
        public virtual DbSet<VButton> VButtons { get; set; } = null!;
        public virtual DbSet<VDocumenEntry> VDocumenEntries { get; set; } = null!;
        public virtual DbSet<VDocument> VDocuments { get; set; } = null!;
        public virtual DbSet<VMenu> VMenus { get; set; } = null!;
        public virtual DbSet<VPrmtAttachment> VPrmtAttachments { get; set; } = null!;
        public virtual DbSet<VPrmtAttachment1> VPrmtAttachments1 { get; set; } = null!;
        public virtual DbSet<VUserlist> VUserlists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=FSCITDEV1; Database=dus;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("branch");

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Branchcode)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Branchname)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Branching>(entity =>
            {
                entity.ToTable("branching");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId).HasColumnName("EmailID");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Button>(entity =>
            {
                entity.ToTable("button");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailId).HasColumnName("EmailID");

                entity.Property(e => e.IsAdd).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsEdit).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.ToTable("documents");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.Property(e => e.AssignSubmenu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Menus)
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();

                entity.Property(e => e.Type)
                    .HasMaxLength(155)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Menu1>(entity =>
            {
                entity.ToTable("menus");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.IsSelected).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<PrmtAttachment>(entity =>
            {
                entity.ToTable("prmt_attachment");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Filename)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Filepath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NewFilename)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PrmtDetail>(entity =>
            {
                entity.ToTable("prmt_details");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUploaded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Document)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.FullDetails)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<UserList>(entity =>
            {
                entity.ToTable("user_list");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ConfirmPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('A')")
                    .IsFixedLength();

                entity.Property(e => e.UserType)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VBranch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vBranch");

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Branchcode)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Branchname)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VBranching>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vBranching");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.BranchName)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VButton>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vButton");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Menu)
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VDocumenEntry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDocumenEntry");

                entity.Property(e => e.Branch)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateUploaded).HasColumnType("datetime");

                entity.Property(e => e.Document)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.FullDetails)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VDocument>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDocument");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<VMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vMenus");

                entity.Property(e => e.AssignParent)
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SubMenu)
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VPrmtAttachment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPrmt_attachment");

                entity.Property(e => e.Branch)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NewFilename)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VPrmtAttachment1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPrmt_attachments");

                entity.Property(e => e.Branch)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.NewFilename)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VUserlist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserlist");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserType)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
