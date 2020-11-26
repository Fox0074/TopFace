using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FizerFox.Meta
{
	public class ScrollStruct // need rename
	{
		public SongTabView tab;
		public SongScrollController scroll;

		public ScrollStruct(SongTabView tab, SongScrollController scroll)
		{
			this.tab = tab;
			this.scroll = scroll;
		}
	}

	// need refactoring
	public class SongScrollsManager : MonoBehaviour
	{
		public enum ScrollType // что-нибудь придумать с этим в будущем
		{
			AllSongs,
			Liked,
			Stage1,
			Stage2,
		}

		[SerializeField] private SongScrollsFactory _scrollsFactory;

		[SerializeField] private GameObject _tabParent;
		[SerializeField] private GameObject _scrollParent;

		[SerializeField] private SongTabView _tabPrefab;

		private List<SongScrollModel> _songViews = new List<SongScrollModel>(); // список всех песен TODO в будущем переделать
		private List<ScrollStruct> _scrolls = new List<ScrollStruct>();

		private int _currentIndex = 0;

		private void Start()
		{
			Init();
		}

		private void InitSongs()
		{
			_songViews.Add(new SongScrollModel
			{
				Title = "Baby Shark",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 0,
				Difficulty = SongDifficulty.Easy,
				AudioPreview = Resources.Load<AudioClip>("BabyShark")
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 1,
				Difficulty = SongDifficulty.Easy,
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title2",
				Author = "Author2",
				CurrentPoints = 3,
				Progress = 1,
				Stage = 0,
				Difficulty = SongDifficulty.Hard,
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "Baby Shark",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 0,
				Difficulty = SongDifficulty.Easy,
				AudioPreview = Resources.Load<AudioClip>("BabyShark")
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 1,
				Difficulty = SongDifficulty.Easy,
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title2",
				Author = "Author2",
				CurrentPoints = 3,
				Progress = 1,
				Stage = 0,
				Difficulty = SongDifficulty.Hard,
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "Baby Shark",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 0,
				Difficulty = SongDifficulty.Easy,
				AudioPreview = Resources.Load<AudioClip>("BabyShark")
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 1,
				Difficulty = SongDifficulty.Easy,
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title2",
				Author = "Author2",
				CurrentPoints = 3,
				Progress = 1,
				Stage = 0,
				Difficulty = SongDifficulty.Hard,
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "Baby Shark",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 0,
				Difficulty = SongDifficulty.Easy,
				AudioPreview = Resources.Load<AudioClip>("BabyShark")
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title",
				Author = "Author",
				CurrentPoints = 0,
				Progress = 0,
				Stage = 1,
				Difficulty = SongDifficulty.Easy,
			});

			_songViews.Add(new SongScrollModel
			{
				Title = "I'm song title2",
				Author = "Author2",
				CurrentPoints = 3,
				Progress = 1,
				Stage = 0,
				Difficulty = SongDifficulty.Hard,
			});
		}

		private void Init()
		{
			InitSongs();

			foreach (ScrollType type in System.Enum.GetValues(typeof(ScrollType)))
				AddTape(type);

			_scrolls[(int) ScrollType.Liked].scroll.EnableCache();

			SelectIndex(0);

			foreach (var scroll in _scrolls)
				foreach(var model in _songViews)
					scroll.scroll.TryAddSong(model);
		}

		private void AddTape(ScrollType type) // TODO factory!!
		{
			_scrolls.Add(new ScrollStruct(AddTab(type, _scrolls.Count), AddScroll(type)));
		}

		private SongScrollController AddScroll(ScrollType type)
		{
			SongScrollController scroll = _scrollsFactory.Create(type, _scrollParent);
			scroll.transform.parent = _scrollParent.transform;
			return scroll;
		}

		private SongTabView AddTab(ScrollType type, int index) // TODO factory!!
		{
			var tab = Instantiate(_tabPrefab);
			tab.transform.parent = _tabParent.transform;
			tab.SetIndex(index);
			tab.SetText(type.ToString());
			tab.SetOnClick(OnSelect);
			return tab;
		}

		private void OnSelect(int index)
		{
			if (_currentIndex == index)
				return;

			SelectIndex(index);
		}

		private void SelectIndex(int index)
		{
			_scrolls[_currentIndex].scroll.Unselect(); // TODO callbacks for animation
			_scrolls[index].scroll.Select();
			_currentIndex = index;
		}

		private void OnLike()
		{
			
		}

		private void OnUnlike()
		{
			
		}
	}
}
