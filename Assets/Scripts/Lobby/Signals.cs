using Zenject;

namespace FizerFox.Meta
{
	public class LikeSongSignal
	{ }

	public class SelectSongScrollSignal
	{
		public int TabIndex { get; set; }
	}

	public class AddScrollSignal
	{
		public string TabName { get; set; }
		public int TabIndex { get; set; }


		public AddScrollSignal()
		{
		}
	}
}