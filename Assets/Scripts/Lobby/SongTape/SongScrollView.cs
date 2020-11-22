using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace FizreFox.Meta
{
	public class SongScrollView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _titleText;
		[SerializeField] private TextMeshProUGUI _authorText;
		[SerializeField] private TextMeshProUGUI _currentPoints;
		[SerializeField] private TextMeshProUGUI _starProgress;
		[SerializeField] private TextMeshProUGUI _difficultyText;
		[SerializeField] private Button _playButton;

		private AudioClip _audioPreview;

		public void SetTitle(string title) => _titleText.text = title;
		public void SetAuthor(string author) => _authorText.text = author;
		public void SetCurrentPoints(int points) => _currentPoints.text = points.ToString();
		public void SetProgress(int progress) => _starProgress.text = progress.ToString();
		public void SetDifficulty(SongDifficulty difficulty) => _difficultyText.text = difficulty.ToString();
		public void SetAudioPreview(AudioClip preview) => _audioPreview = preview;
		private void OnPlayButtonClicked() => SceneManager.LoadScene("Game", LoadSceneMode.Single);

		private void Start()
		{
			_playButton.onClick.AddListener(OnPlayButtonClicked);
		}

	}
}