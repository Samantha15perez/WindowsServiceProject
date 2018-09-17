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
using System.Collections;


//TO DO:
//set paths to external file
// grab items from queue and copy them to directory
//set up said directory
//check if items in queue exist
//physically copy over the files through the queue




namespace WindowsServiceProject9_12
{
    public partial class Service1 : ServiceBase
    {
        Queue<string> qt = new Queue<string>();

        public Service1()
        {
            InitializeComponent();
        }

        //put this in easily accessible external file later

        public string path = @"C:\Users\Public\Pictures\Sample Pictures"; //put path here
        public string  changelogpath = @"C:\Users\Public\Pictures"; //put path here
        public string backuppath = "put here";








        private void watch(string path)
        {

            

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = path; 

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;

            fileSystemWatcher.Filter = "*.jpg";

            //NOTE: add txt files in here too to make testing a lot easier


        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {
            StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
            OUTPUT.WriteLine("File created: {0}", e.Name + " " + DateTime.Now.ToString());
            OUTPUT.Close();


            qt.Enqueue(e.FullPath);




        }

        private void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)

        {

            StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
            OUTPUT.WriteLine("File renamed: {0}", e.Name + " " + DateTime.Now.ToString());
            OUTPUT.Close();

            qt.Enqueue(e.FullPath);

        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)

        {

            StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
            OUTPUT.WriteLine("File deleted: {0}", e.Name + " " + DateTime.Now.ToString());
            OUTPUT.Close();

            qt.Enqueue(e.FullPath); 
        }


        protected override void OnStart(string[] args)
        {
            //do thing on start
            StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
            OUTPUT.WriteLine("Program Started:  " + DateTime.Now.ToString());
            OUTPUT.Close();

            
        }

        protected override void OnStop()
        {
            //do thing before stop
            //should probably output to log

            StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
            OUTPUT.WriteLine("Program Ended:  " + DateTime.Now.ToString());
            
            foreach (string value in qt)
            {
                OUTPUT.WriteLine(value);
            }

            OUTPUT.Close();

        }

        private void servTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //do thing every time the timer goes off
            
            //StreamWriter OUTPUT = new StreamWriter(@"C:\Users\Cyberadmin\Desktop\\CHANGELOG.txt", true);
            //OUTPUT.WriteLine("Program Timer Enabled  " + DateTime.Now.ToString());
            //OUTPUT.Close();
            watch(path);

            //copy files to directory after timer ends


        }


        internal void TestStartandStop(string[] args)
        {
            // If started from Visual studio, run through the events.
            this.OnStart(args);

            while (servTimer.Enabled)
            {
                //help
            }          

            this.OnStop();
        }
    }
}
