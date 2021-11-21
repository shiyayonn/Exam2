using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
using System.IO;

namespace Exam2
{

    public partial class Service1 : ServiceBase
    {
        
        public Service1()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart.");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");

        }
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            string fileName = e.Name;
            
            string sourcePath = @"C:\Folder1";
            string targetPath = @"C:\Folder2";

            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            eventLog1.WriteEntry($"Some file change occured: {destFile}");

            System.IO.File.Copy(sourceFile, destFile, true);

            eventLog1.WriteEntry($"Moved {e.Name} from Folder 1 to Folder 2");
            
        }
    }
}
