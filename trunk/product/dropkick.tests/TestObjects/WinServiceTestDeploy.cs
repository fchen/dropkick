namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.WinService;

    public class WinServiceTestDeploy :
        Deployment<WinServiceTestDeploy>
    {
        public static Part Web { get; set; }

        static WinServiceTestDeploy()
        {
            Define(()=>
                       {
                           During(Web, p=> p.OnServer(System.Environment.MachineName)
                                               .WinService("MSMQ").Do(() =>
                                                                          {
                                                                              //stuff
                                                                          }));
                       });
        }
    }
}