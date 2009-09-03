namespace dropkick.tests
{
    using Configuration.Dsl;

    public class PartCounter :
        DeploymentInspector
    {
        int _count;

        public void Inspect(object obj)
        {
            if(obj is Part)
                _count++;
        }

        public void Inspect(object obj, ExposeMoreInspectionSites additionalInspections)
        {
            Inspect(obj);

            additionalInspections();
        }

        public int Count
        {
            get { return _count; }
        }
    }
}