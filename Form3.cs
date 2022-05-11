using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using GS = OnePushSnap.Properties.Settings;

namespace OnePushSnap
{
    public partial class frmCropMode : Form
    {
        static Point mouse_location;
        static List<frmCropMode> blank_forms = new List<frmCropMode>();

        public frmCropMode()
        {
            InitializeComponent();
        }

        public void overlayCropForm(Screen screen)
        {
            this.Bounds = screen.Bounds;
            Application.EnableVisualStyles();

            blank_forms.Add(this);

            this.Show();
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GS.Default.crop_rectangle = new Rectangle(
                    Math.Min(e.X, mouse_location.X),
                    Math.Min(e.Y, mouse_location.Y),
                    Math.Abs(e.X - mouse_location.X),
                    Math.Abs(e.Y - mouse_location.Y));

                ((frmCropMode)sender).Invalidate();
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            Capture capt = new Capture();
            capt.snap(GS.Default.crop_rectangle);

            foreach (frmCropMode form in blank_forms)
            {
                form.Dispose();
            }

            //configuration_form.kh.start_stop_switch();
            configuration_form.kh.HookFor1PushSnap();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_location = e.Location;
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, GS.Default.crop_rectangle);
            e.Graphics.DrawRectangle(new Pen(Color.GhostWhite), GS.Default.crop_rectangle);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            GS.Default.crop_rectangle = new Rectangle(0, 0, 0, 0);
        }
    }
}
