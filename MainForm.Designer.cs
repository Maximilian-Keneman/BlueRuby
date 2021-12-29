namespace BlueRuby
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
            this.components = new System.ComponentModel.Container();
            this.StartButton = new System.Windows.Forms.Button();
            this.QuickStartButton = new System.Windows.Forms.Button();
            this.BackgroundBox = new System.Windows.Forms.PictureBox();
            this.AnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.SettingsButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(168, 293);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(125, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // QuickStartButton
            // 
            this.QuickStartButton.Location = new System.Drawing.Point(168, 264);
            this.QuickStartButton.Name = "QuickStartButton";
            this.QuickStartButton.Size = new System.Drawing.Size(125, 23);
            this.QuickStartButton.TabIndex = 2;
            this.QuickStartButton.Text = "Quick Start";
            this.QuickStartButton.UseVisualStyleBackColor = true;
            this.QuickStartButton.Click += new System.EventHandler(this.QuickStartButton_Click);
            // 
            // BackgroundBox
            // 
            this.BackgroundBox.Image = global::BlueRuby.Properties.Resources.Background;
            this.BackgroundBox.Location = new System.Drawing.Point(0, 0);
            this.BackgroundBox.Name = "BackgroundBox";
            this.BackgroundBox.Size = new System.Drawing.Size(464, 885);
            this.BackgroundBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BackgroundBox.TabIndex = 0;
            this.BackgroundBox.TabStop = false;
            // 
            // AnimationTimer
            // 
            this.AnimationTimer.Tick += new System.EventHandler(this.AnimationTimer_Tick);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(168, 322);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(125, 23);
            this.SettingsButton.TabIndex = 5;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Debug";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 561);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.QuickStartButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.BackgroundBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button QuickStartButton;
        private System.Windows.Forms.Timer AnimationTimer;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button button1;
    }
}