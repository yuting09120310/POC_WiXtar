namespace POC_WiXtar
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            txtInput = new TextBox();
            picQRCode = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picQRCode).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(253, 32);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtInput
            // 
            txtInput.Location = new Point(23, 33);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(203, 23);
            txtInput.TabIndex = 1;
            // 
            // picQRCode
            // 
            picQRCode.Location = new Point(12, 72);
            picQRCode.Name = "picQRCode";
            picQRCode.Size = new Size(332, 265);
            picQRCode.SizeMode = PictureBoxSizeMode.StretchImage;
            picQRCode.TabIndex = 2;
            picQRCode.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 349);
            Controls.Add(picQRCode);
            Controls.Add(txtInput);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picQRCode).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox txtInput;
        private PictureBox picQRCode;
    }
}
