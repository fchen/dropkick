namespace dropkick.Configuration.Dsl.Msmq
{
    using DeploymentModel;
    using Tasks.Msmq;

    public class MsmqTaskCfg :
        MsmqOptions,
        QueueOptions
    {
        readonly MsmqTask _task;

        //task
        public MsmqTaskCfg(DeploymentServer server)
        {
            //set server name
            _task = new MsmqTask {ServerName = server.Name};
            server.AddDetail(_task.ToDetail(server));
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