using Zenject;

namespace FizerFox.Meta
{
	public class InitializeScrollCommand : ICommand
	{
		[Inject] private SignalBus _signalBus;
		[Inject] private ScrollsData _scrollData;

		public void Execute()
		{
			_signalBus.Fire(new AddScrollSignal() { Data = new ScrollData() 
			{
				TabName = "All songs",
				ScrollIndex = _scrollData.MaxTabIndex++,
				SongsFilter = (data) => true
			}});
			_signalBus.Fire(new AddScrollSignal() { Data = new ScrollData() 
			{
				TabName = "Liked",
				ScrollIndex = _scrollData.MaxTabIndex++,
				SongsFilter = (data) => data.IsLiked
			}});
			_signalBus.Fire(new AddScrollSignal() { Data = new ScrollData() 
			{
				TabName = "Stage 1",
				ScrollIndex = _scrollData.MaxTabIndex++,
				SongsFilter = (data) => data.Stage == 1
			}});
			_signalBus.Fire(new AddScrollSignal() { Data = new ScrollData() 
			{
				TabName = "Stage 2",
				ScrollIndex = _scrollData.MaxTabIndex++,
				SongsFilter = (data) => data.Stage == 2
			}});
			_signalBus.Fire(new AddScrollSignal() { Data = new ScrollData() 
			{
				TabName = "Stage 3",
				ScrollIndex = _scrollData.MaxTabIndex++,
				SongsFilter = (data) => data.Stage == 3
			}});
		}
	}
}
