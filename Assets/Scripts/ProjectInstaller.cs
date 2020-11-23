using UnityEngine;
using Zenject;

namespace FizerFox
{
    public class ProjectInstaller : MonoInstaller
    {
        private T LoadSettingsResource<T>(string path)
        {
            var textAsset = Resources.Load<TextAsset>(path);
            return JsonUtility.FromJson<T>(textAsset.text);
        }

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            //TODO: Load Confis

            // signals
            Container.DeclareSignal<SceneAppearSignal>();
            Container.DeclareSignal<PushWindowSignal>().MoveIntoAllSubContainers();
            Container.DeclareSignal<PopWindowSignal>().MoveIntoAllSubContainers();

            Container.DeclareSignal<PlayerLevelUpdateSignal>().MoveIntoAllSubContainers();


            // models

            // commands
        }
    }
}