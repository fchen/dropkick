namespace dropkick.tests.TestObjects
{
    using dropkick.Configuration.Dsl;

    public class PartCounter :
        DeploymentInspector
    {
        int _count;

        public int Count
        {
            get { return _count; }
        }

        #region DeploymentInspector Members

        public void Inspect(object obj)
        {
            if (obj is Part)
                _count++;
        }

        public void Inspect(object obj, ExposeMoreInspectionSites additionalInspections)
        {
            Inspect(obj);

            additionalInspections();
        }

        #endregion
    }
}