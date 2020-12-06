using UnityEngine;
using System.Collections;
using Zenject;
using FizerFox;

namespace FizerFox.Meta
{
	public class SongTabViewMediator : Mediator<SongTabView>
	{
		[Inject]
		private SignalBus _signalBus;

		public override void OnRegister()
		{
			//View.Initialize();
		}

		public override void OnRemove()
		{
		}
	}
}