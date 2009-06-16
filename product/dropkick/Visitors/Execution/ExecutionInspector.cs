namespace dropkick.Visitors.Execution
{
    using Dsl;
    using Magnum.Reflection;

    public class ExecutionInspector :
        ReflectiveVisitorBase<ExecutionInspector>,
        DeploymentInspector
    {
        public ExecutionInspector() :
            base("Inspect")
        {
        }

        #region DeploymentInspector Members

        public void Inspect(object obj)
        {
            base.Visit(obj);
        }

        public void Inspect(object obj, ExposeMoreInspectionSites additionalInspections)
        {
            base.Visit(obj, () =>
                {
                    additionalInspections();
                    return true;
                });
        }

        #endregion

        public bool Inspect(Task task)
        {
            if (task.VerifyCanRun() == null)
                return false; // is this the correct behavior? I want to stop.
            //report verify failure

            //execute/dryrun
            task.Execute();

            return true;
        }
    }
}