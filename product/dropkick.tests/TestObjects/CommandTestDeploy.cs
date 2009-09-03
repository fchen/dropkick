namespace dropkick.tests.TestObjects
{
    using Configuration.Dsl;
    using Configuration.Dsl.CommandLine;

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
                           .CommandLine("ipconfig");
//                       p.OnServer(System.Environment.MachineName)
//                           .CommandLine("ping").Args("www.google.com")
//                           .ExecutableIsLocatedAt(@"C:\bill");
                   })        
                );
        }
    }
}