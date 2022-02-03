using System;
using System.Windows.Forms;

namespace OnePushSnap
{
    public partial class frmImageType : Form
    {
        private frmImageType()
        {
            InitializeComponent();
        }

        private static frmImageType single_instance;

        public static frmImageType callImageTypeForm()
        {
            {
                if (single_instance == null || single_instance.IsDisposed)
                {
                    single_instance = new frmImageType();
                }

                return single_instance;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.save_image_type = cmbImageType.SelectedItem.ToString();
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void frmImageType_Load(object sender, EventArgs e)
        {
            lblImageType.Text = Properties.Resources.message_image_type_label;
            makeImageTypeCombo();

            flpImageType.Controls.Add(lblImageType);
            flpImageType.Controls.Add(cmbImageType);
            flpImageType.Controls.Add(btnOK);
        }

        /// ComboBox with supporting image formats
        private void makeImageTypeCombo()
        {
            string[] image_format = Properties.Resources.image_format.Split('|');

            cmbImageType.Items.AddRange(image_format);
            cmbImageType.SelectedIndex = Array.IndexOf(image_format, Properties.Settings.Default.save_image_type);
            
        }
    }
}
