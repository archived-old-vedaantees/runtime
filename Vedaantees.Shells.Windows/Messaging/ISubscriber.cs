namespace Vedaantees.Shells.Windows.Messaging
{
    public interface ISubscriber
    {

    }

    public interface ISubscriber<TMessage> : ISubscriber
    {
        void Execute(TMessage message);
    }
}