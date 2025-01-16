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
                MessageBox.Show("請輸入內容以生成 QR Code！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Bitmap qrCodeImage = GenerateQRCode(input);
                picQRCode.Image = qrCodeImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"生成 QR Code 時發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 生成 QR Code 圖片
        /// </summary>
        /// <param name="input">需要轉換為 QR Code 的字串</param>
        /// <returns>生成的 QR Code 圖片 (Bitmap)</returns>
        private Bitmap GenerateQRCode(string input)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20); // 設置像素比例，20 表示每個方塊的大小
                }
            }
        }
    }
}
