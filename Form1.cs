using System;
using System.Windows.Forms;

namespace OnePushSnap 
{
    public partial class configuration_form : Form
    {
        NotifyIcon n_ico = new NotifyIcon();
        KeyHook kh = new KeyHook();
        Boolean working_flg = false;

        public configuration_form()
        {
            InitializeComponent();

            this.ShowInTaskbar = true;
            Properties.Settings.Default.save_folder = Environment.GetEnvironmentVariable(Properties.Resources.initial_folder);
            Properties.Settings.Default.default_folder = Environment.GetEnvironmentVariable(Properties.Resources.initial_folder);

            makeContextMenu();
        }

        private void makeContextMenu()
        {
            n_ico.Icon = Properties.Resources.camera_off;
            n_ico.Visible = true;
            n_ico.Text = Properties.Resources.app_name;

            ContextMenuStrip cms = new ContextMenuStrip();
            n_ico.ContextMenuStrip = cms;

            ToolStripMenuItem tsmi_information = new ToolStripMenuItem();
            cms.Items.Add(tsmi_information);
            tsmi_information.Text = Properties.Resources.context_menu_item_information;
            tsmi_information.Click += ToolStripMenuItem_Information_Click;

            ToolStripMenuItem tsmi_save_to = new ToolStripMenuItem();
            cms.Items.Add(tsmi_save_to);
            tsmi_save_to.Text = Properties.Resources.context_menu_item_save;
            tsmi_save_to.Click += ToolStripMenuItem_SaveTo_Click;

            ToolStripMenuItem tsmi_start = new ToolStripMenuItem();
            cms.Items.Add(tsmi_start);
            tsmi_start.Text = Properties.Resources.context_menu_item_start;
            tsmi_start.Click += ToolStripMenuItem_Start_Click;

            ToolStripMenuItem tsmi_stop = new ToolStripMenuItem();
            cms.Items.Add(tsmi_stop);
            tsmi_stop.Text = Properties.Resources.context_menu_item_stop;
            tsmi_stop.Click += ToolStripMenuItem_Stop_Click;

            ToolStripMenuItem tsmi_close = new ToolStripMenuItem();
            cms.Items.Add(tsmi_close);
            tsmi_close.Text = Properties.Resources.context_menu_item_close;
            tsmi_close.Click += ToolStripMenuItem_Close_Click;
        }

        private void ToolStripMenuItem_Information_Click(object sender, EventArgs e)
        {
            string msg = Properties.Resources.message_save_folder + "\n" + 
                Properties.Settings.Default.save_folder;
            MessageBox.Show(msg);
        }

        private void ToolStripMenuItem_SaveTo_Click(object sender, EventArgs e)
        {
            setFolderPath();
        }

        private void ToolStripMenuItem_Start_Click(object sender, EventArgs e)
        {
            if (working_flg == false)
            {
                working_flg = true;
                n_ico.Icon = Properties.Resources.camera_on;
                kh.Hook();
            }
        }

        private void ToolStripMenuItem_Stop_Click(object sender, EventArgs e)
        {
            if (working_flg == true)
            {
                working_flg = false;
                n_ico.Icon = Properties.Resources.camera_off;
                kh.HookEnd();
            }
            
        }

        private void ToolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            n_ico.Visible = false;
            kh.HookEnd();
            Application.Exit();
        }

        private void setFolderPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                Properties.Settings.Default.save_folder = fbd.SelectedPath.ToString();
            }
        }
    }
}
