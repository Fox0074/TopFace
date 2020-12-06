using System;

namespace FizerFox.Meta
{
	public class ScrollData
	{
		public int ScrollIndex { get; set; }
		public string TabName { get; set; }
		public Func<SongData, bool> SongsFilter { get; set; }
	}
}
