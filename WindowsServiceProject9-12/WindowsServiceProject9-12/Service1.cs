using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net;//
using System.Net.Mail;//

namespace WindowsServiceProject9_12
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //do thing on start
            //notifies that the program has started
            var msg = new MailMessage("samstestapplication@gmail.com", "3525025340@vtext.com", "", "test onstart");
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("samstestapplication@gmail.com", "Fullmetal_69");
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }

        protected override void OnStop()
        {
            //do thing before stop
            //notifies that the program has ended
            var msg = new MailMessage("samstestapplication@gmail.com", "3525025340@vtext.com", "", "test onstop");
            msg.IsBodyHtml = true;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("samstestapplication@gmail.com", "Fullmetal_69");
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }

        internal void TestStartandStop(string[] args)
        {
            // If started from Visual studio, run through the events.
            this.OnStart(args);


            this.OnStop();
        }
    }
}
