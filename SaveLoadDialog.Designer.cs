namespace BlueRuby
{
    partial class SaveLoadDialog
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
            this.SaveLoadButton = new System.Windows.Forms.Button();
            this.CnlButton = new System.Windows.Forms.Button();
            this.SaveFilesView = new System.Windows.Forms.ListView();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SaveNameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SaveLoadButton
            // 
            this.SaveLoadButton.Location = new System.Drawing.Point(12, 227);
            this.SaveLoadButton.Name = "SaveLoadButton";
            this.SaveLoadButton.Size = new System.Drawing.Size(145, 23);
            this.SaveLoadButton.TabIndex = 1;
            this.SaveLoadButton.Text = "SaveLoad";
            this.SaveLoadButton.UseVisualStyleBackColor = true;
            this.SaveLoadButton.Click += new System.EventHandler(this.SaveLoadButton_Click);
            // 
            // CnlButton
            // 
            this.CnlButton.Location = new System.Drawing.Point(163, 227);
            this.CnlButton.Name = "CnlButton";
            this.CnlButton.Size = new System.Drawing.Size(145, 23);
            this.CnlButton.TabIndex = 2;
            this.CnlButton.Text = "Cancel";
            this.CnlButton.UseVisualStyleBackColor = true;
            this.CnlButton.Click += new System.EventHandler(this.CnlButton_Click);
            // 
            // SaveFilesView
            // 
            this.SaveFilesView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.SaveFilesView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.DateColumn});
            this.SaveFilesView.FullRowSelect = true;
            this.SaveFilesView.HideSelection = false;
            this.SaveFilesView.Location = new System.Drawing.Point(12, 38);
            this.SaveFilesView.Name = "SaveFilesView";
            this.SaveFilesView.Size = new System.Drawing.Size(296, 183);
            this.SaveFilesView.TabIndex = 3;
            this.SaveFilesView.UseCompatibleStateImageBehavior = false;
            this.SaveFilesView.View = System.Windows.Forms.View.Details;
            this.SaveFilesView.ItemActivate += new System.EventHandler(this.SaveFilesView_ItemActivate);
            this.SaveFilesView.SelectedIndexChanged += new System.EventHandler(this.SaveFilesView_SelectedIndexChanged);
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 177;
            // 
            // DateColumn
            // 
            this.DateColumn.Text = "Date";
            this.DateColumn.Width = 101;
            // 
            // SaveNameBox
            // 
            this.SaveNameBox.Location = new System.Drawing.Point(12, 12);
            this.SaveNameBox.Name = "SaveNameBox";
            this.SaveNameBox.Size = new System.Drawing.Size(296, 20);
            this.SaveNameBox.TabIndex = 4;
            // 
            // SaveLoadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 262);
            this.Controls.Add(this.SaveNameBox);
            this.Controls.Add(this.SaveFilesView);
            this.Controls.Add(this.CnlButton);
            this.Controls.Add(this.SaveLoadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveLoadDialog";
            this.Text = "SaveLoadDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SaveLoadButton;
        private System.Windows.Forms.Button CnlButton;
        private System.Windows.Forms.ListView SaveFilesView;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader DateColumn;
        private System.Windows.Forms.TextBox SaveNameBox;
    }
}