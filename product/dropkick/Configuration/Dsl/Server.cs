namespace dropkick.Configuration.Dsl
{
    public interface Server
    {
        string Name { get; }
        Role Role { get; }
        bool IsLocal { get; }
    }
    public class ServerI : Server
    {
        public ServerI(string name, Role role)
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