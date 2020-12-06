using Zenject;

namespace FizerFox.Meta
{
	public class InitializeSongsCommand : ICommand
	{
		[Inject] private SignalBus _signalBus;

		[Inject] private LobbyData _lobbyData;

		public void Execute()
		{
			AddSong(new SongData
			{
				Id = 0,
				Title = "Title1",
				Author = "Author1",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 1,
				IsLiked = false,
				Difficulty = SongDifficulty.Easy,
			});

			AddSong(new SongData
			{
				Id = 1,
				Title = "Title2",
				Author = "Author2",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 1,
				IsLiked = false,
				Difficulty = SongDifficulty.Hard,
			});

			AddSong(new SongData
			{
				Id = 2,
				Title = "Title3",
				Author = "Author3",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 2,
				IsLiked = false,
				Difficulty = SongDifficulty.Medium,
			});

			AddSong(new SongData
			{
				Id = 3,
				Title = "Title4",
				Author = "Author4",
				CurrentPoints = 1,
				Progress = 1,
				Stage = 3,
				IsLiked = false,
				Difficulty = SongDifficulty.Easy,
			});
		}

		private void AddSong(SongData songData)
		{
			_lobbyData.SongDatas.Add(songData);
			_signalBus.Fire(new AddSongSignal { Data = songData });
		}

	}
}
