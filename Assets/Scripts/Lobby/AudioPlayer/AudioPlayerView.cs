using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Lobby.AudioPlayer
{
	public class AudioPlayerView : MonoBehaviour
	{
		[SerializeField] private AudioSource _source;

		private Coroutine currentSongCoroutine;

		public void PlaySong(AudioClip clip)
		{
			if (currentSongCoroutine != null)
				StopCoroutine(currentSongCoroutine);

			_source.DOKill();
			_source.volume = 0f;
			//_source.clip = clip; TODO
			_source.Play();

			_source.DOFade(0.1f, 1).OnComplete(() =>
			{
				currentSongCoroutine = StartCoroutine(PlaySong());
			});
		}

		private IEnumerator PlaySong()
		{
			yield return new WaitForSeconds(5); // TODO
			_source.DOFade(0, 1);
		}
	}
}