using UnityEngine;

namespace FizerFox.Meta
{
	public class SongScrollModel
	{
		public int Id { get; set; } // TODO Enum
		public string Title { get; set; }
		public string Author { get; set; }
		public int CurrentPoints { get; set; }
		public int Progress { get; set; }
		public int Stage { get; set; }
		public bool IsLiked { get; set; }
		public SongDifficulty Difficulty { get; set; }
		public AudioClip AudioPreview { get; set; }
	}
}
