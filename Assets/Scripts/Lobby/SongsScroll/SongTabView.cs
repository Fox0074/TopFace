using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace FizerFox.Meta
{
	public class SongTabView : MonoBehaviour
	{
		[SerializeField] private Button _button;
		[SerializeField] private TextMeshProUGUI _text;

		[Inject] private SignalBus _signalBus;

		private int _scrollIndex = 0;

		[Inject]
		public void Construct(ScrollData data)
		{
			_text.text = data.TabName;
			_scrollIndex = data.ScrollIndex;
			_button.onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			_signalBus.Fire(new SelectSongScrollSignal { TabIndex = _scrollIndex });
		}

		public class Factory : PlaceholderFactory<ScrollData, SongTabView>
		{ }
	}
}
