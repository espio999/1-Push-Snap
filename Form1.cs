using System;
using System.IO;
using System.Windows.Forms;

namespace OnePushSnap 
{
    public partial class configuration_form : Form
    {
        private static NotifyIcon n_ico = new NotifyIcon();
        internal static KeyHook kh = new KeyHook();

        /// mode
        /// 0 = off
        /// 1 = 1 push snap
        /// 2 = crop snap
        /// 102 = ignore keybord
        /// 103 = ignore mouse
        internal static int working_flg = 0;

        /// taskbar context menu
        private ContextMenuStrip cms = new ContextMenuStrip();
        private ToolStripMenuItem tsmi_information = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_save_to = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_image_format = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_start_1pushsnap = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_start_cropsnap = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_start_igg_keyboard = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_start_igg_mouse = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_stop = new ToolStripMenuItem();
        private ToolStripMenuItem tsmi_close = new ToolStripMenuItem();

        private static configuration_form single_instance;

        public static configuration_form getInstance()
        {
            {
                if (single_instance == null || single_instance.IsDisposed)
                {
                    single_instance = new configuration_form();
                }

                return single_instance;
            }
        }

        private configuration_form()
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
            this.Activated += MainForm_Activated;
        }

        private void MainForm_Activated(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void makeContextMenu()
        {
            /// taskbar icon
            n_ico.Icon = Properties.Resources._1pushsnap_off;
            n_ico.Visible = true;
            n_ico.Text = Properties.Resources.app_name;
            n_ico.ContextMenuStrip = cms;

            /// Information
            cms.Items.Add(tsmi_information);
            tsmi_information.Text = Properties.Resources.context_menu_item_information;
            tsmi_information.Click += ToolStripMenuItem_Information_Click;

            ///Save To...
            cms.Items.Add(tsmi_save_to);
            tsmi_save_to.Text = Properties.Resources.context_menu_item_save;
            tsmi_save_to.Click += ToolStripMenuItem_SaveTo_Click;

            ///Image format
            cms.Items.Add(tsmi_image_format);
            tsmi_image_format.Text = Properties.Resources.context_menu_item_image_format;
            tsmi_image_format.Click += ToolStripMenuItem_ImageFormat_Click;

            /// Start - 1 push snap
            cms.Items.Add(tsmi_start_1pushsnap);
            tsmi_start_1pushsnap.Text = Properties.Resources.context_menu_item_start_1pushsnap;
            tsmi_start_1pushsnap.Click += ToolStripMenuItem_Start_1PushSnap_Click;

            /// Start - Crop snap
            cms.Items.Add(tsmi_start_cropsnap);
            tsmi_start_cropsnap.Text = Properties.Resources.context_menu_item_start_cropsnap;
            tsmi_start_cropsnap.Click += ToolStripMenuItem_Start_CropSnap_Click;

            /// Start - ignore keyboard
            cms.Items.Add(tsmi_start_igg_keyboard);
            tsmi_start_igg_keyboard.Text = Properties.Resources.context_menu_item_start_igg_keyboard;
            tsmi_start_igg_keyboard.Click += ToolStripMenuItem_Start_IggKeyboard_Click;

            /// Start - ignore mouse
            cms.Items.Add(tsmi_start_igg_mouse);
            tsmi_start_igg_mouse.Text = Properties.Resources.context_menu_item_start_igg_mouse;
            tsmi_start_igg_mouse.Click += ToolStripMenuItem_Start_IggMouse_Click;

            /// Stop
            cms.Items.Add(tsmi_stop);
            tsmi_stop.Text = Properties.Resources.context_menu_item_stop;
            tsmi_stop.Click += ToolStripMenuItem_Stop_Click;

            /// Close
            cms.Items.Add(tsmi_close);
            tsmi_close.Text = Properties.Resources.context_menu_item_close;
            tsmi_close.Click += ToolStripMenuItem_Close_Click;
        }

        private void ToolStripMenuItem_Information_Click(object sender, EventArgs e)
        {
            //String msg = Properties.Resources.message_save_folder + Properties.Settings.Default.save_folder;
            String msg = String.Format(Properties.Resources.message_save_folder, Properties.Settings.Default.save_folder);
            MessageBox.Show(msg);
        }

        private void ToolStripMenuItem_ImageFormat_Click(object sender, EventArgs e)
        {
            frmImageType fit = frmImageType.callImageTypeForm();
            fit.Show();
        }

        private void ToolStripMenuItem_SaveTo_Click(object sender, EventArgs e)
        {
            setFolderPath();
        }
                
        private void switcher()
        {
            kh.start_stop_switch();

            switch (working_flg)
            {
                case 0:
                    n_ico.Icon = Properties.Resources._1pushsnap_off;
                    enableMenuItems();
                    break;
                case 1:
                    n_ico.Icon = Properties.Resources._1pushsnap_on;
                    disableMenuItems();
                    break;
                case 2:
                    n_ico.Icon = Properties.Resources.cropmode_on;
                    disableMenuItems();
                    break;
                default:
                    disableMenuItems();
                    break;
            }

        }

        private void ToolStripMenuItem_Start_1PushSnap_Click(object sender, EventArgs e)
        {
            working_flg = 1;
            switcher();
        }

        private void ToolStripMenuItem_Start_CropSnap_Click(object sender, EventArgs e)
        {
            working_flg = 2;
            switcher();

            frmCropMode form = new frmCropMode();

            foreach(Screen screen in Screen.AllScreens)
            {
                form.overlayCropForm(screen);
            }
        }

        private void ToolStripMenuItem_Start_IggKeyboard_Click(object sender, EventArgs e)
        {
            working_flg = 102;
            switcher();

            MessageBox.Show(Properties.Resources.message_igg_keyboard);
            tsmi_stop.PerformClick();
        }

        private void ToolStripMenuItem_Start_IggMouse_Click(object sender, EventArgs e)
        {
            working_flg = 103;
            switcher();

            MessageBox.Show(Properties.Resources.message_igg_mouse);
            tsmi_stop.PerformClick();
        }

        private void ToolStripMenuItem_Stop_Click(object sender, EventArgs e)
        {
            working_flg = 0;
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

        private void enableMenuItems()
        {
            tsmi_start_1pushsnap.Enabled = true;
            tsmi_start_cropsnap.Enabled = true;
            tsmi_start_igg_keyboard.Enabled = true;
            tsmi_start_igg_mouse.Enabled = true;
        }

        private void disableMenuItems()
        {
            tsmi_start_1pushsnap.Enabled = false;
            tsmi_start_cropsnap.Enabled= false;
            tsmi_start_igg_keyboard.Enabled = false;
            tsmi_start_igg_mouse.Enabled = false;
        }

        private delegate void delegateClickStop();

        internal static void clickStop()
        {
            if (single_instance.InvokeRequired)
            {
                single_instance.Invoke(new delegateClickStop(clickStop));
            }
            else
            {
                single_instance.tsmi_stop.PerformClick();
            }
        }

    }
}
