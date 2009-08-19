namespace dropkick.Dsl.Msmq
{

    public interface QueueOptions
    {
        void CreateIfItDoesntExist();
    }

    public interface MsmqOptions
    {
        QueueOptions PrivateQueueNamed(string name);
    }
}