
using Zenject;

namespace FizerFox.Meta
{
	public class SongTabGroupViewMediator : Mediator<SongTabGroupView>
	{
		[Inject] SignalBus _signalBus;
		[Inject] SongTabView.Factory _factory;

		public override void OnRegister()
		{
			_signalBus.Subscribe<AddScrollSignal>((command) =>
			{
				View.AddTab(_factory.Create(command.TabName).transform);
			});
		}
	}
}
