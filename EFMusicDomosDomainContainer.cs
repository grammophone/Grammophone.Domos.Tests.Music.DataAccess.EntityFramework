using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Grammophone.DataAccess;
using Grammophone.Domos.DataAccess.EntityFramework;
using Grammophone.Domos.Tests.Music.Domain;

namespace Grammophone.Domos.Tests.Music.DataAccess.EntityFramework
{
	/// <summary>
	/// EF6 music Domos test container.
	/// </summary>
	public class EFMusicDomosDomainContainer : EFWorkflowUsersDomainContainer<MusicUser, AlbumStateTransition>
	{
		public EFMusicDomosDomainContainer()
		{
		}

		public EFMusicDomosDomainContainer(string nameOrConnectionString)
			: base(nameOrConnectionString)
		{
		}

		public EFMusicDomosDomainContainer(string nameOrConnectionString, TransactionMode transactionMode)
			: base(nameOrConnectionString, transactionMode)
		{
		}

		public DbSet<RecordLabel> RecordLabels { get; set; }

		public DbSet<RecordLabelAdministrator> RecordLabelAdministrators { get; set; }

		public DbSet<RecordLabelContributor> RecordLabelContributors { get; set; }

		public DbSet<Artist> Artists { get; set; }

		public DbSet<Album> Albums { get; set; }

		public DbSet<Track> Tracks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<RecordLabel>().Property(l => l.Name).IsRequired().HasMaxLength(200);
			modelBuilder.Entity<RecordLabel>().HasIndex(l => l.Name).IsUnique();

			modelBuilder.Entity<RecordLabelDisposition>()
				.HasRequired(d => d.RecordLabel)
				.WithMany()
				.HasForeignKey(d => d.RecordLabelID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Artist>().Property(a => a.Name).IsRequired().HasMaxLength(200);
			modelBuilder.Entity<Artist>().HasRequired(a => a.RecordLabel).WithMany().HasForeignKey(a => a.RecordLabelID).WillCascadeOnDelete(false);
			modelBuilder.Entity<Artist>().HasIndex(a => new { a.RecordLabelID, a.Name }).IsUnique();

			modelBuilder.Entity<Album>().Property(a => a.Title).IsRequired().HasMaxLength(200);
			modelBuilder.Entity<Album>().HasRequired(a => a.RecordLabel).WithMany().HasForeignKey(a => a.RecordLabelID).WillCascadeOnDelete(false);
			modelBuilder.Entity<Album>().HasRequired(a => a.Artist).WithMany().HasForeignKey(a => a.ArtistID).WillCascadeOnDelete(false);
			modelBuilder.Entity<Album>().HasRequired(a => a.Owner).WithMany().HasForeignKey(a => a.OwnerID).WillCascadeOnDelete(false);
			modelBuilder.Entity<Album>().HasRequired(a => a.State).WithMany().HasForeignKey(a => a.StateID).WillCascadeOnDelete(false);

			modelBuilder.Entity<Track>().Property(t => t.Title).IsRequired().HasMaxLength(200);
			modelBuilder.Entity<Track>().HasRequired(t => t.RecordLabel).WithMany().HasForeignKey(t => t.RecordLabelID).WillCascadeOnDelete(false);
			modelBuilder.Entity<Track>().HasRequired(t => t.Album).WithMany(a => a.Tracks).HasForeignKey(t => t.AlbumID).WillCascadeOnDelete(false);
			modelBuilder.Entity<Track>().HasRequired(t => t.Owner).WithMany().HasForeignKey(t => t.OwnerID).WillCascadeOnDelete(false);

			modelBuilder.Entity<AlbumStateTransition>().HasRequired(st => st.Album).WithMany(a => a.StateTransitions).HasForeignKey(st => st.AlbumID).WillCascadeOnDelete(false);
			modelBuilder.Entity<AlbumStateTransition>().Ignore(st => st.FundsTransferEventID);
			modelBuilder.Entity<AlbumStateTransition>().Ignore(st => st.FundsTransferEvent);
		}
	}
}
