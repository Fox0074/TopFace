using UnityEngine;
using System.Collections;
using Zenject;
using FizerFox.Meta;

namespace FizerFox.Meta
{
	public class LikeSongCommand : ICommand<SongId>
	{
		[Inject] private LobbyData _lobbyData;
		[Inject] private SignalBus _signalBus;

		public void Execute(SongId id)
		{
			SongData songData = _lobbyData.GetSongData(id);
			songData.IsLiked = !songData.IsLiked;
			_signalBus.Fire(new LikeSongSignal { Data = songData });
		}
	}
}