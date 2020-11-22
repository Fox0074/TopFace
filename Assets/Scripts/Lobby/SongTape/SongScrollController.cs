using UnityEngine;
using System.Collections.Generic;

namespace FizreFox.Meta
{
	// TODO scrolls manager
	public class SongScrollController : MonoBehaviour
	{
		[SerializeField]
		private Transform _songViewParent;

		private List<SongScrollView> _songViews = new List<SongScrollView>(); // TODO remove me?
		private SongScrollFactory _songsFactory;

		public void AddSong(SongScrollModel model)
		{
			var view = _songsFactory.Create(model);
			_songViews.Add(view);
			view.transform.parent = _songViewParent;
		}

		private void Start()
		{
			_songsFactory = GetComponent<SongScrollFactory>();

			// REMOVE ME
			AddSong(new SongScrollModel
			{
				Title = "I'm song title",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Difficulty = SongDifficulty.Easy,
			});

			AddSong(new SongScrollModel
			{
				Title = "I'm song title",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Difficulty = SongDifficulty.Easy,
			});

			AddSong(new SongScrollModel
			{
				Title = "I'm song title2",
				Author = "Author2",
				CurrentPoints = 3,
				Progress = 1,
				Difficulty = SongDifficulty.Hard,
			});
		}
	}
}
