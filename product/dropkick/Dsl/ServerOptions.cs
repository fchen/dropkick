namespace dropkick.Dsl
{
    public class ServerOptions
    {
        public ServerOptions(string name, Part part)
        {
            Name = name;
            Part = part;
        }

        public string Name { get; set; }
        public Part Part { get; set; }
    }
}