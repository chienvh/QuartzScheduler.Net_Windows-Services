using System.ServiceProcess;

namespace QuartzScheduler_Windows_Services.Net
{
    class Program
    {
        static void Main()
        {
            RunAsService();
        }

        static void RunAsService()
        {
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[] { new ChienService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}
