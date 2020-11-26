using UnityEngine;
using System.Collections;

namespace FizerFox.Meta
{
	public class SongViewFactory : MonoBehaviour
	{
		[SerializeField] private SongView _songScrollViewPrefab;

		public SongView Create(SongScrollModel model)
		{
			var songTapeView = Instantiate(_songScrollViewPrefab);
			songTapeView.Id = model.Id;
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
