namespace BlueRuby
{
    partial class GameForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GameBackgroundBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameBackgroundBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.GameBackgroundBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GameBackgroundBox.Image = global::BlueRuby.Properties.Resources.gametable;
            this.GameBackgroundBox.ImageLocation = "";
            this.GameBackgroundBox.Location = new System.Drawing.Point(0, 0);
            this.GameBackgroundBox.Margin = new System.Windows.Forms.Padding(0);
            this.GameBackgroundBox.Name = "pictureBox1";
            this.GameBackgroundBox.Size = new System.Drawing.Size(676, 900);
            this.GameBackgroundBox.TabIndex = 0;
            this.GameBackgroundBox.TabStop = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 543);
            this.Controls.Add(this.GameBackgroundBox);
            this.Name = "GameForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.GameBackgroundBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GameBackgroundBox;
    }
}

