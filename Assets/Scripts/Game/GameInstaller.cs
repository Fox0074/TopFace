using Zenject;

namespace FizerFox.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SceneSignalKernelInstaller.Install(Container);

        }
    }
}