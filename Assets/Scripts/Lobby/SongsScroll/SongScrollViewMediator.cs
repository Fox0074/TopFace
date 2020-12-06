using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollViewMediator : Mediator<SongScrollView>
	{
		[Inject]
		private SignalBus _signalBus;

		public override void OnRegister()
		{
			_signalBus.Subscribe<SelectSongScrollSignal>(View.Toggle);
		}

		public override void OnRemove()
		{
			_signalBus.Unsubscribe<SelectSongScrollSignal>(View.Toggle);
		}
	}
}
