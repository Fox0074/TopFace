using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollViewMediator : Mediator<SongScrollView>
	{
		[Inject]
		protected SignalBus _signalBus;

		[Inject]
		protected SongView.Factory _factory;

		public override void OnRegister()
		{
			_signalBus.Subscribe<SelectSongScrollSignal>(View.Toggle);
			_signalBus.Subscribe<AddSongSignal>(AddSong);
		}

		public override void OnRemove()
		{
			_signalBus.Unsubscribe<SelectSongScrollSignal>(View.Toggle);
			_signalBus.Unsubscribe<AddSongSignal>(AddSong);
		}

		protected void AddSong(AddSongSignal signal)
		{
			if (!View.CanAdd(signal.Data))
				return;

			var songView = _factory.Create(signal.Data);
			View.AddSong(signal.Data.Id, songView);
		}
	}
}
