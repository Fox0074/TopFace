using Zenject;

namespace FizerFox.Game
{
    public class BackGroundViewMediator : Mediator<BackGroundView>
    {
        [Inject]
        private SignalBus _signalBus;

        public override void OnRegister()
        {
            _signalBus.Subscribe<SwitshBackGroundSignal>(View.SwitchBackGround);
        }

        public override void OnRemove()
        {
            _signalBus.Unsubscribe<SwitshBackGroundSignal>(View.SwitchBackGround);
        }

    }
}