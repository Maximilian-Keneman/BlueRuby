namespace BlueRuby
{
    partial class PlayerPanel
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
            this.components = new System.ComponentModel.Container();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.TeamBox = new System.Windows.Forms.ComboBox();
            this.BotCheck = new System.Windows.Forms.CheckBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.TeamCountError = new System.Windows.Forms.ErrorProvider(this.components);
            this.NamesError = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TeamCountError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NamesError)).BeginInit();
            this.SuspendLayout();
            // 
            // NameBox
            // 
            this.NameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameBox.Location = new System.Drawing.Point(18, 3);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(90, 20);
            this.NameBox.TabIndex = 0;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            this.NameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameBox_KeyPress);
            // 
            // TeamBox
            // 
            this.TeamBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TeamBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TeamBox.FormattingEnabled = true;
            this.TeamBox.Location = new System.Drawing.Point(114, 3);
            this.TeamBox.Name = "TeamBox";
            this.TeamBox.Size = new System.Drawing.Size(121, 21);
            this.TeamBox.TabIndex = 1;
            this.TeamBox.SelectedIndexChanged += new System.EventHandler(this.TeamBox_SelectedIndexChanged);
            // 
            // BotCheck
            // 
            this.BotCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BotCheck.AutoSize = true;
            this.BotCheck.Location = new System.Drawing.Point(254, 5);
            this.BotCheck.Name = "BotCheck";
            this.BotCheck.Size = new System.Drawing.Size(73, 17);
            this.BotCheck.TabIndex = 2;
            this.BotCheck.Text = "BotCheck";
            this.BotCheck.UseVisualStyleBackColor = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.Location = new System.Drawing.Point(333, 3);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(24, 23);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "X";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // TeamCountError
            // 
            this.TeamCountError.ContainerControl = this;
            // 
            // NamesError
            // 
            this.NamesError.ContainerControl = this;
            // 
            // PlayerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.BotCheck);
            this.Controls.Add(this.TeamBox);
            this.Controls.Add(this.NameBox);
            this.Name = "PlayerPanel";
            this.Size = new System.Drawing.Size(360, 27);
            ((System.ComponentModel.ISupportInitialize)(this.TeamCountError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NamesError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.ComboBox TeamBox;
        private System.Windows.Forms.CheckBox BotCheck;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.ErrorProvider TeamCountError;
        private System.Windows.Forms.ErrorProvider NamesError;
    }
}
