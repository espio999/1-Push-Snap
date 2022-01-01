using System;
using System.Windows.Forms;

namespace OnePushSnap
{
    public partial class frmImageType : Form
    {
        public frmImageType()
        {
            InitializeComponent();
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

        private void makeImageTypeCombo()
        {
            string[] image_format = Properties.Resources.image_format.Split('|');

            foreach (var item in image_format)
            {
                cmbImageType.Items.Add(item);
            }

            cmbImageType.SelectedIndex = Array.IndexOf(image_format, Properties.Settings.Default.save_image_type);
            
        }
    }
}
