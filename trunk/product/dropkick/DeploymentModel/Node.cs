namespace dropkick.DeploymentModel
{
    using System;

    public class Node
    {
        TaskToExecute _chain;

        public void Nest(TaskToExecute task)
        {
            _chain += o =>
            {
                //nest start
                var a = task(o);
                //nest end
                return a;
            };
        }

        //list
    }

    public delegate DeploymentResult TaskToExecute(object somethingIn);
}