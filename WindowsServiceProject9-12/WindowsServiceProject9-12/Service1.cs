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

//NOTE:
//this project is not finished. I am aware that my grade will reflect this. 
// but this is all the progress that I could complete between my scheduled work hours

//i have a fileobject class, and other inherited classes, though they arent used yet.

//it correctly monitors the desktop and raises flags for deletion, creation, and renaming correctly
//however after adding the filepaths to the queue, it doesn't actually do anything with the files yet. 

//i do not have a seperate file for the user to put in the directories to monitor, theyre currently held in variables



//TO DO:
//set paths to external file
//set up said directory -- DONE
//check if items in queue exist --DONE
//physically copy over the files through the queue --DONE....ISH.




namespace WindowsServiceProject9_12
{
    public partial class Service1 : ServiceBase
    {
        Queue<string> qt = new Queue<string>();
        Queue<string> dqt = new Queue<string>();


        public Service1()
        {
            InitializeComponent();
        }




        public string path = @"C:\Users\Cyberadmin\Desktop"; //put path here
        public string changelogpath = @"C:\Users\Cyberadmin\Desktop\CHANGELOG.txt"; //put path here
        public string backuppath = @"C:\Users\Cyberadmin\Desktop\TestBackup\Bees\ThisIsATest"; //put backup path here.








        private void watch(string path)
        {
           


            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = path; 

            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            fileSystemWatcher.EnableRaisingEvents = true;

            fileSystemWatcher.Filter = "*.*";

            //NOTE: add txt files in here too to make testing a lot easier
            


        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {

            qt.Enqueue(e.Name);
            //System.Threading.Thread.Sleep(500);



            

        }

        private void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)

        {

            qt.Enqueue(e.Name);
            


        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)

        {

            //System.Threading.Thread.Sleep(500);

            dqt.Enqueue(e.Name);


            //qt.Enqueue(e.Name); 


            //do nothing on delete
        }


        protected override void OnStart(string[] args)
        {
            //do thing on start

            //StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
            //OUTPUT.WriteLine("Program Started:  " + DateTime.Now.ToString());
            //OUTPUT.Close();

           

        }

        protected override void OnStop()
        {
            //do thing before stop
            //should probably output to log

            //StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
            //OUTPUT.WriteLine("Program Ended:  " + DateTime.Now.ToString());

            //OUTPUT.Close();

        }

        private void Backup()
        {
            


            int Q = 0;
            int D = 0;
            
           if (qt.Count > 0)
           {
                StreamWriter OUTPUT = new StreamWriter(changelogpath, true);
                while (Q <= qt.Count)
            {
                if (!Directory.Exists(backuppath))
                {
                    Directory.CreateDirectory(backuppath);
                }

                    OUTPUT.WriteLine("BACKED UP: " + qt.Peek());
                    
                    File.Copy((path + @"\" + qt.Peek()), (backuppath + @"\" + qt.Dequeue()), true);
                Q++;
    

            }

                if (dqt.Count > 0)
                {
                    while (D <= dqt.Count)
                    {
                        OUTPUT.WriteLine(dqt.Dequeue() + " was deleted");
                        D++;
                    }
                }
                OUTPUT.Close();
            }




        }


        private void servTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //do thing every time the timer goes off
            
            
            watch(path);



            Backup();

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
