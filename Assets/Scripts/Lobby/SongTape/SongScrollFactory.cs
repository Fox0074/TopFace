using UnityEngine;

namespace FizerFox.Meta
{
	public class SongScrollFactory : MonoBehaviour
	{
		[SerializeField] private SongScrollView _songScrollViewPrefab;

		public SongScrollView Create(SongScrollModel model)
		{
			var songTapeView = Instantiate(_songScrollViewPrefab);
			songTapeView.SetTitle(model.Title);
			songTapeView.SetAuthor(model.Author);
			songTapeView.SetCurrentPoints(model.CurrentPoints);
			songTapeView.SetProgress(model.Progress);
			songTapeView.SetDifficulty(model.Difficulty);
			songTapeView.SetAudioPreview(model.AudioPreview);
			return songTapeView;
		}
	}
}
