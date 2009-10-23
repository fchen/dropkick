namespace dropkick.Configuration.Dsl.Msmq
{
    using Tasks.Msmq;

    public class MsmqTaskCfg :
        MsmqOptions,
        QueueOptions
    {
        readonly MsmqTask _task;

        //task
        public MsmqTaskCfg(ServerOptions options)
        {
            //set server name
            _task = new MsmqTask {ServerName = options.Name};
            options.Part.AddTask(_task);
        }

        public QueueOptions PrivateQueueNamed(string name)
        {
            _task.QueueName = name;
            return this;
        }

        public void CreateIfItDoesntExist()
        {
            _task.CreateIfItDoesNotExist();
        }
    }
}