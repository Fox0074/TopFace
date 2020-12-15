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
		[SerializeField] private SongScrollView _songScrollViewLikedPrefab;
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
			Container.Bind<LikeSongCommand>().AsSingle();

			//signals
			Container.DeclareSignal<SelectSongScrollSignal>();
			Container.DeclareSignal<LikeSongSignal>();
			Container.DeclareSignal<AddScrollSignal>();
			Container.DeclareSignal<AddSongSignal>();
			Container.DeclareSignal<PreviewSongPlaySignal>();

			// factories
			Container.BindFactory<ScrollData, SongTabView, SongTabView.Factory>()
				.FromComponentInNewPrefab(_songTabViewPrefab);

			Container.BindFactory<ScrollData, SongScrollView, SongScrollView.DefaultFactory>()
				.FromComponentInNewPrefab(_songScrollViewPrefab);

			Container.BindFactory<ScrollData, SongScrollView, SongScrollView.LikedFactory>()
				.FromComponentInNewPrefab(_songScrollViewLikedPrefab);

			Container.BindFactory<SongData, SongView, SongView.Factory>()
				.FromComponentInNewPrefab(_songViewPrefab);
		}
	}
}
