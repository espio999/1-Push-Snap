﻿using System;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /// Run application without window
            //Application.Run(new Form1());

            new configuration_form();
            Application.Run();
        }
    }
}
