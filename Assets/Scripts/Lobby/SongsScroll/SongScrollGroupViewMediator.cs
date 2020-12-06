using FizerFox;
using UnityEngine;
using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollGroupViewMediator : Mediator<SongScrollGroupView>
	{
		[Inject] SignalBus _signalBus;
		[Inject] SongScrollView.Factory _factory;

		public override void OnRegister()
		{
			_signalBus.Subscribe<AddScrollSignal>((command) =>
			{
				View.AddScroll(_factory.Create(command.Data).transform);
			});
		}

	}
}
