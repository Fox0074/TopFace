using UnityEngine;
using System.Collections;
using FizerFox.Meta;
using Zenject;
using System;
using System.Collections.Generic;

namespace FizerFox.Meta
{
	public class SongScrollView : MonoBehaviour
	{
		[SerializeField] private Transform _songViewParent;

		private int _scrollIndex = 0;
		private Func<SongData, bool> _songFilter;

		private Dictionary<SongId, SongView> _views = new Dictionary<SongId, SongView>();

		[Inject]
		public void Construct(ScrollData data)
		{
			_scrollIndex = data.ScrollIndex;
			_songFilter = data.SongsFilter;
		}

		public void Toggle(SelectSongScrollSignal signal) => gameObject.SetActive(_scrollIndex == signal.TabIndex);

		public bool CanAdd(SongData data) => _songFilter.Invoke(data) && !_views.ContainsKey(data.Id);

		public void RemoveSong(SongId id)
		{
			if (!_views.ContainsKey(id))
				return;

			_views[id].Dispose();
			_views.Remove(id);
		}

		public void AddSong(SongId id, SongView songView)
		{
			_views.Add(id, songView);
			songView.transform.parent = _songViewParent;
		}

		public class DefaultFactory : PlaceholderFactory<ScrollData, SongScrollView>
		{ }

		public class LikedFactory : PlaceholderFactory<ScrollData, SongScrollView>
		{ }
	}
}
