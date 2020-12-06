using UnityEngine;
using System.Collections;

namespace FizerFox.Meta
{
	public class SongTabGroupView : MonoBehaviour
	{
		public void AddTab(Transform transform)
		{
			transform.parent = this.transform;
		}
	}
}
