using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace FizerFox.Meta
{
	public class SongTabView : MonoBehaviour
	{
		private int _tabIndex = 0;
		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _text;

		[Inject] private SignalBus _signalBus;
		[Inject] private ScrollData _scrollData;

		[Inject]
		public void Construct(string text)
		{
			_text.text = text;
			_tabIndex = _scrollData.MaxTabIndex;
			_button.onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			_signalBus.Fire(new SelectSongScrollSignal { TabIndex = _tabIndex });
		}

		public class Factory : PlaceholderFactory<string, SongTabView>
		{ }
	}
}
