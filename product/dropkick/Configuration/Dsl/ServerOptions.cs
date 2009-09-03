namespace dropkick.Configuration.Dsl
{
    public class ServerOptions
    {
        public ServerOptions(string name, PartCfg part)
        {
            Name = name;
            Part = part;
        }

        public string Name { get; set; }
        public PartCfg Part { get; set; }
        public bool IsLocal
        {
            get
            {
                return System.Environment.MachineName == Name;
            }
        }
    }
}