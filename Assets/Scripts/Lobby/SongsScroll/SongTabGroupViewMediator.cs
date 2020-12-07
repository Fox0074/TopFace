
using Zenject;

namespace FizerFox.Meta
{
	public class SongTabGroupViewMediator : Mediator<SongTabGroupView>
	{
		[Inject] SignalBus _signalBus;
		[Inject] SongTabView.Factory _factory;

		public override void OnRegister()
		{
			_signalBus.Subscribe<AddScrollSignal>(CreateTab);
		}

		public override void OnRemove()
		{
			_signalBus.Unsubscribe<AddScrollSignal>(CreateTab);
		}

		private void CreateTab(AddScrollSignal signal)
		{
			View.AddTab(_factory.Create(signal.Data).transform);
		}
	}
}
