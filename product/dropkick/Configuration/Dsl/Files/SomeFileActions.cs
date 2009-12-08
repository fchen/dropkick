namespace dropkick.Configuration.Dsl.Files
{
    public class SomeFileActions :
        FileActions
    {
        readonly Role _role;

        public SomeFileActions(Role role)
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