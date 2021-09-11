
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
            this.wordOutputLabel = new System.Windows.Forms.Label();
            this.readingOutputLabel = new System.Windows.Forms.Label();
            this.writeInKanaCheckbox = new System.Windows.Forms.CheckBox();
            this.englishDefinitionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(12, 27);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(775, 23);
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
            this.outputTextBox.Location = new System.Drawing.Point(13, 332);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(775, 136);
            this.outputTextBox.TabIndex = 2;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(12, 314);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(45, 15);
            this.outputLabel.TabIndex = 3;
            this.outputLabel.Text = "Output";
            // 
            // englishDefinitionsGroupBox
            // 
            this.englishDefinitionsGroupBox.Controls.Add(this.englishDefinitionsPanel);
            this.englishDefinitionsGroupBox.Location = new System.Drawing.Point(13, 94);
            this.englishDefinitionsGroupBox.Name = "englishDefinitionsGroupBox";
            this.englishDefinitionsGroupBox.Size = new System.Drawing.Size(775, 217);
            this.englishDefinitionsGroupBox.TabIndex = 4;
            this.englishDefinitionsGroupBox.TabStop = false;
            this.englishDefinitionsGroupBox.Text = "English Definitions";
            // 
            // englishDefinitionsPanel
            // 
            this.englishDefinitionsPanel.AutoScroll = true;
            this.englishDefinitionsPanel.Location = new System.Drawing.Point(6, 23);
            this.englishDefinitionsPanel.Name = "englishDefinitionsPanel";
            this.englishDefinitionsPanel.Size = new System.Drawing.Size(763, 188);
            this.englishDefinitionsPanel.TabIndex = 0;
            // 
            // wordLabel
            // 
            this.wordLabel.AutoSize = true;
            this.wordLabel.Location = new System.Drawing.Point(12, 53);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(36, 15);
            this.wordLabel.TabIndex = 5;
            this.wordLabel.Text = "Word";
            // 
            // readingLabel
            // 
            this.readingLabel.AutoSize = true;
            this.readingLabel.Location = new System.Drawing.Point(12, 76);
            this.readingLabel.Name = "readingLabel";
            this.readingLabel.Size = new System.Drawing.Size(50, 15);
            this.readingLabel.TabIndex = 6;
            this.readingLabel.Text = "Reading";
            // 
            // wordOutputLabel
            // 
            this.wordOutputLabel.AutoSize = true;
            this.wordOutputLabel.Location = new System.Drawing.Point(78, 52);
            this.wordOutputLabel.Name = "wordOutputLabel";
            this.wordOutputLabel.Size = new System.Drawing.Size(0, 15);
            this.wordOutputLabel.TabIndex = 7;
            // 
            // readingOutputLabel
            // 
            this.readingOutputLabel.AutoSize = true;
            this.readingOutputLabel.Location = new System.Drawing.Point(78, 76);
            this.readingOutputLabel.Name = "readingOutputLabel";
            this.readingOutputLabel.Size = new System.Drawing.Size(0, 15);
            this.readingOutputLabel.TabIndex = 8;
            // 
            // writeInKanaCheckbox
            // 
            this.writeInKanaCheckbox.AutoSize = true;
            this.writeInKanaCheckbox.Location = new System.Drawing.Point(691, 53);
            this.writeInKanaCheckbox.Name = "writeInKanaCheckbox";
            this.writeInKanaCheckbox.Size = new System.Drawing.Size(96, 19);
            this.writeInKanaCheckbox.TabIndex = 9;
            this.writeInKanaCheckbox.Text = "Write in Kana";
            this.writeInKanaCheckbox.UseVisualStyleBackColor = true;
            this.writeInKanaCheckbox.CheckedChanged += new System.EventHandler(this.writeInKanaCheckbox_CheckedChanged);
            // 
            // MJapVocabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 478);
            this.Controls.Add(this.writeInKanaCheckbox);
            this.Controls.Add(this.readingOutputLabel);
            this.Controls.Add(this.wordOutputLabel);
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
        private Label wordOutputLabel;
        private Label readingOutputLabel;
        private CheckBox writeInKanaCheckbox;
    }
}

