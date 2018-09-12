using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceProject9_12
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            // If this is being started in Visual Studio, run through the test routine.
            // Otherwise, run as a normal service.
            if (Environment.UserInteractive)
            {
                Service1 S1 = new Service1();
                S1.TestStartandStop(args);
                
               

                
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }
            
        }
    }
}
