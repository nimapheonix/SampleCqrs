using Microsoft.EntityFrameworkCore;
using SampleCqrs.Domain.People;

namespace SmapleCqrs.Persistance
{
    public class SampleCqrsDbContext : DbContext
    {
        //
        static SampleCqrsDbContext() { }
        public SampleCqrsDbContext() { }
        public SampleCqrsDbContext(DbContextOptions<SampleCqrsDbContext> context) : base(context) { }
        //
        public DbSet<Person> People { get; set; }
        //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
        //
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //in case of working with migration 
                optionsBuilder.UseNpgsql("host=127.0.0.1;port=5432;database=SampleCqrsDb;user id=postgres;password=YOUR_PASSWORD;pooling=true;minimum Pool Size=1;Maximum Pool Size=20;");
            }
        }
        //
    }
}