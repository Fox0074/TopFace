using UnityEngine;
using System.Collections;
using FizerFox.Meta;
using Zenject;
namespace FizerFox.Meta
{
	public class SongScrollView : MonoBehaviour
	{
		[SerializeField] private Transform _songViewParent;
		private int _index;

		public void Toggle(SelectSongScrollSignal signal)
		{
			gameObject.SetActive(_index == signal.TabIndex);
		}

		public void AddSong(Transform songView)
		{
			songView.parent = _songViewParent;
		}

		public class Factory : PlaceholderFactory<int, SongScrollView>
		{ }
	}
}
