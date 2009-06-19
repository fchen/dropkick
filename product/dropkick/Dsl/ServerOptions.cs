namespace dropkick.Dsl
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
    }
}