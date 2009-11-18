namespace dropkick.Configuration.Dsl
{
    public class ServerOptions
    {
        public ServerOptions(string name, RoleCfg role)
        {
            Name = name;
            Role = role;
        }

        public string Name { get; set; }
        public RoleCfg Role { get; set; }
        public bool IsLocal
        {
            get
            {
                return System.Environment.MachineName == Name;
            }
        }
    }
}