using UnityEngine;
using System.Collections;
using TMPro;
using Assets.Scripts.Models;

public class SongScrollView : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _titleText;
	[SerializeField] private TextMeshProUGUI _authorText;
	[SerializeField] private TextMeshProUGUI _currentPoints;
	[SerializeField] private TextMeshProUGUI _starProgress;
	[SerializeField] private TextMeshProUGUI difficultyText;

	private AudioClip audioPreview;

	public void SetTitle(string title) => _titleText.text = title;
	public void SetAuthor(string author) => _authorText.text = author;
	public void SetCurrentPoints(int points) => _currentPoints.text = points.ToString();
	public void SetProgress(byte progress) => _starProgress.text = progress.ToString();
	public void SetDifficulty(Difficulty difficulty) => difficultyText.text = difficulty.ToString();
	public void SetAudioPreview(AudioClip preview) => audioPreview = preview;
}
