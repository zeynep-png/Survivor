using Microsoft.EntityFrameworkCore;
using Survivor.Entites;

namespace Survivor.Data
{
    public class SurvivorDbContext :DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CompetitorEntity> Competitors { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=ZEYNEP\SQLEXPRESS; database=SurvivorDb; Trusted_Connection=true; TrustServerCertificate=true");


            
            base.OnConfiguring(optionsBuilder);



        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompetitorEntity>()
                        .HasOne(c => c.Category)
                        .WithMany(c => c.Competitors)
                        .HasForeignKey(c => c.CategoryId);
        }
    }
}
