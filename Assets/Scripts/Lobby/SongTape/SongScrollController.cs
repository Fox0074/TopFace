using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Lobby.SongTape;

// TODO scrolls manager
public class SongScrollController : MonoBehaviour 
{
	private List<SongScrollView> _songViews = new List<SongScrollView>(); // TODO remove me?
	private SongScrollFactory _songsFactory;

	public void AddSong(SongScrollModel model)
	{
		SongScrollView view = _songsFactory.Create(model);
		_songViews.Add(view);
		view.transform.parent = transform;
	}

	private void Start()
	{
		_songsFactory = GetComponent<SongScrollFactory>();

		// REMOVE ME
		AddSong(new SongScrollModel
		{
			title = "I'm song title",
			author = "Author",
			currentPoints = 0,
			progress = 0,
			difficulty = Assets.Scripts.Models.Difficulty.Easy,
		});

		AddSong(new SongScrollModel
		{
			title = "I'm song title",
			author = "Author",
			currentPoints = 0,
			progress = 0,
			difficulty = Assets.Scripts.Models.Difficulty.Easy,
		});

		AddSong(new SongScrollModel
		{
			title = "I'm song title2",
			author = "Author2",
			currentPoints = 3,
			progress = 1,
			difficulty = Assets.Scripts.Models.Difficulty.Hard,
		});
	}
}
