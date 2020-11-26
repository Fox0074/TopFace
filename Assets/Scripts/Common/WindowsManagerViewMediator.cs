using Zenject;

namespace FizerFox
{
    public class WindowsManagerViewMediator : Mediator<WindowsManagerView>
    {
        [Inject]
        private SignalBus _signalBus;

        public override void OnRegister()
        {
            _signalBus.Subscribe<PushWindowSignal>(View.PushWindow);
            _signalBus.Subscribe<PopWindowSignal>(View.PopWindow);

            View.Initialize();
        }

        public override void OnRemove()
        {
            _signalBus.Unsubscribe<PushWindowSignal>(View.PushWindow);
            _signalBus.Unsubscribe<PopWindowSignal>(View.PopWindow);
        }
    }
}
