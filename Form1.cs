﻿using System;
using System.IO;
using System.Windows.Forms;

namespace OnePushSnap 
{
    public partial class configuration_form : Form
    {
        private static NotifyIcon n_ico = new NotifyIcon();
        private static KeyHook kh = new KeyHook();
        internal static Boolean working_flg = false;

        private static frmImageType single_instance;

        public static frmImageType callImageTypeForm
        {
            get {
                if (single_instance == null || single_instance.IsDisposed)
                {
                    single_instance = new frmImageType();
                }

                return single_instance;
            }
        }

        public configuration_form()
        {
            InitializeComponent();

            this.ShowInTaskbar = true;

            setDefaultConfiguration();
            makeContextMenu();
        }

        private void setDefaultConfiguration()
        {
            /// folder
            if (Directory.Exists(Properties.Settings.Default.save_folder) == false)
            {
                Properties.Settings.Default.save_folder = Environment.GetEnvironmentVariable(Properties.Settings.Default.initial_folder);
                Properties.Settings.Default.Save();
            }

            Properties.Settings.Default.default_folder = Environment.GetEnvironmentVariable(Properties.Settings.Default.initial_folder);
        }

        private void makeContextMenu()
        {
            n_ico.Icon = Properties.Resources._1pushsnap_off;
            n_ico.Visible = true;
            n_ico.Text = Properties.Resources.app_name;

            ContextMenuStrip cms = new ContextMenuStrip();
            n_ico.ContextMenuStrip = cms;

            /// Information
            ToolStripMenuItem tsmi_information = new ToolStripMenuItem();
            cms.Items.Add(tsmi_information);
            tsmi_information.Text = Properties.Resources.context_menu_item_information;
            tsmi_information.Click += ToolStripMenuItem_Information_Click;

            ///Save To...
            ToolStripMenuItem tsmi_save_to = new ToolStripMenuItem();
            cms.Items.Add(tsmi_save_to);
            tsmi_save_to.Text = Properties.Resources.context_menu_item_save;
            tsmi_save_to.Click += ToolStripMenuItem_SaveTo_Click;

            ///Image format
            ToolStripMenuItem tsmi_image_format = new ToolStripMenuItem();
            cms.Items.Add(tsmi_image_format);
            tsmi_image_format.Text = Properties.Resources.context_menu_item_image_format;
            tsmi_image_format.Click += ToolStripMenuItem_ImageFormat_Click;

            /// Start
            ToolStripMenuItem tsmi_start = new ToolStripMenuItem();
            cms.Items.Add(tsmi_start);
            tsmi_start.Text = Properties.Resources.context_menu_item_start;
            tsmi_start.Click += ToolStripMenuItem_Start_Click;

            /// Stop
            ToolStripMenuItem tsmi_stop = new ToolStripMenuItem();
            cms.Items.Add(tsmi_stop);
            tsmi_stop.Text = Properties.Resources.context_menu_item_stop;
            tsmi_stop.Click += ToolStripMenuItem_Stop_Click;

            /// Close
            ToolStripMenuItem tsmi_close = new ToolStripMenuItem();
            cms.Items.Add(tsmi_close);
            tsmi_close.Text = Properties.Resources.context_menu_item_close;
            tsmi_close.Click += ToolStripMenuItem_Close_Click;
        }

        private void ToolStripMenuItem_Information_Click(object sender, EventArgs e)
        {
            string msg = Properties.Resources.message_save_folder + Properties.Settings.Default.save_folder;
            MessageBox.Show(msg);
        }

        private void ToolStripMenuItem_ImageFormat_Click(object sender, EventArgs e)
        {
            frmImageType image_form = callImageTypeForm;
            image_form.Show();
        }

        private void ToolStripMenuItem_SaveTo_Click(object sender, EventArgs e)
        {
            setFolderPath();
        }
                
        internal static void switcher()
        {
            working_flg = kh.start_stop_switch();

            if (working_flg == true)
            {
                n_ico.Icon = Properties.Resources._1pushsnap_on;
            }
            else
            {
                n_ico.Icon = Properties.Resources._1pushsnap_off;
            }
        }

        private void ToolStripMenuItem_Start_Click(object sender, EventArgs e)
        {
            switcher();
        }

        private void ToolStripMenuItem_Stop_Click(object sender, EventArgs e)
        {
            switcher();
        }


        private void ToolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            kh.HookEnd();
            n_ico.Visible = false;
            Application.Exit();
        }

        private void setFolderPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                Properties.Settings.Default.save_folder = fbd.SelectedPath.ToString();
                Properties.Settings.Default.Save();
            }
        }
    }
}
