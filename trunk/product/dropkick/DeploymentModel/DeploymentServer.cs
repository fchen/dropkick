namespace dropkick.DeploymentModel
{
    public class DeploymentServer
    {
        public DeploymentServer(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public bool IsLocal
        {
            get
            {
                return System.Environment.MachineName == Name;
            }
        }
    }
}