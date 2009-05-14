namespace dropkick.Dsl.Visitor
{
    using System;
    using Engine;
    using Magnum.Reflection;

    public class ExecutionInspector :
        ReflectiveVisitorBase<ExecutionInspector>,
        DeploymentInspector
    {
        public ExecutionInspector() :
            base("Inspect")
        {
        }


        public void Inspect(object obj)
        {
            base.Visit(obj);
        }


        public void Inspect(object obj, Action additionalInspections)
        {
            base.Visit(obj, () =>
            {
                additionalInspections();
                return true;
            });
        }

        public bool Inspect(Task task)
        {
            if(task.VerifyCanRun() == null)
                return false; // is this the correct behavior? I want to stop.
            //report verify failure

            //execute/dryrun
            task.Execute();

            return true;
        }

    }
}