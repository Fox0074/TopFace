using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollViewMediator : Mediator<SongScrollView>
	{
		[Inject]
		private SignalBus _signalBus;

		[Inject]
		private SongView.Factory _factory;

		public override void OnRegister()
		{
			_signalBus.Subscribe<SelectSongScrollSignal>(View.Toggle);
			_signalBus.Subscribe<AddSongSignal>((signal) => 
			{
				if (!View.CanAdd(signal.Data))
					return;

				var songView = _factory.Create(signal.Data);
				View.AddSong(songView);
			});
		}

		public override void OnRemove()
		{
			_signalBus.Unsubscribe<SelectSongScrollSignal>(View.Toggle);
		}
	}
}
