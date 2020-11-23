using System;

namespace FizerFox
{
    // CALLS
    public class LoadPlayerProfileSignal { }

    public class SavePlayerProfileSignal { }

    public class BuyStoreProductSignal : BaseSignal<string>
    {
        public BuyStoreProductSignal(string value) : base(value) { }
    }

    // EVENTS
    public class PushWindowSignal : BaseSignal<Type, WindowOptions>
    {
        public PushWindowSignal(Type firstValue, WindowOptions secondValue) : base(firstValue, secondValue) { }
    }

    public class PopWindowSignal : BaseSignal<Type>
    {
        public PopWindowSignal(Type value) : base(value) { }
    }
    public class PlayerLevelUpdateSignal : BaseSignal<int>
    {
        public PlayerLevelUpdateSignal(int value) : base(value) { }
    }
}
