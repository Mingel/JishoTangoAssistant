
using System.Windows.Forms;

namespace MJapVocab
{
    partial class MJapVocabForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.englishDefinitionsGroupBox = new System.Windows.Forms.GroupBox();
            this.englishDefinitionsPanel = new System.Windows.Forms.Panel();
            this.wordLabel = new System.Windows.Forms.Label();
            this.readingLabel = new System.Windows.Forms.Label();
            this.readingOutputLabel = new System.Windows.Forms.Label();
            this.writeInKanaCheckbox = new System.Windows.Forms.CheckBox();
            this.copyToClipboardButton = new System.Windows.Forms.Button();
            this.additionalCommentsTextBox = new System.Windows.Forms.TextBox();
            this.wordComboBox = new System.Windows.Forms.ComboBox();
            this.otherFormsComboBox = new System.Windows.Forms.ComboBox();
            this.otherFormsLabel = new System.Windows.Forms.Label();
            this.enterButton = new System.Windows.Forms.Button();
            this.saveAllButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.elementsGridView = new System.Windows.Forms.DataGridView();
            this.deleteSelectionButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.upSelectionButton = new System.Windows.Forms.Button();
            this.downSelectionButton = new System.Windows.Forms.Button();
            this.exportCsvJapToEngButton = new System.Windows.Forms.Button();
            this.englishDefinitionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.elementsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(53, 6);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(650, 23);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(12, 9);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(35, 15);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "Input";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(12, 391);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(775, 110);
            this.outputTextBox.TabIndex = 2;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(12, 373);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(45, 15);
            this.outputLabel.TabIndex = 3;
            this.outputLabel.Text = "Output";
            // 
            // englishDefinitionsGroupBox
            // 
            this.englishDefinitionsGroupBox.Controls.Add(this.englishDefinitionsPanel);
            this.englishDefinitionsGroupBox.Location = new System.Drawing.Point(13, 77);
            this.englishDefinitionsGroupBox.Name = "englishDefinitionsGroupBox";
            this.englishDefinitionsGroupBox.Size = new System.Drawing.Size(775, 213);
            this.englishDefinitionsGroupBox.TabIndex = 4;
            this.englishDefinitionsGroupBox.TabStop = false;
            this.englishDefinitionsGroupBox.Text = "English Definitions";
            // 
            // englishDefinitionsPanel
            // 
            this.englishDefinitionsPanel.AutoScroll = true;
            this.englishDefinitionsPanel.Location = new System.Drawing.Point(6, 23);
            this.englishDefinitionsPanel.Name = "englishDefinitionsPanel";
            this.englishDefinitionsPanel.Size = new System.Drawing.Size(763, 184);
            this.englishDefinitionsPanel.TabIndex = 0;
            // 
            // wordLabel
            // 
            this.wordLabel.AutoSize = true;
            this.wordLabel.Location = new System.Drawing.Point(13, 48);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(36, 15);
            this.wordLabel.TabIndex = 5;
            this.wordLabel.Text = "Word";
            // 
            // readingLabel
            // 
            this.readingLabel.AutoSize = true;
            this.readingLabel.Location = new System.Drawing.Point(574, 48);
            this.readingLabel.Name = "readingLabel";
            this.readingLabel.Size = new System.Drawing.Size(50, 15);
            this.readingLabel.TabIndex = 6;
            this.readingLabel.Text = "Reading";
            // 
            // readingOutputLabel
            // 
            this.readingOutputLabel.AutoSize = true;
            this.readingOutputLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.readingOutputLabel.Location = new System.Drawing.Point(630, 41);
            this.readingOutputLabel.Name = "readingOutputLabel";
            this.readingOutputLabel.Size = new System.Drawing.Size(31, 30);
            this.readingOutputLabel.TabIndex = 8;
            this.readingOutputLabel.Text = "   ";
            // 
            // writeInKanaCheckbox
            // 
            this.writeInKanaCheckbox.AutoSize = true;
            this.writeInKanaCheckbox.Enabled = false;
            this.writeInKanaCheckbox.Location = new System.Drawing.Point(13, 351);
            this.writeInKanaCheckbox.Name = "writeInKanaCheckbox";
            this.writeInKanaCheckbox.Size = new System.Drawing.Size(96, 19);
            this.writeInKanaCheckbox.TabIndex = 9;
            this.writeInKanaCheckbox.Text = "Write in Kana";
            this.writeInKanaCheckbox.UseVisualStyleBackColor = true;
            this.writeInKanaCheckbox.CheckedChanged += new System.EventHandler(this.writeInKanaCheckbox_CheckedChanged);
            // 
            // copyToClipboardButton
            // 
            this.copyToClipboardButton.Location = new System.Drawing.Point(12, 507);
            this.copyToClipboardButton.Name = "copyToClipboardButton";
            this.copyToClipboardButton.Size = new System.Drawing.Size(775, 23);
            this.copyToClipboardButton.TabIndex = 10;
            this.copyToClipboardButton.Text = "Copy Output to Clipboard";
            this.copyToClipboardButton.UseVisualStyleBackColor = true;
            this.copyToClipboardButton.Click += new System.EventHandler(this.copyToClipboardButton_Click);
            // 
            // additionalCommentsTextBox
            // 
            this.additionalCommentsTextBox.Location = new System.Drawing.Point(13, 296);
            this.additionalCommentsTextBox.Multiline = true;
            this.additionalCommentsTextBox.Name = "additionalCommentsTextBox";
            this.additionalCommentsTextBox.PlaceholderText = "Additional Comments";
            this.additionalCommentsTextBox.Size = new System.Drawing.Size(774, 49);
            this.additionalCommentsTextBox.TabIndex = 11;
            this.additionalCommentsTextBox.TextChanged += new System.EventHandler(this.additionalCommentsTextBox_TextChanged);
            this.additionalCommentsTextBox.ScrollBars = ScrollBars.Vertical;
            // 
            // wordComboBox
            // 
            this.wordComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wordComboBox.Enabled = false;
            this.wordComboBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wordComboBox.FormattingEnabled = true;
            this.wordComboBox.Location = new System.Drawing.Point(53, 33);
            this.wordComboBox.Name = "wordComboBox";
            this.wordComboBox.Size = new System.Drawing.Size(121, 38);
            this.wordComboBox.TabIndex = 12;
            this.wordComboBox.SelectedIndexChanged += new System.EventHandler(this.wordComboBox_SelectedIndexChanged);
            // 
            // otherFormsComboBox
            // 
            this.otherFormsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.otherFormsComboBox.Enabled = false;
            this.otherFormsComboBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.otherFormsComboBox.FormattingEnabled = true;
            this.otherFormsComboBox.Location = new System.Drawing.Point(350, 33);
            this.otherFormsComboBox.Name = "otherFormsComboBox";
            this.otherFormsComboBox.Size = new System.Drawing.Size(121, 38);
            this.otherFormsComboBox.TabIndex = 14;
            this.otherFormsComboBox.SelectedIndexChanged += new System.EventHandler(this.otherFormsComboBox_SelectedIndexChanged);
            // 
            // otherFormsLabel
            // 
            this.otherFormsLabel.AutoSize = true;
            this.otherFormsLabel.Location = new System.Drawing.Point(271, 48);
            this.otherFormsLabel.Name = "otherFormsLabel";
            this.otherFormsLabel.Size = new System.Drawing.Size(73, 15);
            this.otherFormsLabel.TabIndex = 15;
            this.otherFormsLabel.Text = "Other Forms";
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(709, 6);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(75, 23);
            this.enterButton.TabIndex = 16;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // saveAllButton
            // 
            this.saveAllButton.Location = new System.Drawing.Point(791, 566);
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size(343, 23);
            this.saveAllButton.TabIndex = 17;
            this.saveAllButton.Text = "Save List";
            this.saveAllButton.UseVisualStyleBackColor = true;
            this.saveAllButton.Click += new System.EventHandler(this.saveAllButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 536);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(775, 23);
            this.addButton.TabIndex = 18;
            this.addButton.Text = "Add to List";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // elementsGridView
            // 
            this.elementsGridView.AllowUserToResizeRows = false;
            this.elementsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.elementsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.elementsGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.elementsGridView.Location = new System.Drawing.Point(791, 6);
            this.elementsGridView.MultiSelect = false;
            this.elementsGridView.Name = "elementsGridView";
            this.elementsGridView.ReadOnly = true;
            this.elementsGridView.RowHeadersVisible = false;
            this.elementsGridView.RowTemplate.Height = 25;
            this.elementsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.elementsGridView.Size = new System.Drawing.Size(343, 495);
            this.elementsGridView.TabIndex = 19;
            this.elementsGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // 
            // deleteSelectionButton
            // 
            this.deleteSelectionButton.Location = new System.Drawing.Point(791, 536);
            this.deleteSelectionButton.Name = "deleteSelectionButton";
            this.deleteSelectionButton.Size = new System.Drawing.Size(343, 23);
            this.deleteSelectionButton.TabIndex = 20;
            this.deleteSelectionButton.Text = "Delete Selection from List";
            this.deleteSelectionButton.UseVisualStyleBackColor = true;
            this.deleteSelectionButton.Click += new System.EventHandler(this.deleteSelectionButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(13, 566);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(774, 23);
            this.loadButton.TabIndex = 21;
            this.loadButton.Text = "Load List";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // upSelectionButton
            // 
            this.upSelectionButton.Location = new System.Drawing.Point(794, 508);
            this.upSelectionButton.Name = "upSelectionButton";
            this.upSelectionButton.Size = new System.Drawing.Size(176, 23);
            this.upSelectionButton.TabIndex = 22;
            this.upSelectionButton.Text = "↑";
            this.upSelectionButton.UseVisualStyleBackColor = true;
            this.upSelectionButton.Click += new System.EventHandler(this.upSelectionButton_Click);
            // 
            // downSelectionButton
            // 
            this.downSelectionButton.Location = new System.Drawing.Point(976, 508);
            this.downSelectionButton.Name = "downSelectionButton";
            this.downSelectionButton.Size = new System.Drawing.Size(158, 23);
            this.downSelectionButton.TabIndex = 23;
            this.downSelectionButton.Text = "↓";
            this.downSelectionButton.UseVisualStyleBackColor = true;
            this.downSelectionButton.Click += new System.EventHandler(this.downSelectionButton_Click);
            // 
            // exportCsvJapToEngButton
            // 
            this.exportCsvJapToEngButton.Location = new System.Drawing.Point(12, 596);
            this.exportCsvJapToEngButton.Name = "exportCsvJapToEngButton";
            this.exportCsvJapToEngButton.Size = new System.Drawing.Size(1122, 23);
            this.exportCsvJapToEngButton.TabIndex = 24;
            this.exportCsvJapToEngButton.Text = "Export to .csv (JP → EN)";
            this.exportCsvJapToEngButton.UseVisualStyleBackColor = true;
            this.exportCsvJapToEngButton.Click += new System.EventHandler(this.exportCsvJapToEngButton_Click);
            // 
            // MJapVocabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 632);
            this.Controls.Add(this.exportCsvJapToEngButton);
            this.Controls.Add(this.downSelectionButton);
            this.Controls.Add(this.upSelectionButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.deleteSelectionButton);
            this.Controls.Add(this.elementsGridView);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.saveAllButton);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.otherFormsComboBox);
            this.Controls.Add(this.wordComboBox);
            this.Controls.Add(this.otherFormsLabel);
            this.Controls.Add(this.additionalCommentsTextBox);
            this.Controls.Add(this.copyToClipboardButton);
            this.Controls.Add(this.writeInKanaCheckbox);
            this.Controls.Add(this.readingOutputLabel);
            this.Controls.Add(this.readingLabel);
            this.Controls.Add(this.wordLabel);
            this.Controls.Add(this.englishDefinitionsGroupBox);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.inputTextBox);
            this.Name = "MJapVocabForm";
            this.Text = "MJapVocab";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MJapVocabForm_FormClosing);
            this.Load += new System.EventHandler(this.MJapVocabForm_Load);
            this.englishDefinitionsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.elementsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label outputLabel;
        private GroupBox englishDefinitionsGroupBox;
        private Panel englishDefinitionsPanel;
        private Label wordLabel;
        private Label readingLabel;
        private Label readingOutputLabel;
        private CheckBox writeInKanaCheckbox;
        private Button copyToClipboardButton;
        private TextBox additionalCommentsTextBox;
        private ComboBox wordComboBox;
        private ComboBox otherFormsComboBox;
        private Label otherFormsLabel;
        private Button enterButton;
        private Button saveAllButton;
        private Button addButton;
        private DataGridView elementsGridView;
        private Button deleteSelectionButton;
        private Button loadButton;
        private Button upSelectionButton;
        private Button downSelectionButton;
        private Button exportCsvJapToEngButton;
    }
}

