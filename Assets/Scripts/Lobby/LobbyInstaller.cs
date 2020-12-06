using UnityEngine;
using System.Collections;
using Zenject;
using FizerFox.Meta;

namespace FizerFox.Meta
{
	public class LobbyInstaller : MonoInstaller
	{
		[SerializeField] private SongTabView _songTabViewPrefab;

		public override void InstallBindings()
		{
			SceneSignalKernelInstaller.Install(Container);

			// models
			Container.Bind<LobbyData>().AsSingle();
			Container.Bind<ScrollData>().AsSingle();

			//commands
			Container.Bind<InitializeScrollCommand>().AsSingle();

			//signals
			Container.DeclareSignal<SelectSongScrollSignal>();
			Container.DeclareSignal<LikeSongSignal>();
			Container.DeclareSignal<AddScrollSignal>();

			// factories
			Container.BindFactory<string, SongTabView, SongTabView.Factory>()
				.FromComponentInNewPrefab(_songTabViewPrefab);
		}
	}
}
