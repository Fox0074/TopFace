using Assets.Scripts.Lobby.AudioPlayer;
using System.Collections;
using UnityEngine;
using Zenject;

namespace FizerFox.Meta
{
	public class AudioPlayerViewMediator : Mediator<AudioPlayerView>
	{
		[Inject] private SignalBus _signalBus;

		public override void OnRegister()
		{
			_signalBus.Subscribe<PreviewSongPlaySignal>(OnSignal);
		}

		public override void OnRemove()
		{
			_signalBus.Unsubscribe<PreviewSongPlaySignal>(OnSignal);
		}

		private void OnSignal(PreviewSongPlaySignal signal)
		{
			View.PlaySong(signal.Data.AudioPreview);
		}
	}
}