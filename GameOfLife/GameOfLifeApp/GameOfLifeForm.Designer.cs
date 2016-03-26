namespace GameOfLifeApp
{
    partial class GameOfLifeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameOfLifeForm));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.nextStateButton = new System.Windows.Forms.ToolStripButton();
            this.picture = new System.Windows.Forms.PictureBox();
            this.resetButton = new System.Windows.Forms.ToolStripButton();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.nextStateButton,
            this.resetButton});
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
            // 
            // picture
            // 
            this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture.Location = new System.Drawing.Point(0, 25);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(427, 391);
            this.picture.TabIndex = 1;
            this.picture.TabStop = false;
            // 
            // resetButton
            // 
            this.resetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.resetButton.Image = ((System.Drawing.Image)(resources.GetObject("resetButton.Image")));
            this.resetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(39, 22);
            this.resetButton.Text = "Reset";
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
    }
}

