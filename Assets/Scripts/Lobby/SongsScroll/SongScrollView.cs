using UnityEngine;
using System.Collections;
using FizerFox.Meta;
using Zenject;
using System;

namespace FizerFox.Meta
{
	public class SongScrollView : MonoBehaviour
	{
		[SerializeField] private Transform _songViewParent;

		private int _scrollIndex = 0;
		private Func<SongData, bool> _songFilter;

		[Inject]
		public void Construct(ScrollData data)
		{
			_scrollIndex = data.ScrollIndex;
			_songFilter = data.SongsFilter;
		}

		public void Toggle(SelectSongScrollSignal signal) => gameObject.SetActive(_scrollIndex == signal.TabIndex);

		public bool CanAdd(SongData data) => _songFilter.Invoke(data);

		public void AddSong(SongView songView) => songView.transform.parent = _songViewParent;

		public class Factory : PlaceholderFactory<ScrollData, SongScrollView>
		{ }
	}
}
