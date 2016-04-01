namespace GameOfLifeApp
{
    partial class GameOfLifeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameOfLifeForm));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.nextStateButton = new System.Windows.Forms.ToolStripButton();
            this.resetButton = new System.Windows.Forms.ToolStripButton();
            this.picture = new System.Windows.Forms.PictureBox();
            this.cellSizeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.cellSizeLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gridButton = new System.Windows.Forms.ToolStripButton();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.nextStateButton,
            this.resetButton,
            this.gridButton,
            this.toolStripSeparator1,
            this.cellSizeLabel,
            this.cellSizeComboBox});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(427, 25);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolBar";
            // 
            // openFileButton
            // 
            this.openFileButton.AutoToolTip = false;
            this.openFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openFileButton.Image = ((System.Drawing.Image)(resources.GetObject("openFileButton.Image")));
            this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(61, 22);
            this.openFileButton.Text = "Open File";
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // nextStateButton
            // 
            this.nextStateButton.AutoToolTip = false;
            this.nextStateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.nextStateButton.Image = ((System.Drawing.Image)(resources.GetObject("nextStateButton.Image")));
            this.nextStateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextStateButton.Name = "nextStateButton";
            this.nextStateButton.Size = new System.Drawing.Size(64, 22);
            this.nextStateButton.Text = "Next State";
            this.nextStateButton.Click += new System.EventHandler(this.nextStateButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resetButton.Image = ((System.Drawing.Image)(resources.GetObject("resetButton.Image")));
            this.resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(39, 22);
            this.resetButton.Text = "Reset";
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // picture
            // 
            this.picture.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture.Location = new System.Drawing.Point(0, 25);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(427, 391);
            this.picture.TabIndex = 1;
            this.picture.TabStop = false;
            // 
            // cellSizeComboBox
            // 
            this.cellSizeComboBox.Items.AddRange(new object[] {
            "5",
            "10",
            "20",
            "30",
            "40",
            "50"});
            this.cellSizeComboBox.Name = "cellSizeComboBox";
            this.cellSizeComboBox.Size = new System.Drawing.Size(75, 25);
            this.cellSizeComboBox.Text = "10";
            this.cellSizeComboBox.Click += new System.EventHandler(this.cellSizeComboBox_Click);
            // 
            // cellSizeLabel
            // 
            this.cellSizeLabel.Name = "cellSizeLabel";
            this.cellSizeLabel.Size = new System.Drawing.Size(53, 22);
            this.cellSizeLabel.Text = "Cell Size:";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // gridButton
            // 
            this.gridButton.CheckOnClick = true;
            this.gridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gridButton.Image = ((System.Drawing.Image)(resources.GetObject("gridButton.Image")));
            this.gridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gridButton.Name = "gridButton";
            this.gridButton.Size = new System.Drawing.Size(33, 22);
            this.gridButton.Text = "Grid";
            // 
            // GameOfLifeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 416);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.toolBar);
            this.Name = "GameOfLifeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game of Life";
            this.Load += new System.EventHandler(this.GameOfLifeForm_Load);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton openFileButton;
        private System.Windows.Forms.ToolStripButton nextStateButton;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.ToolStripButton resetButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel cellSizeLabel;
        private System.Windows.Forms.ToolStripComboBox cellSizeComboBox;
        private System.Windows.Forms.ToolStripButton gridButton;
    }
}

