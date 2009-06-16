namespace dropkick.Dsl.Files
{
    using System;

    public class SomeFileActions :
        FileActions
    {
        readonly Part _part;

        public SomeFileActions(Part part)
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