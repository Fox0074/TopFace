using UnityEngine;
using System.Collections;
using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollGroupView : MonoBehaviour
	{
		public void AddScroll(Transform transform)
		{
			transform.parent = this.transform;
		}

		public class Factory : PlaceholderFactory<ScrollData, SongScrollView>
		{ }
	}
}
