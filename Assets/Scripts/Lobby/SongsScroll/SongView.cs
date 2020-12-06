using UnityEngine;
using System.Collections;
using Zenject;
using TMPro;

namespace FizerFox.Meta
{
	public class SongView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _titleText;
		[SerializeField] private TextMeshProUGUI _authorText;

		public SongView(string title, string author)
		{
			_titleText.text = title;
			_authorText.text = author;
		}

		public class Factory : PlaceholderFactory<string, string, SongView>
		{ }
	}
}
