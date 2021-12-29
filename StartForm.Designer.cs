namespace BlueRuby
{
    partial class StartForm
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
            this.TeamVSTeamMode = new System.Windows.Forms.RadioButton();
            this.PlayerVSPlayerMode = new System.Windows.Forms.RadioButton();
            this.GameModeBox = new System.Windows.Forms.GroupBox();
            this.PlayersPanel = new System.Windows.Forms.Panel();
            this.AddPlayerButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.FiveGold_Numeric = new System.Windows.Forms.NumericUpDown();
            this.FiveGold_Check = new System.Windows.Forms.CheckBox();
            this.DeckCompilationBox = new System.Windows.Forms.GroupBox();
            this.TenGold_Check = new System.Windows.Forms.CheckBox();
            this.TenGold_Numeric = new System.Windows.Forms.NumericUpDown();
            this.Wizard_Check = new System.Windows.Forms.CheckBox();
            this.Wizard_Numeric = new System.Windows.Forms.NumericUpDown();
            this.Spell_Check = new System.Windows.Forms.CheckBox();
            this.Spell_Numeric = new System.Windows.Forms.NumericUpDown();
            this.Shield_Check = new System.Windows.Forms.CheckBox();
            this.Dragon_Numeric = new System.Windows.Forms.NumericUpDown();
            this.Shield_Numeric = new System.Windows.Forms.NumericUpDown();
            this.Dragon_Check = new System.Windows.Forms.CheckBox();
            this.DeckModesBox = new System.Windows.Forms.ComboBox();
            this.StartLive_Label = new System.Windows.Forms.Label();
            this.StartLive_Numeric = new System.Windows.Forms.NumericUpDown();
            this.SaveParamsButton = new System.Windows.Forms.Button();
            this.DeckLive_Numeric = new System.Windows.Forms.NumericUpDown();
            this.LiveCompilationBox = new System.Windows.Forms.GroupBox();
            this.DeckLive_Check = new System.Windows.Forms.CheckBox();
            this.NamesError = new System.Windows.Forms.ErrorProvider(this.components);
            this.CountError = new System.Windows.Forms.ErrorProvider(this.components);
            this.DeleteButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TeamCountError = new System.Windows.Forms.ErrorProvider(this.components);
            this.GameModeBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FiveGold_Numeric)).BeginInit();
            this.DeckCompilationBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TenGold_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wizard_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spell_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dragon_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Shield_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartLive_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeckLive_Numeric)).BeginInit();
            this.LiveCompilationBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NamesError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeamCountError)).BeginInit();
            this.SuspendLayout();
            // 
            // TeamVSTeamMode
            // 
            this.TeamVSTeamMode.AutoSize = true;
            this.TeamVSTeamMode.Location = new System.Drawing.Point(6, 42);
            this.TeamVSTeamMode.Name = "TeamVSTeamMode";
            this.TeamVSTeamMode.Size = new System.Drawing.Size(96, 17);
            this.TeamVSTeamMode.TabIndex = 0;
            this.TeamVSTeamMode.Text = "Team vs Team";
            this.TeamVSTeamMode.UseVisualStyleBackColor = true;
            this.TeamVSTeamMode.CheckedChanged += new System.EventHandler(this.Gamemode_Changed);
            // 
            // PlayerVSPlayerMode
            // 
            this.PlayerVSPlayerMode.AutoSize = true;
            this.PlayerVSPlayerMode.Checked = true;
            this.PlayerVSPlayerMode.Location = new System.Drawing.Point(6, 19);
            this.PlayerVSPlayerMode.Name = "PlayerVSPlayerMode";
            this.PlayerVSPlayerMode.Size = new System.Drawing.Size(100, 17);
            this.PlayerVSPlayerMode.TabIndex = 1;
            this.PlayerVSPlayerMode.TabStop = true;
            this.PlayerVSPlayerMode.Text = "Player vs Player";
            this.PlayerVSPlayerMode.UseVisualStyleBackColor = true;
            this.PlayerVSPlayerMode.CheckedChanged += new System.EventHandler(this.Gamemode_Changed);
            // 
            // GameModeBox
            // 
            this.GameModeBox.Controls.Add(this.TeamVSTeamMode);
            this.GameModeBox.Controls.Add(this.PlayerVSPlayerMode);
            this.GameModeBox.Location = new System.Drawing.Point(12, 12);
            this.GameModeBox.Name = "GameModeBox";
            this.GameModeBox.Size = new System.Drawing.Size(114, 68);
            this.GameModeBox.TabIndex = 3;
            this.GameModeBox.TabStop = false;
            this.GameModeBox.Text = "GameMode";
            // 
            // PlayersPanel
            // 
            this.PlayersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayersPanel.AutoScroll = true;
            this.PlayersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PlayersPanel.Location = new System.Drawing.Point(132, 12);
            this.PlayersPanel.Name = "PlayersPanel";
            this.PlayersPanel.Size = new System.Drawing.Size(441, 124);
            this.PlayersPanel.TabIndex = 4;
            // 
            // AddPlayerButton
            // 
            this.AddPlayerButton.Location = new System.Drawing.Point(12, 86);
            this.AddPlayerButton.Name = "AddPlayerButton";
            this.AddPlayerButton.Size = new System.Drawing.Size(114, 23);
            this.AddPlayerButton.TabIndex = 5;
            this.AddPlayerButton.Text = "Add Player";
            this.AddPlayerButton.UseVisualStyleBackColor = true;
            this.AddPlayerButton.Click += new System.EventHandler(this.AddPlayer_Click);
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(495, 415);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // FiveGold_Numeric
            // 
            this.FiveGold_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FiveGold_Numeric.Location = new System.Drawing.Point(116, 18);
            this.FiveGold_Numeric.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.FiveGold_Numeric.Name = "FiveGold_Numeric";
            this.FiveGold_Numeric.Size = new System.Drawing.Size(48, 20);
            this.FiveGold_Numeric.TabIndex = 7;
            this.FiveGold_Numeric.ValueChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // FiveGold_Check
            // 
            this.FiveGold_Check.AutoSize = true;
            this.FiveGold_Check.Location = new System.Drawing.Point(6, 19);
            this.FiveGold_Check.Name = "FiveGold_Check";
            this.FiveGold_Check.Size = new System.Drawing.Size(55, 17);
            this.FiveGold_Check.TabIndex = 9;
            this.FiveGold_Check.Text = "5 gold";
            this.FiveGold_Check.UseVisualStyleBackColor = true;
            this.FiveGold_Check.CheckedChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // DeckCompilationBox
            // 
            this.DeckCompilationBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeckCompilationBox.Controls.Add(this.TenGold_Check);
            this.DeckCompilationBox.Controls.Add(this.TenGold_Numeric);
            this.DeckCompilationBox.Controls.Add(this.Wizard_Check);
            this.DeckCompilationBox.Controls.Add(this.FiveGold_Check);
            this.DeckCompilationBox.Controls.Add(this.Wizard_Numeric);
            this.DeckCompilationBox.Controls.Add(this.FiveGold_Numeric);
            this.DeckCompilationBox.Controls.Add(this.Spell_Check);
            this.DeckCompilationBox.Controls.Add(this.Spell_Numeric);
            this.DeckCompilationBox.Controls.Add(this.Shield_Check);
            this.DeckCompilationBox.Controls.Add(this.Dragon_Numeric);
            this.DeckCompilationBox.Controls.Add(this.Shield_Numeric);
            this.DeckCompilationBox.Controls.Add(this.Dragon_Check);
            this.DeckCompilationBox.Location = new System.Drawing.Point(12, 142);
            this.DeckCompilationBox.Name = "DeckCompilationBox";
            this.DeckCompilationBox.Size = new System.Drawing.Size(173, 160);
            this.DeckCompilationBox.TabIndex = 10;
            this.DeckCompilationBox.TabStop = false;
            this.DeckCompilationBox.Text = "Deck Compilation";
            // 
            // TenGold_Check
            // 
            this.TenGold_Check.AutoSize = true;
            this.TenGold_Check.Location = new System.Drawing.Point(6, 42);
            this.TenGold_Check.Name = "TenGold_Check";
            this.TenGold_Check.Size = new System.Drawing.Size(61, 17);
            this.TenGold_Check.TabIndex = 19;
            this.TenGold_Check.Text = "10 gold";
            this.TenGold_Check.UseVisualStyleBackColor = true;
            this.TenGold_Check.CheckedChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // TenGold_Numeric
            // 
            this.TenGold_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TenGold_Numeric.Location = new System.Drawing.Point(116, 41);
            this.TenGold_Numeric.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TenGold_Numeric.Name = "TenGold_Numeric";
            this.TenGold_Numeric.Size = new System.Drawing.Size(48, 20);
            this.TenGold_Numeric.TabIndex = 18;
            this.TenGold_Numeric.ValueChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Wizard_Check
            // 
            this.Wizard_Check.AutoSize = true;
            this.Wizard_Check.Location = new System.Drawing.Point(6, 134);
            this.Wizard_Check.Name = "Wizard_Check";
            this.Wizard_Check.Size = new System.Drawing.Size(59, 17);
            this.Wizard_Check.TabIndex = 17;
            this.Wizard_Check.Text = "Wizard";
            this.Wizard_Check.UseVisualStyleBackColor = true;
            this.Wizard_Check.CheckedChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Wizard_Numeric
            // 
            this.Wizard_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Wizard_Numeric.Location = new System.Drawing.Point(116, 133);
            this.Wizard_Numeric.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.Wizard_Numeric.Name = "Wizard_Numeric";
            this.Wizard_Numeric.Size = new System.Drawing.Size(48, 20);
            this.Wizard_Numeric.TabIndex = 16;
            this.Wizard_Numeric.ValueChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Spell_Check
            // 
            this.Spell_Check.AutoSize = true;
            this.Spell_Check.Location = new System.Drawing.Point(6, 65);
            this.Spell_Check.Name = "Spell_Check";
            this.Spell_Check.Size = new System.Drawing.Size(101, 17);
            this.Spell_Check.TabIndex = 15;
            this.Spell_Check.Text = "Spell Magic Fire";
            this.Spell_Check.UseVisualStyleBackColor = true;
            this.Spell_Check.CheckedChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Spell_Numeric
            // 
            this.Spell_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Spell_Numeric.Location = new System.Drawing.Point(116, 64);
            this.Spell_Numeric.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Spell_Numeric.Name = "Spell_Numeric";
            this.Spell_Numeric.Size = new System.Drawing.Size(48, 20);
            this.Spell_Numeric.TabIndex = 14;
            this.Spell_Numeric.ValueChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Shield_Check
            // 
            this.Shield_Check.AutoSize = true;
            this.Shield_Check.Location = new System.Drawing.Point(6, 88);
            this.Shield_Check.Name = "Shield_Check";
            this.Shield_Check.Size = new System.Drawing.Size(55, 17);
            this.Shield_Check.TabIndex = 13;
            this.Shield_Check.Text = "Shield";
            this.Shield_Check.UseVisualStyleBackColor = true;
            this.Shield_Check.CheckedChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Dragon_Numeric
            // 
            this.Dragon_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Dragon_Numeric.Location = new System.Drawing.Point(116, 110);
            this.Dragon_Numeric.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Dragon_Numeric.Name = "Dragon_Numeric";
            this.Dragon_Numeric.Size = new System.Drawing.Size(48, 20);
            this.Dragon_Numeric.TabIndex = 10;
            this.Dragon_Numeric.ValueChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Shield_Numeric
            // 
            this.Shield_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Shield_Numeric.Location = new System.Drawing.Point(116, 87);
            this.Shield_Numeric.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Shield_Numeric.Name = "Shield_Numeric";
            this.Shield_Numeric.Size = new System.Drawing.Size(48, 20);
            this.Shield_Numeric.TabIndex = 12;
            this.Shield_Numeric.ValueChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // Dragon_Check
            // 
            this.Dragon_Check.AutoSize = true;
            this.Dragon_Check.Location = new System.Drawing.Point(6, 111);
            this.Dragon_Check.Name = "Dragon_Check";
            this.Dragon_Check.Size = new System.Drawing.Size(61, 17);
            this.Dragon_Check.TabIndex = 11;
            this.Dragon_Check.Text = "Dragon";
            this.Dragon_Check.UseVisualStyleBackColor = true;
            this.Dragon_Check.CheckedChanged += new System.EventHandler(this.DeckParams_Changed);
            // 
            // DeckModesBox
            // 
            this.DeckModesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeckModesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeckModesBox.FormattingEnabled = true;
            this.DeckModesBox.Location = new System.Drawing.Point(12, 115);
            this.DeckModesBox.Name = "DeckModesBox";
            this.DeckModesBox.Size = new System.Drawing.Size(114, 21);
            this.DeckModesBox.TabIndex = 11;
            this.DeckModesBox.SelectedIndexChanged += new System.EventHandler(this.DeckMode_Changed);
            // 
            // StartLive_Label
            // 
            this.StartLive_Label.AutoSize = true;
            this.StartLive_Label.Location = new System.Drawing.Point(24, 19);
            this.StartLive_Label.Margin = new System.Windows.Forms.Padding(3);
            this.StartLive_Label.Name = "StartLive_Label";
            this.StartLive_Label.Size = new System.Drawing.Size(83, 13);
            this.StartLive_Label.TabIndex = 12;
            this.StartLive_Label.Text = "Start Live Count";
            // 
            // StartLive_Numeric
            // 
            this.StartLive_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StartLive_Numeric.Location = new System.Drawing.Point(116, 17);
            this.StartLive_Numeric.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.StartLive_Numeric.Name = "StartLive_Numeric";
            this.StartLive_Numeric.Size = new System.Drawing.Size(48, 20);
            this.StartLive_Numeric.TabIndex = 13;
            this.StartLive_Numeric.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.StartLive_Numeric.ValueChanged += new System.EventHandler(this.LiveParams_Changed);
            // 
            // SaveParamsButton
            // 
            this.SaveParamsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveParamsButton.Location = new System.Drawing.Point(12, 386);
            this.SaveParamsButton.Name = "SaveParamsButton";
            this.SaveParamsButton.Size = new System.Drawing.Size(173, 23);
            this.SaveParamsButton.TabIndex = 14;
            this.SaveParamsButton.Text = "Сохранить параметры старта";
            this.SaveParamsButton.UseVisualStyleBackColor = true;
            this.SaveParamsButton.Click += new System.EventHandler(this.SaveParamsButton_Click);
            // 
            // DeckLive_Numeric
            // 
            this.DeckLive_Numeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeckLive_Numeric.Location = new System.Drawing.Point(116, 43);
            this.DeckLive_Numeric.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.DeckLive_Numeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.DeckLive_Numeric.Name = "DeckLive_Numeric";
            this.DeckLive_Numeric.Size = new System.Drawing.Size(48, 20);
            this.DeckLive_Numeric.TabIndex = 16;
            this.DeckLive_Numeric.ValueChanged += new System.EventHandler(this.LiveParams_Changed);
            // 
            // LiveCompilationBox
            // 
            this.LiveCompilationBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LiveCompilationBox.Controls.Add(this.DeckLive_Check);
            this.LiveCompilationBox.Controls.Add(this.StartLive_Label);
            this.LiveCompilationBox.Controls.Add(this.DeckLive_Numeric);
            this.LiveCompilationBox.Controls.Add(this.StartLive_Numeric);
            this.LiveCompilationBox.Location = new System.Drawing.Point(12, 308);
            this.LiveCompilationBox.Name = "LiveCompilationBox";
            this.LiveCompilationBox.Size = new System.Drawing.Size(173, 72);
            this.LiveCompilationBox.TabIndex = 17;
            this.LiveCompilationBox.TabStop = false;
            this.LiveCompilationBox.Text = "Live Compilation";
            // 
            // DeckLive_Check
            // 
            this.DeckLive_Check.AutoSize = true;
            this.DeckLive_Check.Location = new System.Drawing.Point(6, 44);
            this.DeckLive_Check.Name = "DeckLive_Check";
            this.DeckLive_Check.Size = new System.Drawing.Size(105, 17);
            this.DeckLive_Check.TabIndex = 18;
            this.DeckLive_Check.Text = "Deck Live count";
            this.DeckLive_Check.UseVisualStyleBackColor = true;
            this.DeckLive_Check.CheckedChanged += new System.EventHandler(this.LiveParams_Changed);
            // 
            // NamesError
            // 
            this.NamesError.ContainerControl = this;
            // 
            // CountError
            // 
            this.CountError.ContainerControl = this;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(12, 415);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(173, 23);
            this.DeleteButton.TabIndex = 18;
            this.DeleteButton.Text = "DeleteParams";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadButton.Location = new System.Drawing.Point(495, 386);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(75, 23);
            this.LoadButton.TabIndex = 19;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.Location = new System.Drawing.Point(432, 142);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(141, 32);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Text = "Раздать синие рубины случайным образом";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // TeamCountError
            // 
            this.TeamCountError.ContainerControl = this;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 450);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.LiveCompilationBox);
            this.Controls.Add(this.SaveParamsButton);
            this.Controls.Add(this.DeckModesBox);
            this.Controls.Add(this.DeckCompilationBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.AddPlayerButton);
            this.Controls.Add(this.PlayersPanel);
            this.Controls.Add(this.GameModeBox);
            this.MinimumSize = new System.Drawing.Size(598, 489);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.GameModeBox.ResumeLayout(false);
            this.GameModeBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FiveGold_Numeric)).EndInit();
            this.DeckCompilationBox.ResumeLayout(false);
            this.DeckCompilationBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TenGold_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Wizard_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Spell_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dragon_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Shield_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartLive_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeckLive_Numeric)).EndInit();
            this.LiveCompilationBox.ResumeLayout(false);
            this.LiveCompilationBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NamesError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TeamCountError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton TeamVSTeamMode;
        private System.Windows.Forms.RadioButton PlayerVSPlayerMode;
        private System.Windows.Forms.GroupBox GameModeBox;
        private System.Windows.Forms.Panel PlayersPanel;
        private System.Windows.Forms.Button AddPlayerButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.NumericUpDown FiveGold_Numeric;
        private System.Windows.Forms.CheckBox FiveGold_Check;
        private System.Windows.Forms.GroupBox DeckCompilationBox;
        private System.Windows.Forms.CheckBox TenGold_Check;
        private System.Windows.Forms.NumericUpDown TenGold_Numeric;
        private System.Windows.Forms.CheckBox Wizard_Check;
        private System.Windows.Forms.NumericUpDown Wizard_Numeric;
        private System.Windows.Forms.CheckBox Spell_Check;
        private System.Windows.Forms.NumericUpDown Spell_Numeric;
        private System.Windows.Forms.CheckBox Shield_Check;
        private System.Windows.Forms.NumericUpDown Dragon_Numeric;
        private System.Windows.Forms.NumericUpDown Shield_Numeric;
        private System.Windows.Forms.CheckBox Dragon_Check;
        private System.Windows.Forms.ComboBox DeckModesBox;
        private System.Windows.Forms.Label StartLive_Label;
        private System.Windows.Forms.NumericUpDown StartLive_Numeric;
        private System.Windows.Forms.Button SaveParamsButton;
        private System.Windows.Forms.NumericUpDown DeckLive_Numeric;
        private System.Windows.Forms.GroupBox LiveCompilationBox;
        private System.Windows.Forms.CheckBox DeckLive_Check;
        private System.Windows.Forms.ErrorProvider NamesError;
        private System.Windows.Forms.ErrorProvider CountError;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ErrorProvider TeamCountError;
    }
}