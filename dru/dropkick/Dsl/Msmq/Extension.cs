namespace dropkick.Dsl.Msmq
{
    using System;
    using System.Messaging;
    using Engine;

    public static class Extension
    {
        public static MsmqOptions Msmq(this ServerOptions serverName)
        {
            return new MsmqTaskCfg(serverName);
        }
    }

    public class MsmqTaskCfg :
        MsmqOptions,
        QueueOptions
    {
        readonly ServerOptions _options;
        private bool _createIfDoesntExist;
        private MsmqTask _task;

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

    public interface MsmqOptions
    {
        QueueOptions PrivateQueueNamed(string name);
    }

    public interface QueueOptions
    {
        void CreateIfItDoesntExist();
    }

    public class MsmqTask :
        Task,
        DeploymentInspectorSite
    {
        private string _queueName;
        private string _serverName;
        private bool _createIfNoExst;

        public string Name
        {
            get { return string.Format("MsmqTask for server '{0}' and private queue named '{1}'", _serverName, _queueName); }
        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();
            var path = string.Format(@"FormatName:DIRECT=OS:{0}\Private$\{1}", _serverName,_queueName);

            if (System.Environment.MachineName.Equals(_serverName))
            {
                if(MessageQueue.Exists(path))
                {
                    result.AddGood("'{0}' does exist");
                }
                else
                {
                    result.AddAlert(string.Format("'{0}' doesn't exist and will be created", _queueName));
                }
            }
            else
            {
                result.AddError(string.Format("Cannot check for queue '{0}' on server '{1}' while on server '{2}'",
                                              _queueName, _serverName, System.Environment.MachineName));
            }
            

            return result;
        }

        public string QueueName
        {
            get { return _queueName; }
            set { _queueName = value; }
        }

        public string ServerName
        {
            get { return _serverName; }
            set
            {
                _serverName = value.Equals(".") ? Environment.MachineName : value;
            }
        }

        public void Execute()
        {
            Console.WriteLine("did nothing");
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public void CreateIfItDoesNotExist()
        {
            _createIfNoExst = true;
        }
    }
}