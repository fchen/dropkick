namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.CommandLine;

    public class CommandTestDeploy :
        Deployment<CommandTestDeploy>
    {
        public static Part Web { get; set; }

        static CommandTestDeploy()
        {
            Define(() =>
                   During(Web, (p) =>
                   {
                       p.OnServer(System.Environment.MachineName)
                           .CommandLine("ping").Args("www.google.com");
                       p.OnServer(System.Environment.MachineName)
                           .CommandLine("ping").Args("www.google.com")
                           .ExecutableIsLocatedAt(@"C:\bill");
                   })        
                );
        }
    }
}