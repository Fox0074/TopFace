using Zenject;

namespace FizerFox.Meta
{
	public class InitializeScrollCommand : ICommand
	{
		[Inject] private SignalBus _signalBus;

		public void Execute()
		{
			_signalBus.Fire(new AddScrollSignal() { TabName = "Test" }); // TODO не нравится что индексирование происходит здесь
			_signalBus.Fire(new AddScrollSignal() { TabName = "Test" });
			_signalBus.Fire(new AddScrollSignal() { TabName = "Test" });
			_signalBus.Fire(new AddScrollSignal() { TabName = "Test" });
		}
	}
}
