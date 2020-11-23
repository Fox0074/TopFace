using Zenject;

namespace FizerFox
{
    public class SceneSignalKernelInstaller : Installer<SceneSignalKernelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneSignalsKernel>().AsSingle();
            Container.BindExecutionOrder<SceneSignalsKernel>(100);

            Container.DeclareSignal<SceneStartSignal>();
        }
    }
}