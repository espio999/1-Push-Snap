using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
                Properties.Settings.Default.crop_rectangle = new Rectangle(
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
            capt.snap(Properties.Settings.Default.crop_rectangle);

            foreach (frmCropMode form in blank_forms)
            {
                form.Dispose();

                /*
                form.Invoke((MethodInvoker)delegate ()
                {
                    form.Dispose();
                });
                */
            }

            configuration_form.kh.start_stop_switch();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_location = e.Location;
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, Properties.Settings.Default.crop_rectangle);
            e.Graphics.DrawRectangle(new Pen(Color.GhostWhite), Properties.Settings.Default.crop_rectangle);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.crop_rectangle = new Rectangle(0, 0, 0, 0);
        }
    }
}
