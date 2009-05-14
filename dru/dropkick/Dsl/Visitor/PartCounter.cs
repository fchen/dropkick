namespace dropkick.Dsl.Visitor
{
    using System;

    public class PartCounter :
        DeploymentInspector
    {
        int _count = 0;

        public void Inspect(object obj)
        {
            if(obj is Part)
                _count++;
        }

        public void Inspect(object obj, Action additionalInspections)
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