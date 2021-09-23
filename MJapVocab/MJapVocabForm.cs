using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MJapVocab
{
    public partial class MJapVocabForm : Form
    {
        private List<CheckBox> outputCheckBoxes = new List<CheckBox>();
        private static bool running = false;

        private JishoDatum[] latestResult;

        public List<Element> addedElements = new List<Element>();
        public BindingList<Element> bindingAddedElements;

        private bool userMadeChanges = false;

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
            bindingAddedElements = new BindingList<Element>(addedElements);
            elementsGridView.DataSource = bindingAddedElements;
            UpdateElementsGridView();
        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
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
                    wordComboBox.Items.Clear();
                    wordComboBox.Enabled = false;
                    otherFormsComboBox.Items.Clear();
                    otherFormsComboBox.Enabled = false;
                    additionalCommentsTextBox.Text = String.Empty;
                    DisposeOutputCheckBoxes(true);
                    outputTextBox.Text = String.Empty;
                    running = false;
                    return;
                }

                latestResult = result;

                wordComboBox.Enabled = true;
                otherFormsComboBox.Enabled = true;

                wordComboBox.Items.Clear();
                additionalCommentsTextBox.Text = String.Empty;

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
            elementsGridView.DataSource = addedElements;
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

        private void addButton_Click(object sender, EventArgs e)
        {
            if (this.latestResult == null)
                return;
            var outputText = String.Empty;
            var englishDefinitionsString = String.Join("; ", outputCheckBoxes.Where(x => x.Checked).Select(x => x.Text));
            outputText += englishDefinitionsString;
            if (!string.IsNullOrWhiteSpace(additionalCommentsTextBox.Text) && !string.IsNullOrWhiteSpace(englishDefinitionsString))
            {
                outputText += Environment.NewLine;
            }
            outputText += additionalCommentsTextBox.Text;
            bool showReading = !writeInKanaCheckbox.Checked;
            var word = showReading ? otherFormsComboBox.SelectedItem.ToString() : readingOutputLabel.Text;
            Element elem = new Element(word, showReading, readingOutputLabel.Text, outputText);
            addedElements.Add(elem);
            userMadeChanges = true;
            UpdateElementsGridView();
            elementsGridView.Rows[elementsGridView.Rows.Count - 1].Selected = true;
        }

        private void UpdateElementsGridView()
        {
            // for some reason, ResetBindings() is not working sometimes TODO
            //bindingAddedElements.ResetBindings();
            // alternative:
            bindingAddedElements = new BindingList<Element>(addedElements);
            elementsGridView.DataSource = bindingAddedElements;

            elementsGridView.Columns["ShowReading"].Visible = false;
        }

        private void saveAllButton_Click(object sender, EventArgs e)
        {
            save();
        }

        private void save()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "MJV Files (*.mjv)|*.mjv";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.Write(Element.ListToJson(addedElements.ToArray()));
                    userMadeChanges = false;
                }
            }
        }

        private void deleteSelectionButton_Click(object sender, EventArgs e)
        {
            if (elementsGridView.SelectedRows.Count < 1)
                return;
            var result = MessageBox.Show("Do you really want to delete your selection?",
                                          "Deletion Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                addedElements.RemoveAt(elementsGridView.SelectedRows[0].Index);
                userMadeChanges = true;
                UpdateElementsGridView();
                elementsGridView.ClearSelection();
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "MJV Files (*.mjv)|*.mjv";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    var fileContent = reader.ReadToEnd();
                    var loadedElements = Element.JsonToList(fileContent);
                    addedElements.Clear();
                    addedElements.AddRange(loadedElements);
                    userMadeChanges = false;
                    UpdateElementsGridView();
                    elementsGridView.ClearSelection();
                }
            }
        }

        private void upSelectionButton_Click(object sender, EventArgs e)
        {
            if (elementsGridView.SelectedRows.Count < 1)
                return;
            var selectedIndex = elementsGridView.SelectedRows[0].Index;
            if (selectedIndex < 1)
                return;
            var tmp = addedElements[selectedIndex - 1];
            addedElements[selectedIndex - 1] = addedElements[selectedIndex];
            addedElements[selectedIndex] = tmp;
            userMadeChanges = true;
            UpdateElementsGridView();
            elementsGridView.Rows[selectedIndex - 1].Selected = true;
        }

        private void downSelectionButton_Click(object sender, EventArgs e)
        {
            if (elementsGridView.SelectedRows.Count < 1)
                return;
            var selectedIndex = elementsGridView.SelectedRows[0].Index;
            if (selectedIndex >= elementsGridView.Rows.Count - 1)
                return;
            var tmp = addedElements[selectedIndex + 1];
            addedElements[selectedIndex + 1] = addedElements[selectedIndex];
            addedElements[selectedIndex] = tmp;
            userMadeChanges = true;
            UpdateElementsGridView();
            elementsGridView.Rows[selectedIndex + 1].Selected = true;
        }

        private void MJapVocabForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (userMadeChanges)
            {
                var result = MessageBox.Show("You have made unsaved changes. Do you want to quit the application?",
                                          "Unsaved Changes",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void exportCsvJapToEngButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            exportFileDialog.RestoreDirectory = true;

            if (exportFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(exportFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.Write(Element.ListToJapEng(addedElements.ToArray()));
                }
            }
        }
    }
}
