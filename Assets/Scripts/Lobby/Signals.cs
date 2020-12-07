using Zenject;

namespace FizerFox.Meta
{
	public class LikeSongSignal
	{
		public SongData Data { get; set; }
	}

	public class SelectSongScrollSignal
	{
		public int TabIndex { get; set; }
	}

	public class AddScrollSignal
	{
		public ScrollData Data { get; set; }
	}

	public class AddSongSignal
	{
		public SongData Data { get; set; }
	}
}