using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CountryDashboard.Models
{
    public partial class CountryDashboardContext : DbContext
    {

        public CountryDashboardContext(DbContextOptions<CountryDashboardContext> options)
            : base(options)
        {
        }
        
        public CountryDashboardContext()
        {
        }
        

        public virtual DbSet<Countries> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=.;Password=@Admin123;User ID=sa;Initial Catalog=CountryDashboard;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}