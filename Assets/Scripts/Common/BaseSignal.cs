namespace FizerFox
{
    public class BaseSignal<T1>
    {
        public T1 FirstValue { get; private set; }

        public BaseSignal(T1 value)
        {
            FirstValue = value;
        }
    }

    public class BaseSignal<T1, T2>
    {
        public T1 FirstValue { get; private set; }
        public T2 SecondValue { get; private set; }

        public BaseSignal(T1 firstValue, T2 secondValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
        }
    }

    public class BaseSignal<T1, T2, T3>
    {
        public T1 FirstValue { get; private set; }
        public T2 SecondValue { get; private set; }
        public T3 ThridValue { get; private set; }

        public BaseSignal(T1 firstValue, T2 secondValue, T3 thridValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
            ThridValue = thridValue;
        }
    }

    public class BaseSignal<T1, T2, T3, T4>
    {
        public T1 FirstValue { get; private set; }
        public T2 SecondValue { get; private set; }
        public T3 ThridValue { get; private set; }
        public T4 FourValue { get; private set; }

        public BaseSignal(T1 firstValue, T2 secondValue, T3 thridValue, T4 fourValue)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
            ThridValue = thridValue;
            FourValue = fourValue;
        }
    }
}