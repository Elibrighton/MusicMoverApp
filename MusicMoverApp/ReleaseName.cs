using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicMoverApp
{
    public partial class ReleaseName : Form
    {
        private MusicMover musicMover;

        public ReleaseName(MusicMover musicMover, string directoryName, string targetPath)
        {
            InitializeComponent();
            this.musicMover = musicMover;
            directoryNameDisplayLabel.Text = directoryName;
            targetPathDisplayLabel.Text = targetPath;
        }

        public void saveButton_Click(object sender, EventArgs e)
        {
            musicMover.Pattern = patternTextBox.Text;
            musicMover.ReleaseNameDisplay = releaseNameTextBox.Text;
            musicMover.IsUsingParentDirectory = useParentDirCheckBox.Checked;
            Close();
        }
    }
}
