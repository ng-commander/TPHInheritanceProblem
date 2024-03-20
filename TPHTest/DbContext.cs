using Microsoft.EntityFrameworkCore;

namespace TPHTest
{
    public class TestDbContext : DbContext
    {
        public DbSet<Parent> Parents => Set<Parent>();
        public DbSet<Message> Messages => Set<Message>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost,5433;Initial Catalog=LazyLoading;User Id=sa;Password=Pass@word;TrustServerCertificate=true"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Child>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Child>("Child")
                .HasValue<ChildDerived>("Derived");

            modelBuilder.Entity<ChildDerived>().OwnsOne(x => x.Misc);
        }
    }
}