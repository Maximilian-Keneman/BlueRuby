namespace BlueRuby
{
    partial class GameMenuForm
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.CardsDeckBox = new System.Windows.Forms.PictureBox();
            this.EnergyDeckBox = new System.Windows.Forms.PictureBox();
            this.DeckCountLabel = new System.Windows.Forms.Label();
            this.LiveCountLabel = new System.Windows.Forms.Label();
            this.LoadButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CardsDeckBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnergyDeckBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(224, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(224, 70);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // CardsDeckBox
            // 
            this.CardsDeckBox.Location = new System.Drawing.Point(12, 12);
            this.CardsDeckBox.Name = "CardsDeckBox";
            this.CardsDeckBox.Size = new System.Drawing.Size(100, 112);
            this.CardsDeckBox.TabIndex = 3;
            this.CardsDeckBox.TabStop = false;
            // 
            // EnergyDeckBox
            // 
            this.EnergyDeckBox.Location = new System.Drawing.Point(118, 12);
            this.EnergyDeckBox.Name = "EnergyDeckBox";
            this.EnergyDeckBox.Size = new System.Drawing.Size(100, 112);
            this.EnergyDeckBox.TabIndex = 4;
            this.EnergyDeckBox.TabStop = false;
            // 
            // DeckCountLabel
            // 
            this.DeckCountLabel.AutoSize = true;
            this.DeckCountLabel.Location = new System.Drawing.Point(46, 132);
            this.DeckCountLabel.Name = "DeckCountLabel";
            this.DeckCountLabel.Size = new System.Drawing.Size(19, 13);
            this.DeckCountLabel.TabIndex = 5;
            this.DeckCountLabel.Text = "00";
            // 
            // LiveCountLabel
            // 
            this.LiveCountLabel.AutoSize = true;
            this.LiveCountLabel.Location = new System.Drawing.Point(162, 132);
            this.LiveCountLabel.Name = "LiveCountLabel";
            this.LiveCountLabel.Size = new System.Drawing.Size(19, 13);
            this.LiveCountLabel.TabIndex = 6;
            this.LiveCountLabel.Text = "00";
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(224, 41);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 7;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // GameMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 159);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.LiveCountLabel);
            this.Controls.Add(this.DeckCountLabel);
            this.Controls.Add(this.EnergyDeckBox);
            this.Controls.Add(this.CardsDeckBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SaveButton);
            this.Name = "GameMenuForm";
            this.Text = "GameMenuForm";
            ((System.ComponentModel.ISupportInitialize)(this.CardsDeckBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnergyDeckBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.PictureBox CardsDeckBox;
        private System.Windows.Forms.PictureBox EnergyDeckBox;
        private System.Windows.Forms.Label DeckCountLabel;
        private System.Windows.Forms.Label LiveCountLabel;
        private System.Windows.Forms.Button LoadButton;
    }
}