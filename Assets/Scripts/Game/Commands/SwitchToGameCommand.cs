using Asyncoroutine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace FizerFox.Game
{
    public class SwitchToGameCommand : ICommand<LevelData>
    {
        [Inject]
        private ZenjectSceneLoader _zenjectSceneLoader;

        [Inject]
        private SignalBus _signalBus;

        public void Execute(LevelData levelData)
        {
            SwitchScene("Game", levelData);
        }

        public async void SwitchScene(string sceneName, LevelData options)
        {
            if (SceneManager.sceneCount > 1)
            {
                Debug.LogError("Incorrect switch scene call");
                return;
            }

            var prevSceneName = SceneManager.GetActiveScene().name;

            await SceneManager.UnloadSceneAsync(prevSceneName);

            await _zenjectSceneLoader.LoadSceneAsync(sceneName, LoadSceneMode.Additive,
                container => { container.BindInstance(options); });

            _signalBus.Fire<SceneAppearSignal>();
        }
    }
}