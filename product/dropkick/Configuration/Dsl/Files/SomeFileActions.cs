namespace dropkick.Configuration.Dsl.Files
{
    public class SomeFileActions :
        FileActions
    {
        readonly RoleCfg _role;

        public SomeFileActions(RoleCfg role)
        {
            _role = role;
        }

        public FileAction WebConfig
        {
            get { return new WebFileActions(_role); }
        }

        public FileAction AppConfig
        {
            get { return new AppFileActions(_role); }
        }
    }
}