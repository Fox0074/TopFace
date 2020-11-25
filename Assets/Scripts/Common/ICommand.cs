namespace FizerFox
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<TParam1>
    {
        void Execute(TParam1 arg);
    }

    public interface ICommand<TParam1, TParam2>
    {
        void Execute(TParam1 arg1, TParam2 arg2);
    }

    public interface ICommand<TParam1, TParam2, TParam3>
    {
        void Execute(TParam1 arg, TParam2 arg2, TParam3 arg3);
    }

    public interface ICommand<TParam1, TParam2, TParam3, TParam4>
    {
        void Execute(TParam1 arg, TParam2 arg2, TParam3 arg3, TParam4 arg4);
    }
}