using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsServiceProject9_12
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public string path = @"C:\Users\Cyberadmin\Desktop"; //put path here
        


        private void watch(string path)
        {



            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = path; 

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;

            fileSystemWatcher.Filter = "*.mp3; *.wav; *.png; *.jpg; *.mp4; *.avi; *.txt";

            //NOTE: add txt files in here too to make testing a lot easier


        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {
            StreamWriter OUTPUT = new StreamWriter(@"C:\Users\Cyberadmin\Desktop\\CHANGELOG.txt", true);
            OUTPUT.WriteLine("File created: {0}", e.Name);
            OUTPUT.Close();

        }

        private static void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)

        {

            StreamWriter OUTPUT = new StreamWriter(@"C:\Users\Cyberadmin\Desktop\\CHANGELOG.txt", true);
            OUTPUT.WriteLine("File renamed: {0}", e.Name);
            OUTPUT.Close();

        }

        private static void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)

        {

            StreamWriter OUTPUT = new StreamWriter(@"C:\Users\Cyberadmin\Desktop\\CHANGELOG.txt", true);
            OUTPUT.WriteLine("File deleted: {0}", e.Name);
            OUTPUT.Close();

        }





        protected override void OnStart(string[] args)
        {
            //do thing on start
            StreamWriter OUTPUT = new StreamWriter(@"C:\Users\Cyberadmin\Desktop\\CHANGELOG.txt", true);
            OUTPUT.WriteLine("Program Started:  " + DateTime.Now.ToString());
            OUTPUT.Close();
           // watch(@"C:\Users\Cyberadmin\Desktop");
        }

        protected override void OnStop()
        {
            //do thing before stop
            //should probably output to log

            StreamWriter OUTPUT = new StreamWriter(@"C:\Users\Cyberadmin\Desktop\\CHANGELOG.txt", true);
            OUTPUT.WriteLine("Program Ended:  " + DateTime.Now.ToString());
            OUTPUT.Close();

        }

        private void servTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //do thing every time the timer goes off
            watch(@"C:\Users\Cyberadmin\Desktop");
            
        }



        internal void TestStartandStop(string[] args)
        {
            // If started from Visual studio, run through the events.
            this.OnStart(args);
            
            //gotta run timer to test if everything works
            
            
              

            this.OnStop();
        }
    }
}
