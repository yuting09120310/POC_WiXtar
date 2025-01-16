using QRCoder;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace POC_WiXtar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 標籤參數
            string itemName = "冰四季烏龍L";
            string price = "$65";
            string temperature = "常溫 / 無糖";
            string qrContent = "{\"item\":\"冰四季烏龍L\",\"price\":\"65\",\"origin\":\"台灣\"}";
            string storeCode = "店號: PQ";
            string date = "2024/11/13";
            string phone = "電話: 2711-8868";

            // 生成標籤
            GenerateLabel(itemName, price, temperature, qrContent, storeCode, date, phone);
        }


        private void GenerateLabel(string itemName, string price, string temperature, string qrContent, string storeCode, string date, string phone)
        {
            // 標籤畫布大小
            const int canvasWidth = 300;
            const int canvasHeight = 150;
            Bitmap labelBitmap = new Bitmap(canvasWidth, canvasHeight);

            using Graphics g = Graphics.FromImage(labelBitmap);

            // 設定背景
            g.Clear(Color.White);

            // 字體與畫刷
            Font titleFont = new Font("Arial", 12, FontStyle.Bold); // 商品名稱字體
            Font contentFont = new Font("Arial", 10, FontStyle.Regular); // 內容字體
            Brush brush = Brushes.Black;


            // 商品名稱與溫度位置 (垂直置中且靠左)
            int totalTextHeight = (int)g.MeasureString(itemName, titleFont).Height + (int)g.MeasureString(temperature, contentFont).Height;
            int startY = (canvasHeight - totalTextHeight) / 2;

            // 商品名稱
            g.DrawString(itemName, titleFont, brush, 10, startY - 20);

            // 溫度
            startY += (int)g.MeasureString(itemName, titleFont).Height + 5;
            g.DrawString(temperature, contentFont, brush, 10, startY);

            // 價格
            g.DrawString(price, titleFont, brush, canvasWidth - 140, 65);

            // QR Code
            Bitmap qrCodeImage = GenerateQRCode(qrContent);
            if (qrCodeImage != null)
            {
                int qrCodeSize = 80;
                int qrCodeY = (canvasHeight - qrCodeSize) / 2;
                g.DrawImage(qrCodeImage, canvasWidth - qrCodeSize - 10, qrCodeY, qrCodeSize, qrCodeSize);
            }

            // 底部文字 (店號、日期、電話)
            int bottomY = canvasHeight - (int)g.MeasureString("Test", contentFont).Height - 10;
            int spacing = (canvasWidth - 20) / 3;

            g.DrawString(storeCode, contentFont, brush, 10, bottomY); // 左
            g.DrawString(date, contentFont, brush, 10 + spacing, bottomY); // 中
            g.DrawString(phone, contentFont, brush, 10 + spacing * 2, bottomY); // 右

            // 顯示標籤到 PictureBox
            PictureBox picBox = new PictureBox
            {
                Image = labelBitmap,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            Controls.Add(picBox);
        }


        private Bitmap GenerateQRCode(string qrContent)
        {
            try
            {
                using QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCoder.QRCodeGenerator.ECCLevel.Q);
                using QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
                return qrCode.GetGraphic(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"QR Code 生成失敗: {ex.Message}");
                return null;
            }
        }
    }
}
