using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomMessageBox : Form
{
    public CustomMessageBox(Image image, string message)
    {
        // 設定對話框大小
        this.Size = new Size(400, 320);
        this.Text = "自訂訊息框";
        this.StartPosition = FormStartPosition.CenterScreen;

        // 加入 PictureBox
        PictureBox pictureBox = new PictureBox
        {
            Image = image,
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(10, 10),
            Size = new Size(360, 180) // 圖片區域大小
        };
        this.Controls.Add(pictureBox);

        // 加入訊息文字
        Label messageLabel = new Label
        {
            Text = message,
            AutoSize = false,
            Location = new Point(10, 200),
            Size = new Size(360, 40),
            TextAlign = ContentAlignment.MiddleCenter
        };
        this.Controls.Add(messageLabel);

        // 加入確定按鈕
        Button okButton = new Button
        {
            Text = "確定",
            DialogResult = DialogResult.OK,
            Location = new Point(160, 250),
            Size = new Size(75, 30)
        };
        this.Controls.Add(okButton);

        // 設定按鈕的 DialogResult
        this.AcceptButton = okButton;
    }

    private void InitializeComponent()
    {

    }
}
