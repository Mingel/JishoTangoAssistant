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
        private List<CheckBox> outputCheckBoxes = new List<CheckBox>();
        private static bool running = false;

        private JishoDatum[] latestResult;

        protected void DisposeOutputCheckBoxes(bool disposing)
        {
            if (disposing && (outputCheckBoxes != null))
            {
                foreach (var disposable in outputCheckBoxes)
                {
                    disposable.Dispose();
                }
            }
            outputCheckBoxes.Clear();
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

                ProcessInput();
            }
        }

        private async void ProcessInput()
        {
            if (!running)
            {
                running = true;

                var result = await JishoWebAPIClient.RunAsync(inputTextBox.Text);
                if (result == null || result.Length == 0)
                {
                    readingOutputLabel.Text = String.Empty;
                    wordComboBox.Enabled = false;
                    otherFormsComboBox.Enabled = false;
                    running = false;
                    return;
                }

                latestResult = result;

                wordComboBox.Enabled = true;
                otherFormsComboBox.Enabled = true;

                wordComboBox.Items.Clear();

                var firstResult = result[0];
                var firstJapaneseEntry = firstResult.japanese[0];
                foreach (var res in result)
                {
                    if (res.japanese[0].word != null)
                        wordComboBox.Items.Add(res.japanese[0].word);
                    else
                        wordComboBox.Items.Add(res.japanese[0].reading);
                }
                if (wordComboBox.Items.Count > 0)
                {
                    wordComboBox.SelectedIndex = 0;
                }
                wordComboBox.Text = wordComboBox.SelectedItem.ToString();
                
                readingOutputLabel.Text = firstJapaneseEntry.reading;

                writeInKanaCheckbox.Enabled = firstJapaneseEntry.word != null;
                writeInKanaCheckbox.Checked = firstResult.senses.Where(x => x.tags.Contains("Usually written using kana alone")).Any() 
                    || firstJapaneseEntry.word == null;
                UpdateOutputTextbox();
                CreateEnglishDefinitionsCheckBoxes(firstResult);
                running = false;
            }
        }

        private void CreateEnglishDefinitionsCheckBoxes(JishoDatum datum)
        {
            DisposeOutputCheckBoxes(true);
            var startLocationX = 12;
            var totalStepLocationX = 0; // variable
            var startLocationY = 1;
            var stepLocationY = 25;
            for (int i = 0; i < datum.senses.Length; i++)
            {
                for (int j = 0; j < datum.senses[i].english_definitions.Length; j++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Location = new Point(startLocationX + totalStepLocationX, startLocationY + i * stepLocationY);
                    checkBox.Name = String.Format("outputCheckBox{0}-{1}", i, j);
                    checkBox.TabIndex = 3;
                    checkBox.Text = datum.senses[i].english_definitions[j];
                    checkBox.AutoSize = true;
                    checkBox.UseVisualStyleBackColor = true;
                    totalStepLocationX += checkBox.PreferredSize.Width + 10;
                    checkBox.CheckedChanged += new EventHandler(this.outputCheckBox_CheckedChanged);
                    this.englishDefinitionsPanel.Controls.Add(checkBox);
                    outputCheckBoxes.Add(checkBox);
                }
                totalStepLocationX = 0;
            }
        }

        private void writeInKanaCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOutputTextbox();
        }

        private void outputCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOutputTextbox();
        }

        private void UpdateOutputTextbox()
        {
            outputTextBox.Text = String.Empty;
            var englishDefinitionsString = String.Join("; ", outputCheckBoxes.Where(x => x.Checked).Select(x => x.Text));
            if (!writeInKanaCheckbox.Checked)
            {
                outputTextBox.Text += readingOutputLabel.Text;
                if (!string.IsNullOrWhiteSpace(englishDefinitionsString))
                {
                    outputTextBox.Text += Environment.NewLine;
                }
            }
            outputTextBox.Text += englishDefinitionsString;
            if (!string.IsNullOrWhiteSpace(additionalCommentsTextBox.Text))
            {
                outputTextBox.Text += Environment.NewLine;
            }
            outputTextBox.Text += additionalCommentsTextBox.Text;
        }

        private void copyToClipboardButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(outputTextBox.Text);
        }

        private void additionalCommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateOutputTextbox();
        }

        private void wordComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            writeInKanaCheckbox.Enabled = true;

            otherFormsComboBox.Items.Clear();
            var selectedDatum = this.latestResult[wordComboBox.SelectedIndex];
            foreach (var japItem in selectedDatum.japanese)
            {
                if (japItem.word != null)
                    otherFormsComboBox.Items.Add(japItem.word);
                else
                    otherFormsComboBox.Items.Add(japItem.reading);
            }
            if (otherFormsComboBox.Items.Count > 0)
            {
                otherFormsComboBox.SelectedIndex = 0;
            }

            readingOutputLabel.Text = selectedDatum.japanese[0].reading;

            CreateEnglishDefinitionsCheckBoxes(selectedDatum);

            writeInKanaCheckbox.Enabled = selectedDatum.japanese[0].word != null;
            writeInKanaCheckbox.Checked = selectedDatum.senses.Where(x => x.tags.Contains("Usually written using kana alone")).Any()
                || selectedDatum.japanese[0].word == null;

            UpdateOutputTextbox();
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            ProcessInput();
        }

        private void otherFormsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            readingOutputLabel.Text = this.latestResult[wordComboBox.SelectedIndex].japanese[otherFormsComboBox.SelectedIndex].reading;
            UpdateOutputTextbox();
        }
    }
}
