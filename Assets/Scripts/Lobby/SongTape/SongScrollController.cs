using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FizerFox.Meta
{
	// TODO scrolls manager
	public class SongScrollController : MonoBehaviour
	{
		[SerializeField] private Transform _songViewParent;
		[SerializeField] private SongViewFactory _songsFactory;

		private List<SongView> _views = null;
		private bool _needCache = false;

		private Func<SongScrollModel, bool> _filter;

		public void EnableCache()
		{
			_needCache = true;
			_views = new List<SongView>();
		}

		public void Select()
		{
			gameObject.SetActive(true);
		}

		public void Unselect()
		{
			gameObject.SetActive(false);
		}

		public void SetFilter(Func<SongScrollModel, bool> filter)
		{
			_filter = filter;
		}

		public void TryAddSong(SongScrollModel model)
		{
			if (!CanAdd(model))
				return;

			var view = _songsFactory.Create(model);
			view.transform.parent = _songViewParent;

			if (_needCache)
				_views.Add(view);
		}

		public void RemoveSong(int id)
		{
			if (!_needCache)
				return;

			var selectedView = _views.FirstOrDefault((SongView view) => view.Id == id);

			if (selectedView != null)
				selectedView.transform.parent = null;
		}

		private bool CanAdd(SongScrollModel model) => _filter(model);
	}
}
