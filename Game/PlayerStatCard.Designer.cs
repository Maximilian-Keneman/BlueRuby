namespace BlueRuby
{
    partial class PlayerStatCard
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnergyBox = new System.Windows.Forms.PictureBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.LiveCount = new System.Windows.Forms.Label();
            this.CardsBox = new System.Windows.Forms.PictureBox();
            this.GoldCount = new System.Windows.Forms.Label();
            this.BlueRubyBox = new System.Windows.Forms.PictureBox();
            this.BlueRubyCount = new System.Windows.Forms.Label();
            this.TurnBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.EnergyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueRubyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnBox)).BeginInit();
            this.SuspendLayout();
            // 
            // EnergyBox
            // 
            this.EnergyBox.Location = new System.Drawing.Point(3, 28);
            this.EnergyBox.Name = "EnergyBox";
            this.EnergyBox.Size = new System.Drawing.Size(100, 112);
            this.EnergyBox.TabIndex = 0;
            this.EnergyBox.TabStop = false;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLabel.Location = new System.Drawing.Point(3, 8);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(46, 17);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "label1";
            // 
            // LiveCount
            // 
            this.LiveCount.AutoSize = true;
            this.LiveCount.Location = new System.Drawing.Point(3, 143);
            this.LiveCount.Name = "LiveCount";
            this.LiveCount.Size = new System.Drawing.Size(35, 13);
            this.LiveCount.TabIndex = 2;
            this.LiveCount.Text = "label2";
            // 
            // CardsBox
            // 
            this.CardsBox.Location = new System.Drawing.Point(109, 28);
            this.CardsBox.Name = "CardsBox";
            this.CardsBox.Size = new System.Drawing.Size(100, 112);
            this.CardsBox.TabIndex = 3;
            this.CardsBox.TabStop = false;
            // 
            // GoldCount
            // 
            this.GoldCount.AutoSize = true;
            this.GoldCount.Location = new System.Drawing.Point(106, 143);
            this.GoldCount.Name = "GoldCount";
            this.GoldCount.Size = new System.Drawing.Size(35, 13);
            this.GoldCount.TabIndex = 4;
            this.GoldCount.Text = "label3";
            // 
            // BlueRubyBox
            // 
            this.BlueRubyBox.Location = new System.Drawing.Point(215, 28);
            this.BlueRubyBox.Name = "BlueRubyBox";
            this.BlueRubyBox.Size = new System.Drawing.Size(64, 112);
            this.BlueRubyBox.TabIndex = 5;
            this.BlueRubyBox.TabStop = false;
            // 
            // BlueRubyCount
            // 
            this.BlueRubyCount.AutoSize = true;
            this.BlueRubyCount.Location = new System.Drawing.Point(212, 143);
            this.BlueRubyCount.Name = "BlueRubyCount";
            this.BlueRubyCount.Size = new System.Drawing.Size(35, 13);
            this.BlueRubyCount.TabIndex = 6;
            this.BlueRubyCount.Text = "label4";
            // 
            // TurnBox
            // 
            this.TurnBox.Location = new System.Drawing.Point(246, 3);
            this.TurnBox.Name = "TurnBox";
            this.TurnBox.Size = new System.Drawing.Size(33, 19);
            this.TurnBox.TabIndex = 7;
            this.TurnBox.TabStop = false;
            // 
            // PlayerStatCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.TurnBox);
            this.Controls.Add(this.BlueRubyCount);
            this.Controls.Add(this.BlueRubyBox);
            this.Controls.Add(this.GoldCount);
            this.Controls.Add(this.CardsBox);
            this.Controls.Add(this.LiveCount);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.EnergyBox);
            this.Name = "PlayerStatCard";
            this.Size = new System.Drawing.Size(282, 156);
            ((System.ComponentModel.ISupportInitialize)(this.EnergyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardsBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueRubyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox EnergyBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label LiveCount;
        private System.Windows.Forms.PictureBox CardsBox;
        private System.Windows.Forms.Label GoldCount;
        private System.Windows.Forms.PictureBox BlueRubyBox;
        private System.Windows.Forms.Label BlueRubyCount;
        private System.Windows.Forms.PictureBox TurnBox;
    }
}
