using Grammophone.DataAccess;
using Grammophone.DataAccess.EntityFramework;
using Grammophone.Domos.DataAccess.EntityFramework;
using Grammophone.Domos.Tests.Music.DataAccess;
using Grammophone.Domos.Tests.Music.Domain;

namespace Grammophone.Domos.Tests.Music.DataAccess.EntityFramework
{
	/// <summary>
	/// EF6 adapter for the music Domos test container.
	/// </summary>
	public class EFMusicDomosDomainContainerAdapter : EFWorkflowUsersDomainContainerAdapter<MusicUser, AlbumStateTransition, EFMusicDomosDomainContainer>, IMusicDomosDomainContainer
	{
		private IEntitySet<RecordLabel> recordLabels;
		private IEntitySet<RecordLabelAdministrator> recordLabelAdministrators;
		private IEntitySet<RecordLabelContributor> recordLabelContributors;
		private IEntitySet<Artist> artists;
		private IEntitySet<Album> albums;
		private IEntitySet<Track> tracks;

		public EFMusicDomosDomainContainerAdapter()
			: this(new EFMusicDomosDomainContainer("default"))
		{
		}

		public EFMusicDomosDomainContainerAdapter(EFMusicDomosDomainContainer innerContainer)
			: base(innerContainer)
		{
		}

		public IEntitySet<RecordLabel> RecordLabels => recordLabels ?? (recordLabels = new EFSet<RecordLabel>(this.InnerDomainContainer.RecordLabels, this));

		public IEntitySet<RecordLabelAdministrator> RecordLabelAdministrators => recordLabelAdministrators ?? (recordLabelAdministrators = new EFSet<RecordLabelAdministrator>(this.InnerDomainContainer.RecordLabelAdministrators, this));

		public IEntitySet<RecordLabelContributor> RecordLabelContributors => recordLabelContributors ?? (recordLabelContributors = new EFSet<RecordLabelContributor>(this.InnerDomainContainer.RecordLabelContributors, this));

		public IEntitySet<Artist> Artists => artists ?? (artists = new EFSet<Artist>(this.InnerDomainContainer.Artists, this));

		public IEntitySet<Album> Albums => albums ?? (albums = new EFSet<Album>(this.InnerDomainContainer.Albums, this));

		public IEntitySet<Track> Tracks => tracks ?? (tracks = new EFSet<Track>(this.InnerDomainContainer.Tracks, this));
	}
}
