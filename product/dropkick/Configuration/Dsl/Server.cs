namespace dropkick.Configuration.Dsl
{
    public class Server
    {
        public Server(string name, Role role)
        {
            Name = name;
            Role = role;
        }

        public string Name { get; set; }
        public Role Role { get; set; }
        public bool IsLocal
        {
            get
            {
                return System.Environment.MachineName == Name;
            }
        }
    }
}