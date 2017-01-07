using System.Data.Entity;

namespace FinanceManager.Entities.Context
{
    public class FinanceManagerContext : DbContext
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Outgoing> Outgoings { get; set; }
        public DbSet<TypeOfOutgoing> TypeOfOutgoings { get; set; }
        public DbSet<SourceOfAmount> SourceOfAmounts { get; set; }

        public FinanceManagerContext()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer<FinanceManagerContext>(new CreateDatabaseIfNotExists<FinanceManagerContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Income>().HasRequired(x => x.Source);
            modelBuilder.Entity<Outgoing>().HasRequired(x => x.Type);

            //modelBuilder.Entity<SourceOfAmount>()
            //    .HasMany(e => e.Incomes)
            //    .WithRequired(e => e.Source)
            //    .HasForeignKey(e => e.SourceId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<TypeOfOutgoing>()
            //    .HasMany(e => e.Outgoings)
            //    .WithRequired(e => e.Type)
            //    .HasForeignKey(e => e.TypeId)
            //    .WillCascadeOnDelete(false);
        }
    }
}