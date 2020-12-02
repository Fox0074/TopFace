using Zenject;

namespace FizerFox.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SceneSignalKernelInstaller.Install(Container);

            // models
            Container.Bind<Game>().AsSingle();

            //commands
            Container.Bind<SwitchToGameCommand>().AsSingle();

            //signals
            Container.DeclareSignal<SwitshBackGroundSignal>();
            Container.BindSignal<SceneStartSignal>().ToMethod<GameSceneStartCommand>(x => x.Execute).From(x => x.AsCached());

        }
    }
}