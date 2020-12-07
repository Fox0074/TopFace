using FizerFox;
using UnityEngine;
using Zenject;

namespace FizerFox.Meta
{
	public class SongScrollGroupViewMediator : Mediator<SongScrollGroupView>
	{
		[Inject] SignalBus _signalBus;
		[Inject] SongScrollView.DefaultFactory _defaultFactory;
		[Inject] SongScrollView.LikedFactory _likedFactory;

		public override void OnRegister()
		{
			_signalBus.Subscribe<AddScrollSignal>(AddScroll);
		}

		public override void OnRemove()
		{
			_signalBus.Unsubscribe<AddScrollSignal>(AddScroll);
		}

		private void AddScroll(AddScrollSignal signal)
		{
			switch (signal.Data.Type)
			{
				case ScrollType.Default:
					View.AddScroll(_defaultFactory.Create(signal.Data).transform);
					break;
				case ScrollType.Liked:
					View.AddScroll(_likedFactory.Create(signal.Data).transform);
					break;
			}

		}

	}
}
