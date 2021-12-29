namespace BlueRuby
{
    partial class SettingsForm
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
            this.DefaultsSaveButton = new System.Windows.Forms.Button();
            this.LocalizationBox = new System.Windows.Forms.ComboBox();
            this.LightNamesBox = new System.Windows.Forms.RichTextBox();
            this.DarkNamesBox = new System.Windows.Forms.RichTextBox();
            this.PlayersCountNumeric = new System.Windows.Forms.NumericUpDown();
            this.DeckmodeBox = new System.Windows.Forms.ComboBox();
            this.SaveParamsButton = new System.Windows.Forms.Button();
            this.LotteryBox = new System.Windows.Forms.CheckBox();
            this.CreateLocButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PlayersCountNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // DefaultsSaveButton
            // 
            this.DefaultsSaveButton.Location = new System.Drawing.Point(12, 359);
            this.DefaultsSaveButton.Name = "DefaultsSaveButton";
            this.DefaultsSaveButton.Size = new System.Drawing.Size(121, 23);
            this.DefaultsSaveButton.TabIndex = 6;
            this.DefaultsSaveButton.Text = "Save Defaults";
            this.DefaultsSaveButton.UseVisualStyleBackColor = true;
            this.DefaultsSaveButton.Click += new System.EventHandler(this.DefaultsSaveButton_Click);
            // 
            // LocalizationBox
            // 
            this.LocalizationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LocalizationBox.FormattingEnabled = true;
            this.LocalizationBox.Location = new System.Drawing.Point(33, 175);
            this.LocalizationBox.Name = "LocalizationBox";
            this.LocalizationBox.Size = new System.Drawing.Size(121, 21);
            this.LocalizationBox.TabIndex = 5;
            this.LocalizationBox.SelectedIndexChanged += new System.EventHandler(this.LocalizationBox_SelectedIndexChanged);
            // 
            // LightNamesBox
            // 
            this.LightNamesBox.Location = new System.Drawing.Point(292, 27);
            this.LightNamesBox.Name = "LightNamesBox";
            this.LightNamesBox.Size = new System.Drawing.Size(87, 123);
            this.LightNamesBox.TabIndex = 7;
            this.LightNamesBox.Text = "";
            this.LightNamesBox.TextChanged += new System.EventHandler(this.NamesCount_Changed);
            this.LightNamesBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NamesBox_KeyPress);
            // 
            // DarkNamesBox
            // 
            this.DarkNamesBox.Location = new System.Drawing.Point(385, 27);
            this.DarkNamesBox.Name = "DarkNamesBox";
            this.DarkNamesBox.Size = new System.Drawing.Size(87, 123);
            this.DarkNamesBox.TabIndex = 8;
            this.DarkNamesBox.Text = "";
            this.DarkNamesBox.TextChanged += new System.EventHandler(this.NamesCount_Changed);
            this.DarkNamesBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NamesBox_KeyPress);
            // 
            // PlayersCountNumeric
            // 
            this.PlayersCountNumeric.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.PlayersCountNumeric.Location = new System.Drawing.Point(400, 168);
            this.PlayersCountNumeric.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.PlayersCountNumeric.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.PlayersCountNumeric.Name = "PlayersCountNumeric";
            this.PlayersCountNumeric.Size = new System.Drawing.Size(33, 20);
            this.PlayersCountNumeric.TabIndex = 9;
            this.PlayersCountNumeric.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.PlayersCountNumeric.ValueChanged += new System.EventHandler(this.NamesCount_Changed);
            // 
            // DeckmodeBox
            // 
            this.DeckmodeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeckmodeBox.FormattingEnabled = true;
            this.DeckmodeBox.Location = new System.Drawing.Point(385, 206);
            this.DeckmodeBox.Name = "DeckmodeBox";
            this.DeckmodeBox.Size = new System.Drawing.Size(121, 21);
            this.DeckmodeBox.TabIndex = 10;
            this.DeckmodeBox.SelectedIndexChanged += new System.EventHandler(this.DeckmodeBox_SelectedIndexChanged);
            // 
            // SaveParamsButton
            // 
            this.SaveParamsButton.Location = new System.Drawing.Point(358, 256);
            this.SaveParamsButton.Name = "SaveParamsButton";
            this.SaveParamsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveParamsButton.TabIndex = 11;
            this.SaveParamsButton.Text = "Save params";
            this.SaveParamsButton.UseVisualStyleBackColor = true;
            this.SaveParamsButton.Click += new System.EventHandler(this.SaveParamsButton_Click);
            // 
            // LotteryBox
            // 
            this.LotteryBox.AutoSize = true;
            this.LotteryBox.Location = new System.Drawing.Point(392, 233);
            this.LotteryBox.Name = "LotteryBox";
            this.LotteryBox.Size = new System.Drawing.Size(90, 17);
            this.LotteryBox.TabIndex = 12;
            this.LotteryBox.Text = "Rubies lottery";
            this.LotteryBox.UseVisualStyleBackColor = true;
            // 
            // CreateLocButton
            // 
            this.CreateLocButton.Location = new System.Drawing.Point(33, 206);
            this.CreateLocButton.Name = "CreateLocButton";
            this.CreateLocButton.Size = new System.Drawing.Size(121, 23);
            this.CreateLocButton.TabIndex = 13;
            this.CreateLocButton.Text = "Create Localization";
            this.CreateLocButton.UseVisualStyleBackColor = true;
            this.CreateLocButton.Click += new System.EventHandler(this.CreateLocButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 394);
            this.Controls.Add(this.CreateLocButton);
            this.Controls.Add(this.LotteryBox);
            this.Controls.Add(this.SaveParamsButton);
            this.Controls.Add(this.DeckmodeBox);
            this.Controls.Add(this.PlayersCountNumeric);
            this.Controls.Add(this.DarkNamesBox);
            this.Controls.Add(this.LightNamesBox);
            this.Controls.Add(this.DefaultsSaveButton);
            this.Controls.Add(this.LocalizationBox);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.PlayersCountNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DefaultsSaveButton;
        private System.Windows.Forms.ComboBox LocalizationBox;
        private System.Windows.Forms.RichTextBox LightNamesBox;
        private System.Windows.Forms.RichTextBox DarkNamesBox;
        private System.Windows.Forms.NumericUpDown PlayersCountNumeric;
        private System.Windows.Forms.ComboBox DeckmodeBox;
        private System.Windows.Forms.Button SaveParamsButton;
        private System.Windows.Forms.CheckBox LotteryBox;
        private System.Windows.Forms.Button CreateLocButton;
    }
}