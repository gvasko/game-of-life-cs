using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLifeApp
{
    public partial class GameOfLifeForm : Form
    {
        internal const int DefaultCellSize = 10;

        private Logic.IApplication appLogic;

        public GameOfLifeForm()
        {
            InitializeComponent();
        }

        private void GameOfLifeForm_Load(object sender, EventArgs e)
        {
            appLogic = Logic.DefaultAppFactory.GetFactory().CreateApplicationLogic();
            appLogic.CellSize = DefaultCellSize;
            picture.DataBindings.Add("Image", appLogic, "Image");
            cellSizeComboBox.TextChanged += cellSizeComboBox_Changed;
            gridButton.CheckedChanged += gridButton_Chenged;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Game Of Life files (*.lif)|*.lif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    appLogic.File = openFileDialog.FileName;
                }
                catch (Exception)
                {
                    MessageBox.Show(string.Format("File cannot be loaded:\n{0}", openFileDialog.FileName), "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void nextStateButton_Click(object sender, EventArgs e)
        {
            appLogic.NextImage();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            appLogic.ResetImage();
        }

        private void cellSizeComboBox_Click(object sender, EventArgs e)
        {
        }

        private void cellSizeComboBox_Changed(object sender, EventArgs e)
        {
            int newCellSize = 0;
            if (!Int32.TryParse(cellSizeComboBox.Text, out newCellSize))
            {
                newCellSize = DefaultCellSize;
            }

            appLogic.CellSize = newCellSize;
        }

        private void gridButton_Chenged(object sender, EventArgs e)
        {
            appLogic.GridEnabled = gridButton.Checked;
        }

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
            if (disposing)
            {
                appLogic.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
