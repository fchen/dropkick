namespace dropkick.Configuration.Dsl.Msmq
{
    using Tasks.Msmq;

    public class MsmqTaskCfg :
        MsmqOptions,
        QueueOptions
    {
        readonly MsmqTask _task;

        //task
        public MsmqTaskCfg(Role role)
        {
            //set server name
            _task = new MsmqTask {ServerName = role.Name};
            role.AddTask(_task);
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