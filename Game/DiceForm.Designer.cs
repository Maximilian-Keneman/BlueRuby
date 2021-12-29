namespace BlueRuby
{
    partial class DiceForm
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
            this.RollButton = new System.Windows.Forms.Button();
            this.DiceCheckBox = new System.Windows.Forms.PictureBox();
            this.CardCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DiceCheckBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RollButton
            // 
            this.RollButton.Enabled = false;
            this.RollButton.Location = new System.Drawing.Point(12, 12);
            this.RollButton.Name = "RollButton";
            this.RollButton.Size = new System.Drawing.Size(100, 23);
            this.RollButton.TabIndex = 0;
            this.RollButton.Text = "Бросить кости";
            this.RollButton.UseVisualStyleBackColor = true;
            this.RollButton.Click += new System.EventHandler(this.RollButton_Click);
            // 
            // DiceCheckBox
            // 
            this.DiceCheckBox.Location = new System.Drawing.Point(118, 12);
            this.DiceCheckBox.Name = "DiceCheckBox";
            this.DiceCheckBox.Size = new System.Drawing.Size(70, 70);
            this.DiceCheckBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.DiceCheckBox.TabIndex = 1;
            this.DiceCheckBox.TabStop = false;
            // 
            // CardCheckBox
            // 
            this.CardCheckBox.Location = new System.Drawing.Point(12, 41);
            this.CardCheckBox.Name = "CardCheckBox";
            this.CardCheckBox.Size = new System.Drawing.Size(100, 41);
            this.CardCheckBox.TabIndex = 2;
            this.CardCheckBox.Text = "Use Magic Fire";
            this.CardCheckBox.UseVisualStyleBackColor = true;
            this.CardCheckBox.Visible = false;
            // 
            // DiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 94);
            this.Controls.Add(this.CardCheckBox);
            this.Controls.Add(this.DiceCheckBox);
            this.Controls.Add(this.RollButton);
            this.Name = "DiceForm";
            this.Text = "DiceForm";
            ((System.ComponentModel.ISupportInitialize)(this.DiceCheckBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RollButton;
        private System.Windows.Forms.PictureBox DiceCheckBox;
        private System.Windows.Forms.CheckBox CardCheckBox;
    }
}