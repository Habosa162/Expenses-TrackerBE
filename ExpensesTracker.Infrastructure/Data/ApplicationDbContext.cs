using Expenses_Tracker;
using ExpensesTracker.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=ExpensesTracker;User ID=sa;Password=iti;Encrypt=False;TrustServerCertificate=True;");
        //}
           

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<AppUser>()
        //   .ToTable("AspNetUsers");
        //    modelBuilder.Entity<Category>(entity =>
        //    {
        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("ID");
        //        entity.Property(e => e.Name).HasMaxLength(50);
        //    });

        //    modelBuilder.Entity<Transaction>(entity =>
        //    {
        //        entity.HasNoKey();

        //        entity.Property(e => e.Amount).HasColumnType("decimal(10, 5)");
        //        entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
        //        entity.Property(e => e.Date).HasColumnType("datetime");
        //        entity.Property(e => e.Id).HasColumnName("ID");
        //        entity.Property(e => e.Note).HasMaxLength(100);
        //        entity.Property(e => e.TypeId).HasColumnName("TypeID");
        //        entity.Property(e => e.UserId)
        //            .HasMaxLength(450)
        //            .HasColumnName("UserID");

        //        entity.HasOne(d => d.Category).WithMany()
        //            .HasForeignKey(d => d.CategoryId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Transactions_Categories");

        //        entity.HasOne(d => d.Type).WithMany()
        //            .HasForeignKey(d => d.TypeId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Transactions_TransactionTypes");

        //        entity.HasOne(d => d.User).WithMany()
        //            .HasForeignKey(d => d.UserId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_Transactions_AspNetUsers");
        //    });

        //    modelBuilder.Entity<TransactionType>(entity =>
        //    {
        //        entity.Property(e => e.Id)
        //            .ValueGeneratedNever()
        //            .HasColumnName("ID");
        //        entity.Property(e => e.Type).HasMaxLength(50);
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
