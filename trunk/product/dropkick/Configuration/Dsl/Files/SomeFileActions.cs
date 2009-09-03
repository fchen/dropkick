namespace dropkick.Configuration.Dsl.Files
{
    public class SomeFileActions :
        FileActions
    {
        readonly PartCfg _part;

        public SomeFileActions(PartCfg part)
        {
            _part = part;
        }

        public FileAction WebConfig
        {
            get { return new WebFileActions(_part); }
        }

        public FileAction AppConfig
        {
            get { return new AppFileActions(_part); }
        }
    }
}