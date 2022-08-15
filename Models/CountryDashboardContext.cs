using Microsoft.EntityFrameworkCore;

namespace CountryDashboard.Models
{
    public partial class CountryDashboardContext : DbContext
    {
        public CountryDashboardContext(DbContextOptions<CountryDashboardContext> options)
            : base(options)
        {
        }

        protected CountryDashboardContext()
        {
        }

        public virtual DbSet<Countries> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
