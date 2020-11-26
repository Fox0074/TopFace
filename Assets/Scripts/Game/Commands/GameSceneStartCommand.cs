using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FizerFox.Game
{
    public class GameSceneStartCommand : ICommand
    {
        [Inject]
        private Game _game;

        [InjectOptional]
        private LevelData _sceneOptions;

        public void Execute()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            if (_sceneOptions == null)
                throw new Exception("Missing required scene arguments");

            if (_sceneOptions.AudioClip == null)
                throw new Exception("Missing required scene argument: AudioPreview");

            if (_sceneOptions.Title == null)
                throw new Exception("Missing required scene argument: Title");

            _game.Level = _sceneOptions;
        }
    }
}
