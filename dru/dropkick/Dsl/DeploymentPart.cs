namespace dropkick.Dsl
{
    using System;

    public class DeploymentPart<T> 
        where T : Deployment<T>
    {
        public DeploymentPart(Part part)
		{
			Part = part;
		}

        public Part Part { get; set; }
    }
}