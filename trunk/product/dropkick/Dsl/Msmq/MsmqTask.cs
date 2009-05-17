namespace dropkick.Dsl.Msmq
{
    using System;
    using System.Messaging;
    using Engine;

    public class MsmqTask :
        Task,
        DeploymentInspectorSite
    {
        string _queueName;
        string _serverName;
        bool _createIfNoExst;

        public string Name
        {
            get { return string.Format("MsmqTask for server '{0}' and private queue named '{1}'", _serverName, _queueName); }
        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();
            var path = string.Format(@"FormatName:DIRECT=OS:{0}\Private$\{1}", _serverName, _queueName);

            if(Environment.MachineName.Equals(_serverName))
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
                                              _queueName, _serverName, Environment.MachineName));
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
            set { _serverName = value.Equals(".") ? Environment.MachineName : value; }
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