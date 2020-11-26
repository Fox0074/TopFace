using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;

namespace FizerFox.Meta
{
	public class SongTabView : MonoBehaviour
	{
		private int _tabIndex = 0;
		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _text;

		public void SetIndex(int tabIndex) => _tabIndex = tabIndex;
		public void SetText(string text) => _text.text = text;
		public void SetOnClick(Action<int> onClick) => _button.onClick.AddListener(() => onClick.Invoke(_tabIndex));
	}
}
