using QRCoder;

namespace POC_WiXtar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text;
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("�п�J���e�H�ͦ� QR Code�I", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Bitmap qrCodeImage = GenerateQRCode(input);
                picQRCode.Image = qrCodeImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�ͦ� QR Code �ɵo�Ϳ��~�G{ex.Message}", "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// �ͦ� QR Code �Ϥ�
        /// </summary>
        /// <param name="input">�ݭn�ഫ�� QR Code ���r��</param>
        /// <returns>�ͦ��� QR Code �Ϥ� (Bitmap)</returns>
        private Bitmap GenerateQRCode(string input)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20); // �]�m������ҡA20 ��ܨC�Ӥ�����j�p
                }
            }
        }
    }
}
