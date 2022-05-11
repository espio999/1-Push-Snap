using System;
using System.Windows.Forms;

using GR = OnePushSnap.Properties.Resources;
using GS = OnePushSnap.Properties.Settings;

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
            GS.Default.save_image_type = cmbImageType.SelectedItem.ToString();
            GS.Default.Save();
            this.Close();
        }

        private void frmImageType_Load(object sender, EventArgs e)
        {
            lblImageType.Text = GR.message_image_type_label;
            makeImageTypeCombo();

            flpImageType.Controls.Add(lblImageType);
            flpImageType.Controls.Add(cmbImageType);
            flpImageType.Controls.Add(btnOK);
        }

        /// ComboBox with supporting image formats
        private void makeImageTypeCombo()
        {
            string[] image_format = GR.image_format.Split('|');

            cmbImageType.Items.AddRange(image_format);
            cmbImageType.SelectedIndex = Array.IndexOf(image_format, GS.Default.save_image_type);
            
        }
    }
}
