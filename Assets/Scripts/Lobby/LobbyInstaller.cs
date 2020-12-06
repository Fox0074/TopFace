using UnityEngine;
using System.Collections;
using Zenject;
using FizerFox.Meta;

namespace FizerFox.Meta
{
	public class LobbyInstaller : MonoInstaller
	{
		[SerializeField] private SongTabView _songTabViewPrefab;
		[SerializeField] private SongScrollView _songScrollViewPrefab;
		[SerializeField] private SongView _songViewPrefab;

		public override void InstallBindings()
		{
			SceneSignalKernelInstaller.Install(Container);

			// models
			Container.Bind<LobbyData>().AsSingle();
			Container.Bind<ScrollsData>().AsSingle();

			//commands
			Container.Bind<InitializeScrollCommand>().AsSingle();
			Container.Bind<InitializeSongsCommand>().AsSingle();

			//signals
			Container.DeclareSignal<SelectSongScrollSignal>();
			Container.DeclareSignal<LikeSongSignal>();
			Container.DeclareSignal<AddScrollSignal>();
			Container.DeclareSignal<AddSongSignal>();

			// factories
			Container.BindFactory<ScrollData, SongTabView, SongTabView.Factory>()
				.FromComponentInNewPrefab(_songTabViewPrefab);

			Container.BindFactory<ScrollData, SongScrollView, SongScrollView.Factory>()
				.FromComponentInNewPrefab(_songScrollViewPrefab);

			Container.BindFactory<SongData, SongView, SongView.Factory>()
				.FromComponentInNewPrefab(_songViewPrefab);
		}
	}
}
