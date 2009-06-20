namespace dropkick.Dsl.Msmq
{
    using System;
    using System.Messaging;
    using System.Threading;
    using MassTransit.Transports.Msmq;
    using Verification;

    public class MsmqTask :
        Task
    {
        bool _createIfNoExst;
        string _queueName;
        string _serverName;

        public string QueueName
        {
            get { return _queueName; }
            set { _queueName = value; }
        }

        public string QueuePath
        {
            get { return new QueueAddress(string.Format("msmq://{0}/{1}", ServerName, QueueName)).FormatName; }
        }

        public string ServerName
        {
            get { return _serverName; }
            set { _serverName = value.Equals(".") ? Environment.MachineName : value; }
        }

        #region Task Members

        public string Name
        {
            get { return string.Format("MsmqTask for server '{0}' and private queue named '{1}'", _serverName, _queueName); }
        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            VerifyInAdministratorRole(result);

            if (Environment.MachineName.Equals(_serverName))
            {
                //if(MessageQueue.Exists(path))
                //{
                //    result.AddGood("'{0}' does exist");
                //}
                //else
                //{
                //    result.AddAlert(string.Format("'{0}' doesn't exist and will be created", _queueName));
                //}
                result.AddAlert("I can't check queue exstance yet");
            }
            else
            {
                result.AddAlert(string.Format("Cannot check for queue '{0}' on server '{1}' while on server '{2}'",
                                              _queueName, _serverName, Environment.MachineName));
            }


            return result;
        }

        public void Execute()
        {
            if (_serverName == Environment.MachineName)
            {
                MessageQueue.Create(QueuePath);
            }
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        #endregion

        void VerifyInAdministratorRole(VerificationResult result)
        {
            if (Thread.CurrentPrincipal.IsInRole("Administrator"))
            {
                result.AddAlert("You are not in the Administrator role");
            }
            else
            {
                result.AddGood("You are in the Administrator role");
            }
        }

        public void CreateIfItDoesNotExist()
        {
            _createIfNoExst = true;
        }
    }
}