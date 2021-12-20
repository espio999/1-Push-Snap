using System;
using System.Threading;
using System.Windows.Forms;

namespace OnePushSnap
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Mutex mtx = new Mutex(false, "one");

            /// Prevent application from multiplying
            if (mtx.WaitOne(0, false) == false)
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /// Run application without window
            //Application.Run(new Form1());

            new configuration_form();
            Application.Run();
        }
    }
}
