using bemtiviter.Model;
using Microsoft.EntityFrameworkCore;

namespace bemtiviter.Data
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postagem>().ToTable("tb_postagens");
            modelBuilder.Entity<Tema>().ToTable("tb_temas");
            modelBuilder.Entity<Usuario>().ToTable("tb_usuarios");

            //Relating Postagem -> Tema
            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.Tema)
                .WithMany()
                .HasForeignKey("TemaId")
                .OnDelete(DeleteBehavior.Cascade);

            // Relating Postagem -> Usuario
            modelBuilder.Entity<Postagem>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey("UsuarioId")
                .OnDelete(DeleteBehavior.Cascade);
        }

        //Entities 
        public DbSet<Postagem> Postagens { get; set; } = null!;
        public DbSet<Tema> Temas { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = this.ChangeTracker.Entries()
                                   .Where(x => x.State == EntityState.Added)
                                   .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                if (insertedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                       .Where(x => x.State == EntityState.Modified)
                       .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            { 
                if (modifiedEntry is Auditable auditableEntity)
                {
                    auditableEntity.Data = DateTimeOffset.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
