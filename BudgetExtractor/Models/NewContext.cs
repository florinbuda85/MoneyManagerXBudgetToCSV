using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BudgetExtractor.Models
{
    public partial class NewContext : DbContext
    {
        private readonly string databasePath;

        public NewContext(string databasePath)
        {
            this.databasePath = databasePath;
        }

        public NewContext(DbContextOptions<NewContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountlistV1> AccountlistV1s { get; set; }
        public virtual DbSet<BudgettableV1> BudgettableV1s { get; set; }
        public virtual DbSet<BudgetyearV1> BudgetyearV1s { get; set; }
        public virtual DbSet<CategoryV1> CategoryV1s { get; set; }
        public virtual DbSet<CheckingaccountV1> CheckingaccountV1s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"DataSource={databasePath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountlistV1>(entity =>
            {
                entity.HasKey(e => e.Accountid);

                entity.ToTable("ACCOUNTLIST_V1");

                entity.HasIndex(e => e.Accountname, "IX_ACCOUNTLIST_V1_ACCOUNTNAME")
                    .IsUnique();

                entity.HasIndex(e => e.Accounttype, "IDX_ACCOUNTLIST_ACCOUNTTYPE");

                entity.Property(e => e.Accountid)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("ACCOUNTID");

                entity.Property(e => e.Accessinfo).HasColumnName("ACCESSINFO");

                entity.Property(e => e.Accountname)
                    .IsRequired()
                    .HasColumnName("ACCOUNTNAME");

                entity.Property(e => e.Accountnum).HasColumnName("ACCOUNTNUM");

                entity.Property(e => e.Accounttype)
                    .IsRequired()
                    .HasColumnName("ACCOUNTTYPE");

                entity.Property(e => e.Contactinfo).HasColumnName("CONTACTINFO");

                entity.Property(e => e.Creditlimit)
                    .HasColumnType("numeric")
                    .HasColumnName("CREDITLIMIT");

                entity.Property(e => e.Currencyid)
                    .HasColumnType("integer")
                    .HasColumnName("CURRENCYID");

                entity.Property(e => e.Favoriteacct)
                    .IsRequired()
                    .HasColumnName("FAVORITEACCT");

                entity.Property(e => e.Heldat).HasColumnName("HELDAT");

                entity.Property(e => e.Initialbal)
                    .HasColumnType("numeric")
                    .HasColumnName("INITIALBAL");

                entity.Property(e => e.Interestrate)
                    .HasColumnType("numeric")
                    .HasColumnName("INTERESTRATE");

                entity.Property(e => e.Minimumbalance)
                    .HasColumnType("numeric")
                    .HasColumnName("MINIMUMBALANCE");

                entity.Property(e => e.Minimumpayment)
                    .HasColumnType("numeric")
                    .HasColumnName("MINIMUMPAYMENT");

                entity.Property(e => e.Notes).HasColumnName("NOTES");

                entity.Property(e => e.Paymentduedate)
                    .HasColumnType("text")
                    .HasColumnName("PAYMENTDUEDATE");

                entity.Property(e => e.Statementdate).HasColumnName("STATEMENTDATE");

                entity.Property(e => e.Statementlocked)
                    .HasColumnType("integer")
                    .HasColumnName("STATEMENTLOCKED");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("STATUS");

                entity.Property(e => e.Website).HasColumnName("WEBSITE");
            });

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
