using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OnePushSnap
{
    internal class Capture
    {
        [StructLayout(LayoutKind.Sequential)]
        
        private struct WindowCoordinate
        {
            public int upper_left_x;
            public int upper_left_y;
            public int bottom_right_x;
            public int bottom_right_y;
        }

        [DllImport("user32.Dll")]
        static extern int GetWindowRect(IntPtr hWnd, out WindowCoordinate rect);


        [DllImport("user32.dll")]
        extern static IntPtr GetForegroundWindow();

        [DllImport("dwmapi.dll")]
        extern static int DwmGetWindowAttribute(IntPtr hWnd, int dwAttribute, out WindowCoordinate rect, int cbAttribute);


        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        String filename(String my_prefix)
        {
            String my_dir;
            String my_file = String.Join("", new String[]{
                my_prefix, 
                DateTime.Now.ToString("yyyyMMddHHmmss"), 
                DateTime.Now.Millisecond.ToString(), ".", Properties.Settings.Default.save_image_type});

            if (Directory.Exists(Properties.Settings.Default.save_folder)){
                my_dir = Properties.Settings.Default.save_folder;
            }
            else
            {
                my_dir = Properties.Settings.Default.default_folder;

                String msg = String.Format(Properties.Resources.message_non_existent_folder, Properties.Settings.Default.default_folder);
                Task.Factory.StartNew(() => MessageBox.Show(msg));
            }

            return String.Join(@"\", new String[] { my_dir, my_file});
        }

        public void snap(Rectangle my_rectangle)
        {
            Bitmap my_bmp = new Bitmap(my_rectangle.Width, my_rectangle.Height);
            Graphics my_graphics = Graphics.FromImage(my_bmp);

            my_graphics.CopyFromScreen(my_rectangle.X, my_rectangle.Y, 0, 0, my_rectangle.Size);

            switch (Properties.Settings.Default.save_image_type)
            {
                case "BMP":
                    my_bmp.Save(
                        filename(Properties.Resources.file_prefix),
                        System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case "GIF":
                    my_bmp.Save(
                        filename(Properties.Resources.file_prefix),
                        System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case "JPG":
                    my_bmp.Save(
                        filename(Properties.Resources.file_prefix),
                        System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case "PNG":
                    my_bmp.Save(
                        filename(Properties.Resources.file_prefix),
                        System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }

            my_graphics.Dispose();
            my_bmp.Dispose();
        }


        public void snapActiveWindow()
        {
            int DWMWA_EXTENDED_FRAME_BOUNDS = 9;

            WindowCoordinate wc;
            IntPtr window_handle = GetForegroundWindow();

            DwmGetWindowAttribute(window_handle, DWMWA_EXTENDED_FRAME_BOUNDS, out wc, Marshal.SizeOf(typeof(WindowCoordinate)));

            int rectangle_width = wc.bottom_right_x - wc.upper_left_x;
            int rectangle_height = wc.bottom_right_y - wc.upper_left_y;
            Rectangle my_rectangle = new Rectangle(wc.upper_left_x, wc.upper_left_y, rectangle_width, rectangle_height);

            Task.Factory.StartNew(() => snap(my_rectangle));
        }

        public void snapScreen()
        {
            Task.Factory.StartNew(() => snap(Screen.PrimaryScreen.Bounds));
        }
    }
}
