using UnityEngine;

namespace Assets.Scripts.Lobby.SongTape
{
	public class SongScrollFactory : MonoBehaviour
	{
		[SerializeField] private SongScrollView _songScrollViewPrefab;

		public SongScrollView Create(SongScrollModel model)
		{
			SongScrollView songTapeView = Object.Instantiate(_songScrollViewPrefab);
			songTapeView.SetTitle(model.title);
			songTapeView.SetAuthor(model.author);
			songTapeView.SetCurrentPoints(model.currentPoints);
			songTapeView.SetProgress(model.progress);
			songTapeView.SetDifficulty(model.difficulty);
			songTapeView.SetAudioPreview(model.audioPreview);
			return songTapeView;
		}
	}
}
