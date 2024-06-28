using DynamicDashboardAspPostgres.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicDashboardAspPostgres.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Chart> Charts { get; set; }
        public DbSet<ChartData> ChartData { get; set; }

        public DbSet<DataSet> Dataset { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chart>()
                .HasOne(c => c.Data)
                .WithMany()
                .HasForeignKey(c => c.DataId);

            modelBuilder.Entity<ChartData>()
                .Property(cd => cd.Labels)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            modelBuilder.Entity<DataSet>()
                .Property(ds => ds.Data)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList());

            modelBuilder.Entity<DataSet>()
                .Property(ds => ds.BackgroundColor)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
