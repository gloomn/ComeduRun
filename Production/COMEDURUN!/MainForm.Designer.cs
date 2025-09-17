namespace COMEDURUN_
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.startPanel = new System.Windows.Forms.Panel();
            this.startButton = new System.Windows.Forms.Button();
            this.startBackground = new System.Windows.Forms.PictureBox();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.gameBackground = new System.Windows.Forms.PictureBox();
            this.startPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startBackground)).BeginInit();
            this.gamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // startPanel
            // 
            this.startPanel.Controls.Add(this.startButton);
            this.startPanel.Controls.Add(this.startBackground);
            this.startPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPanel.Location = new System.Drawing.Point(0, 0);
            this.startPanel.Name = "startPanel";
            this.startPanel.Size = new System.Drawing.Size(1898, 1024);
            this.startPanel.TabIndex = 1;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startButton.FlatAppearance.BorderSize = 3;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Showcard Gothic", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(760, 635);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(400, 120);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "START GAME";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // startBackground
            // 
            this.startBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startBackground.Image = global::COMEDURUN_.Properties.Resources.startBackground;
            this.startBackground.Location = new System.Drawing.Point(0, 0);
            this.startBackground.Name = "startBackground";
            this.startBackground.Size = new System.Drawing.Size(1898, 1024);
            this.startBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.startBackground.TabIndex = 0;
            this.startBackground.TabStop = false;
            // 
            // gamePanel
            // 
            this.gamePanel.Controls.Add(this.gameBackground);
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 0);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(1898, 1024);
            this.gamePanel.TabIndex = 2;
            // 
            // gameBackground
            // 
            this.gameBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameBackground.Image = global::COMEDURUN_.Properties.Resources.gameScene;
            this.gameBackground.Location = new System.Drawing.Point(12, 132);
            this.gameBackground.Name = "gameBackground";
            this.gameBackground.Size = new System.Drawing.Size(1898, 1024);
            this.gameBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gameBackground.TabIndex = 0;
            this.gameBackground.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1898, 1024);
            this.Controls.Add(this.startPanel);
            this.Controls.Add(this.gamePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COMEDURUN!";
            this.startPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.startBackground)).EndInit();
            this.gamePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gameBackground)).EndInit();
            this.ResumeLayout(false);

        }

        private void GamePanel_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.PictureBox startBackground;
        private System.Windows.Forms.Panel startPanel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.PictureBox gameBackground;
    }
}

