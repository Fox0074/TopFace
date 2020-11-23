using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using Asyncoroutine;

namespace FizerFox
{
    public class SceneSignalsKernel : IInitializable
    {
        private SignalBus _signalBus;

        public SceneSignalsKernel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public async void Initialize()
        {
            _signalBus.Fire(new SceneStartSignal());
            await new WaitForEndOfFrame();
            if (SceneManager.sceneCount == 1)
                _signalBus.Fire(new SceneAppearSignal());
        }

    }
}
