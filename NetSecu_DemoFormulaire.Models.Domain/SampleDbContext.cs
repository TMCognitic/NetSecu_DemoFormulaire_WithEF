using Microsoft.EntityFrameworkCore;
using NetSecu_DemoFormulaire.Models.Domain.Configurations;
using NetSecu_DemoFormulaire.Models.Entities;

namespace NetSecu_DemoFormulaire.Models.Domain
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get { return Set<Utilisateur>(); } }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoFormulaire;Integrated Security=True;Encrypt=False");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UtilisateurConfig());
        }
    }
}