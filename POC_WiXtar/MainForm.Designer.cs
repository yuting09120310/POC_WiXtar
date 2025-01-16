namespace POC_WiXtar
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txt_EcrHdKey = new TextBox();
            btn_search = new Button();
            SuspendLayout();
            // 
            // txt_EcrHdKey
            // 
            txt_EcrHdKey.Location = new Point(328, 40);
            txt_EcrHdKey.Name = "txt_EcrHdKey";
            txt_EcrHdKey.Size = new Size(167, 23);
            txt_EcrHdKey.TabIndex = 0;
            // 
            // btn_search
            // 
            btn_search.Location = new Point(377, 82);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(75, 23);
            btn_search.TabIndex = 1;
            btn_search.Text = "搜尋";
            btn_search.UseVisualStyleBackColor = true;
            btn_search.Click += btn_search_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 152);
            Controls.Add(btn_search);
            Controls.Add(txt_EcrHdKey);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_EcrHdKey;
        private Button btn_search;
    }
}