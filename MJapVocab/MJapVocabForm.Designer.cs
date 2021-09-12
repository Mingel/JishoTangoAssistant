
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
            this.englishDefinitionsGroupBox.SuspendLayout();
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
            // MJapVocabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 542);
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
            this.Load += new System.EventHandler(this.MJapVocabForm_Load);
            this.englishDefinitionsGroupBox.ResumeLayout(false);
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
    }
}

