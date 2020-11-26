using UnityEngine;
using System.Collections;

namespace FizerFox.Meta
{
	public class SongScrollsFactory : MonoBehaviour
	{
		[SerializeField] private SongScrollController _scrollPrefab;

		public SongScrollController Create(SongScrollsManager.ScrollType type, GameObject parent)
		{
			var scroll = Instantiate(_scrollPrefab, parent.transform);
			scroll.Unselect();

			switch (type)
			{
				case SongScrollsManager.ScrollType.AllSongs:
					scroll.SetFilter((SongScrollModel model) => true);
					break;
				case SongScrollsManager.ScrollType.Liked:
					scroll.SetFilter((SongScrollModel model) => model.IsLiked);
					break;
				case SongScrollsManager.ScrollType.Stage1:
					scroll.SetFilter((SongScrollModel model) => model.Stage == 0);
					break;
				case SongScrollsManager.ScrollType.Stage2:
					scroll.SetFilter((SongScrollModel model) => model.Stage == 1);
					break;
			}

			return scroll;
		}
	}
}
