using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MJapVocab
{
    public partial class MJapVocabForm : Form
    {
        private List<IDisposable> outputCheckBoxesDisposables = new List<IDisposable>();

        private string[] lastResult;

        protected void DisposeOutputCheckBoxes(bool disposing)
        {
            if (disposing && (outputCheckBoxesDisposables != null))
            {
                foreach (var disposable in outputCheckBoxesDisposables)
                {
                    disposable.Dispose();
                }
            }
            outputCheckBoxesDisposables.Clear();
        }

        public MJapVocabForm()
        {
            InitializeComponent();
        }

        private void MJapVocabForm_Load(object sender, EventArgs e)
        {

        }

        private async void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // to supress sound
                e.Handled = true;
                e.SuppressKeyPress = true;

                DisposeOutputCheckBoxes(true);

                string[] result = await JishoWebAPIClient.RunAsync(inputTextBox.Text);
                lastResult = result;
                wordOutputLabel.Text = result[0];
                readingOutputLabel.Text = result[1];//String.Join(Environment.NewLine, result);

                // start at 1, go in steps of 25
                var startLocationY = 1;
                var stepLocationY = 25;
                for (int i = 2; i < result.Length; i++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.AutoSize = true;
                    checkBox.Location = new Point(12, startLocationY + (i - 2) * stepLocationY );
                    checkBox.Name = "outputCheckBox" + i;
                    checkBox.Size = new Size(83, 19);
                    checkBox.TabIndex = 3;
                    checkBox.Text = result[i];
                    checkBox.UseVisualStyleBackColor = true;
                    this.englishDefinitionsPanel.Controls.Add(checkBox);
                    outputCheckBoxesDisposables.Add(checkBox);
                }
            }
        }

        private void writeInKanaCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(writeInKanaCheckbox.Checked)
            {
                readingLabel.Hide();
                readingOutputLabel.Hide();
                wordOutputLabel.Text = lastResult[1];
            }
            else
            {
                readingLabel.Show();
                readingOutputLabel.Show();
                wordOutputLabel.Text = lastResult[0];
            }
        }
    }
}
