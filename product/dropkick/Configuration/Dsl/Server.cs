namespace dropkick.Configuration.Dsl
{
    public interface Server
    {
        string Name { get; }
        Role Role { get; }
        bool IsLocal { get; }
    }
}