using UnityEngine;
using System.Collections;
using Zenject;
using TMPro;
using UnityEngine.UI;

namespace FizerFox.Meta
{
	public class SongView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _titleText;
		[SerializeField] private TextMeshProUGUI _authorText;
		[SerializeField] private TextMeshProUGUI _currentPointsText;
		[SerializeField] private TextMeshProUGUI _progressText;
		[SerializeField] private TextMeshProUGUI _difficultyText;

		[SerializeField] private Button _likeButton;
		[SerializeField] private Button _previewButton;

		[Inject] private LikeSongCommand _likeSongCommand;

		[Inject] private SignalBus _signalBus; // TODO remove me
		[Inject] private LobbyData _lobbyData; //

		private SongId _id;

		[Inject]
		public void Construct(SongData data)
		{
			_id = data.Id;
			_titleText.text = data.Title;
			_authorText.text = data.Author;
			_currentPointsText.text = data.CurrentPoints.ToString();
			_progressText.text = data.Progress.ToString();
			//_stageText.text = data.Stage.ToString();
			_difficultyText.text = data.Difficulty.ToString();

			_likeButton.onClick.AddListener(OnLikeButton);
			_previewButton.onClick.AddListener(OnPreviewButton);
		}

		public void Dispose()
		{
			Destroy(gameObject); // TODO ?
		}

		private void OnLikeButton()
		{
			_likeSongCommand.Execute(_id);
		}

		private void OnPreviewButton()
		{
			_signalBus.Fire(new PreviewSongPlaySignal { Data = _lobbyData.GetSongData(_id) });
		}

		public class Factory : PlaceholderFactory<SongData, SongView>
		{ }
	}
}
