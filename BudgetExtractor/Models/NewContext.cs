using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BudgetExtractor.Models
{
    public partial class NewContext : DbContext
    {

        public string path;

        public NewContext(string path)
        {
            this.path = path;
        }

        public NewContext(DbContextOptions<NewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BudgettableV1> BudgettableV1s { get; set; }
        public virtual DbSet<BudgetyearV1> BudgetyearV1s { get; set; }
        public virtual DbSet<CategoryV1> CategoryV1s { get; set; }
        public virtual DbSet<CheckingaccountV1> CheckingaccountV1s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"DataSource={path}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgettableV1>(entity =>
            {
                entity.HasKey(e => e.Budgetentryid);

                entity.ToTable("BUDGETTABLE_V1");

                entity.HasIndex(e => e.Budgetyearid, "IDX_BUDGETTABLE_BUDGETYEARID");

                entity.Property(e => e.Budgetentryid)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("BUDGETENTRYID");

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasColumnType("numeric")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Budgetyearid)
                    .HasColumnType("integer")
                    .HasColumnName("BUDGETYEARID");

                entity.Property(e => e.Categid)
                    .HasColumnType("integer")
                    .HasColumnName("CATEGID");

                entity.Property(e => e.Period)
                    .IsRequired()
                    .HasColumnName("PERIOD");

                entity.Property(e => e.Subcategid)
                    .HasColumnType("integer")
                    .HasColumnName("SUBCATEGID");
            });

            modelBuilder.Entity<BudgetyearV1>(entity =>
            {
                entity.HasKey(e => e.Budgetyearid);

                entity.ToTable("BUDGETYEAR_V1");

                entity.HasIndex(e => e.Budgetyearname, "IX_BUDGETYEAR_V1_BUDGETYEARNAME")
                    .IsUnique();

                entity.HasIndex(e => e.Budgetyearname, "IDX_BUDGETYEAR_BUDGETYEARNAME");

                entity.Property(e => e.Budgetyearid)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("BUDGETYEARID");

                entity.Property(e => e.Budgetyearname)
                    .IsRequired()
                    .HasColumnName("BUDGETYEARNAME");
            });

            modelBuilder.Entity<CategoryV1>(entity =>
            {
                entity.HasKey(e => e.Categid);

                entity.ToTable("CATEGORY_V1");

                entity.HasIndex(e => e.Categname, "IX_CATEGORY_V1_CATEGNAME")
                    .IsUnique();

                entity.HasIndex(e => e.Categname, "IDX_CATEGORY_CATEGNAME");

                entity.Property(e => e.Categid)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("CATEGID");

                entity.Property(e => e.Categname)
                    .IsRequired()
                    .HasColumnName("CATEGNAME");
            });

            modelBuilder.Entity<CheckingaccountV1>(entity =>
            {
                entity.HasKey(e => e.Transid);

                entity.ToTable("CHECKINGACCOUNT_V1");

                entity.HasIndex(e => new { e.Accountid, e.Toaccountid }, "IDX_CHECKINGACCOUNT_ACCOUNT");

                entity.HasIndex(e => e.Transdate, "IDX_CHECKINGACCOUNT_TRANSDATE");

                entity.Property(e => e.Transid)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("TRANSID");

                entity.Property(e => e.Accountid)
                    .HasColumnType("integer")
                    .HasColumnName("ACCOUNTID");

                entity.Property(e => e.Categid)
                    .HasColumnType("integer")
                    .HasColumnName("CATEGID");

                entity.Property(e => e.Followupid)
                    .HasColumnType("integer")
                    .HasColumnName("FOLLOWUPID");

                entity.Property(e => e.Notes).HasColumnName("NOTES");

                entity.Property(e => e.Payeeid)
                    .HasColumnType("integer")
                    .HasColumnName("PAYEEID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.Subcategid)
                    .HasColumnType("integer")
                    .HasColumnName("SUBCATEGID");

                entity.Property(e => e.Toaccountid)
                    .HasColumnType("integer")
                    .HasColumnName("TOACCOUNTID");

                entity.Property(e => e.Totransamount)
                    .HasColumnType("numeric")
                    .HasColumnName("TOTRANSAMOUNT");

                entity.Property(e => e.Transactionnumber).HasColumnName("TRANSACTIONNUMBER");

                entity.Property(e => e.Transamount)
                    .IsRequired()
                    .HasColumnType("numeric")
                    .HasColumnName("TRANSAMOUNT");

                entity.Property(e => e.Transcode)
                    .IsRequired()
                    .HasColumnName("TRANSCODE");

                entity.Property(e => e.Transdate).HasColumnName("TRANSDATE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
