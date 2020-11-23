using System;
using UnityEngine;
using Zenject;

namespace FizerFox.Game
{
    public class MonoGameInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings _settings;

        public override void InstallBindings()
        {
            Container.BindFactory<Type, BaseWindow, BaseWindow.Factory>()
                .FromIFactory(binding => binding.To<WindowFactory>().AsCached().WithArguments(_settings.WindowPrefabs));
        }

        [Serializable]
        public class Settings
        {
            [Header("Windows")]
            public BaseWindow[] WindowPrefabs;
        }
    }
}