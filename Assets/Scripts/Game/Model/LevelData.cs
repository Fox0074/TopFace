using UnityEngine;

namespace FizerFox
{
    public class LevelData
    {
		public string Title { get; set; }
		public string Author { get; set; }
		public SongDifficulty Difficulty { get; set; }
		public AudioClip AudioClip { get; set; }
	}
}