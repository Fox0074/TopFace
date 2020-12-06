using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace FizerFox.Meta
{
    public class MetaGameViewMediator : Mediator<MetaGameView>
    {
        //[Inject]
        //private MetaGame _metaGame;

        //[Inject]
        //private Player _player;

        //[Inject]
        //private ApplicationSettings _settings;

        [Inject]
        private SignalBus _signalBus;

        private static int _lastCrownEventStage;
        private bool _sceneAppeared;

        public override void OnRegister()
        {
            _signalBus.Subscribe<SceneAppearSignal>(OnSceneAppear);
            View.Initialize();
        }

        public override void OnRemove()
        {
            _signalBus.Unsubscribe<SceneAppearSignal>(OnSceneAppear);
        }

        private void OnSceneAppear()
        {
            _sceneAppeared = true;

            if (TryShowGameWindows())
                return;
        }

        private bool TryShowGameWindows()
        {
            var windowsStack = GetGameWindows().ToArray();

            for (var i = 0; i < windowsStack.Length; i++)
            {
                var item = windowsStack[i];
                item.Value.Secondary = true;
                item.Value.Stacked = i < (windowsStack.Length - 1);
                item.Value.WaitForAnimations = true;
                _signalBus.Fire<PushWindowSignal>(new PushWindowSignal(item.Key, item.Value));
            }

            return windowsStack.Length > 0;
        }

        // WARN: не стоит тут использовать WindowOptions.Empty
        private IEnumerable<KeyValuePair<Type, WindowOptions>> GetGameWindows()
        {
            //    if (_player.DailyBonusExist())
            //    {
            //        //_player.TryBreakDailyBonus();

            //        yield return new KeyValuePair<Type, WindowOptions>(
            //            typeof(DailyBonusWindow),
            //            new DailyBonusWindowOptions()
            //            {
            //                DailyBonuses = _player.GetDailyBonuses(),
            //                CurrentDay = _player.DailyBonusCurrentDay
            //            }
            //        );
            //    }


            if (false)
            {
                yield return new KeyValuePair<Type, WindowOptions>();
            }
        }

        private void OnHardCurrencyLack()
        {

        }

        private void OnHealthLack()
        {

        }

        private void OnNetworkLack()
        {

        }

        private void OnLevelFetchingFinished()
        {

        }

        private void OnLevelFetchingFailed()
        {

        }
    }
}