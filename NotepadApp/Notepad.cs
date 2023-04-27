using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace NotepadApp
{
    public partial class frmNotepad : Form
    {
        public frmNotepad()
        {
            InitializeComponent();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;
            searchWithWebToolStripMenuItem.Enabled = false;
            findToolStripMenuItem.Enabled = false;
            findNextToolStripMenuItem.Enabled = false;
            findPreviousToolStripMenuItem.Enabled = false;
            searchWithWebToolStripMenuItem.Enabled = false;
        }

        private void rtxtBox_TextChanged(object sender, EventArgs e)
        {
            undoToolStripMenuItem.Enabled = true;
            redoToolStripMenuItem.Enabled = true;
            findToolStripMenuItem.Enabled = true;
            findNextToolStripMenuItem.Enabled = true;
            findPreviousToolStripMenuItem.Enabled = true;
        }

        private void rtxtBox_SelectionChanged(object sender, EventArgs e)
        {
            cutToolStripMenuItem.Enabled = rtxtBox.SelectionLength > 0;
            copyToolStripMenuItem.Enabled = rtxtBox.SelectionLength > 0;
            deleteToolStripMenuItem.Enabled = rtxtBox.SelectionLength > 0;
            searchWithWebToolStripMenuItem.Enabled = rtxtBox.SelectionLength > 0;
        }

        string filePath = "";



        #region File
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtxtBox.Modified)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes to this note?", "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
                else
                    rtxtBox.Clear();
            }
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNotepad newForm = new frmNotepad();
            newForm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Text Documents (*.txt)|*.txt|All Files (*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                filePath = open.FileName;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string fileContents = reader.ReadToEnd();
                    rtxtBox.Text = fileContents;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath == "")
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    filePath = save.FileName;
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(rtxtBox.Text);
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(rtxtBox.Text);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveAs = new SaveFileDialog();
            saveAs.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveAs.ShowDialog() == DialogResult.OK)
            {
                filePath = saveAs.FileName;
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(rtxtBox.Text);
                }
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This function is not available currently \nDo use other features...");
            return;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is coming soon...");
            return;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (filePath == "")
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        filePath = save.FileName;
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            writer.Write(rtxtBox.Text);
                        }
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(rtxtBox.Text);
                    }
                }
                Application.ExitThread();
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
            else
                Application.ExitThread();
        }

        private void frmNotepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtxtBox.Modified)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (filePath == "")
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            filePath = save.FileName;
                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                writer.Write(rtxtBox.Text);
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            writer.Write(rtxtBox.Text);
                        }
                    }
                    Application.ExitThread();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
                Application.ExitThread();
        }
        #endregion

        #region Edit
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtBox.Undo();
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtBox.SelectedText = "";
        }

        private void searchWithWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = rtxtBox.SelectedText;
            string searchUrl = "https://www.google.com/search?q=" + query;
            Process.Start(searchUrl);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string searchText = "";
            searchText = Microsoft.VisualBasic.Interaction.InputBox("Enter text to search:", "Find", "", -1, -1);
            if (searchText != "")
            {
                int index = rtxtBox.Text.IndexOf(searchText, StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0)
                {
                    rtxtBox.Select(index, searchText.Length);
                }
                else
                {
                    MessageBox.Show("The text was not found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        
    }
}