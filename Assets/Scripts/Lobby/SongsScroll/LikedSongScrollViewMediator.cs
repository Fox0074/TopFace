using UnityEngine;
using System.Collections;

namespace FizerFox.Meta
{
	public class LikedSongScrollViewMediator : SongScrollViewMediator
	{
		public override void OnRegister()
		{
			base.OnRegister();

			_signalBus.Subscribe<LikeSongSignal>(OnSongLike);
		}

		public override void OnRemove()
		{
			base.OnRemove();

			_signalBus.Unsubscribe<LikeSongSignal>(OnSongLike);
		}

		private void OnSongLike(LikeSongSignal signal)
		{
			if (signal.Data.IsLiked)
			{
				if (!View.CanAdd(signal.Data))
					return;

				var songView = _factory.Create(signal.Data);
				View.AddSong(signal.Data.Id, songView);
			}
			else
				View.RemoveSong(signal.Data.Id);
		}
	}
}