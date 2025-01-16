using QRCoder;
using System;
using System.Data;
using System.Data.SqlClient;
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
            
        }

       

        private void btn_search_Click(object sender, EventArgs e)
        {
            string ecrhdkey = txt_EcrHdKey.Text;

            string sql = @$"select EHH.ShopNo, EHH.BDate, S.Tel1, VW.ITEM_NAME, CONVERT(int, VW.SALE_PRICE) AS SALE_PRICE
                            from EcrTrHs ETH
                            left join EcrHdHs EHH ON ETH.EcrHdKey = EHH.EcrHdKey
                            left join [TR_DATA].[dbo].[VW_TRPOS_ITEM_V] VW on ETH.HGdsNo = VW.HGDSNO
                            left join Shop S ON EHH.ShopNo = S.ShopNo
                            where 1=1
                            AND vw.CLASS_WTG = 'WTG-IT'
                            AND EHH.EcrHdKey = '{ecrhdkey}'";


            DataTable dt = GetData(sql);

            if(dt.Rows.Count == 0)
            {
                MessageBox.Show("查無資料");
                return;
            }


            // 標籤參數
            string storeCode = $"店號: {dt.Rows[0][0].ToString()}";
            string date = dt.Rows[0][1].ToString();
            string phone = dt.Rows[0][2].ToString();
            string itemName = dt.Rows[0][3].ToString();
            string price = $"${dt.Rows[0][4].ToString()}";
            string temperature = "常溫 / 無糖";
            string qrContent = $"{{\"item\":\"{itemName}\",\"price\":\"{dt.Rows[0][4].ToString()}\",\"memo\":\"{temperature}\"}}";

            // 生成標籤
            GenerateLabel(itemName, price, temperature, qrContent, storeCode, date, phone);
        }


        public DataTable GetData(string sql)
        {
            // 建立資料庫連接字串，請依您的環境修改
            string connectionString = "Server=10.10.5.66;Database=RX_V4_TRN;User Id=sa;Password=Retex16031227;";

            // 創建一個空的 DataTable 來儲存查詢結果
            DataTable dataTable = new DataTable();

            try
            {
                // 建立資料庫連接
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // 使用 SqlDataAdapter 執行查詢並填充 DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                    {
                        // 開啟資料庫連接
                        conn.Open();

                        // 填充 DataTable
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                // 若發生錯誤，顯示錯誤訊息
                Console.WriteLine($"Error: {ex.Message}");
            }

            // 返回 DataTable，若查詢失敗則返回空的 DataTable
            return dataTable;
        }


        #region 貼紙 QR Code 標籤

        private void GenerateLabel(string itemName, string price, string temperature, string qrContent, string storeCode, string date, string phone)
        {
            // 移除現有的 PictureBox (清除上一次的標籤)
            foreach (Control control in Controls)
            {
                if (control is PictureBox existingPicBox)
                {
                    Controls.Remove(existingPicBox); // 移除 PictureBox 控制項
                    existingPicBox.Dispose(); // 釋放 PictureBox 控制項的資源
                }
            }

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
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            Controls.Add(picBox);
        }


        private Bitmap GenerateQRCode(string qrContent)
        {
            try
            {
                using QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                using QRCode qrCode = new QRCode(qrCodeData);
                return qrCode.GetGraphic(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"QR Code 生成失敗: {ex.Message}");
                return null;
            }
        }

        #endregion

    }
}
