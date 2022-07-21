using AlexandruMaries.Model;
using Microsoft.EntityFrameworkCore;

namespace AlexandruMaries.Data
{
	public class AlexandruMariesDbContext : DbContext
	{
		public AlexandruMariesDbContext(DbContextOptions<AlexandruMariesDbContext> options) : base(options)
		{
			
		}

		public virtual DbSet<Reference> References { get; set; }

		public virtual DbSet<User> Users { get; set; }

		public virtual DbSet<View> Views { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Reference>().ToTable(nameof(Reference));
			modelBuilder.Entity<User>().ToTable(nameof(User));
		}
	}
}