using UnityEngine;

namespace FizerFox.Meta
{
	public class SongData
	{
		public SongId Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public int CurrentPoints { get; set; }
		public int Progress { get; set; }
		public int Stage { get; set; }
		public bool IsLiked { get; set; }
		public SongDifficulty Difficulty { get; set; }
		public AudioClip AudioPreview { get; set; }

		public SongData Clone()
		{
			return new SongData
			{
				Id = this.Id,
				Title = this.Title,
				Author = this.Author,
				CurrentPoints = this.CurrentPoints,
				Progress = this.Progress,
				Stage = this.Stage,
				IsLiked = this.IsLiked,
				Difficulty = this.Difficulty,
			};
		}
	}
}