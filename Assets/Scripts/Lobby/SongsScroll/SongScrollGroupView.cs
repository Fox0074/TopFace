using UnityEngine;
using System.Collections;
using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollGroupView : MonoBehaviour
	{
		public void AddScroll(SongScrollView view)
		{
			view.transform.parent = transform;
			view.AfterAddOnScene();
		}

		public class Factory : PlaceholderFactory<ScrollData, SongScrollView>
		{ }
	}
}
