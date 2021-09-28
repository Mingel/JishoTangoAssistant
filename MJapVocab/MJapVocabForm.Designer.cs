
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
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.buttonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.leftButtonsPanel = new System.Windows.Forms.Panel();
            this.copyToClipboardButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.rightButtonsPanel = new System.Windows.Forms.Panel();
            this.UpDownButtonsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.downSelectionButton = new System.Windows.Forms.Button();
            this.upSelectionButton = new System.Windows.Forms.Button();
            this.saveAllButton = new System.Windows.Forms.Button();
            this.deleteSelectionButton = new System.Windows.Forms.Button();
            this.exportCsvJapToEngButton = new System.Windows.Forms.Button();
            this.userIOTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.userInputPanel = new System.Windows.Forms.Panel();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.inputLabel = new System.Windows.Forms.Label();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.enterButton = new System.Windows.Forms.Button();
            this.parentWordPanel = new System.Windows.Forms.Panel();
            this.wordTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.otherFormsPanel = new System.Windows.Forms.Panel();
            this.otherFormsLabel = new System.Windows.Forms.Label();
            this.otherFormsComboBox = new System.Windows.Forms.ComboBox();
            this.readingPanel = new System.Windows.Forms.Panel();
            this.readingOutputLabel = new System.Windows.Forms.Label();
            this.readingLabel = new System.Windows.Forms.Label();
            this.wordPanel = new System.Windows.Forms.Panel();
            this.wordLabel = new System.Windows.Forms.Label();
            this.wordComboBox = new System.Windows.Forms.ComboBox();
            this.outputPanel = new System.Windows.Forms.Panel();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.parentEnglishDefinitionsPanel = new System.Windows.Forms.Panel();
            this.englishDefinitionsGroupBox = new System.Windows.Forms.GroupBox();
            this.englishDefinitionsPanel = new System.Windows.Forms.Panel();
            this.writeInKanaPanel = new System.Windows.Forms.Panel();
            this.writeInKanaCheckbox = new System.Windows.Forms.CheckBox();
            this.additionalCommentsPanel = new System.Windows.Forms.Panel();
            this.additionalCommentsTextBox = new System.Windows.Forms.TextBox();
            this.vocabularyItemsPanel = new System.Windows.Forms.Panel();
            this.vocabularyItemsGridView = new System.Windows.Forms.DataGridView();
            this.windowTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.exportCsvEngToJapButton = new System.Windows.Forms.Button();
            this.buttonPanel.SuspendLayout();
            this.buttonTableLayoutPanel.SuspendLayout();
            this.leftButtonsPanel.SuspendLayout();
            this.rightButtonsPanel.SuspendLayout();
            this.UpDownButtonsTableLayoutPanel.SuspendLayout();
            this.userIOTableLayoutPanel.SuspendLayout();
            this.userInputPanel.SuspendLayout();
            this.inputPanel.SuspendLayout();
            this.parentWordPanel.SuspendLayout();
            this.wordTableLayoutPanel.SuspendLayout();
            this.otherFormsPanel.SuspendLayout();
            this.readingPanel.SuspendLayout();
            this.wordPanel.SuspendLayout();
            this.outputPanel.SuspendLayout();
            this.outputGroupBox.SuspendLayout();
            this.parentEnglishDefinitionsPanel.SuspendLayout();
            this.englishDefinitionsGroupBox.SuspendLayout();
            this.writeInKanaPanel.SuspendLayout();
            this.additionalCommentsPanel.SuspendLayout();
            this.vocabularyItemsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vocabularyItemsGridView)).BeginInit();
            this.windowTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.exportCsvEngToJapButton);
            this.buttonPanel.Controls.Add(this.buttonTableLayoutPanel);
            this.buttonPanel.Controls.Add(this.exportCsvJapToEngButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(3, 494);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(978, 164);
            this.buttonPanel.TabIndex = 31;
            // 
            // buttonTableLayoutPanel
            // 
            this.buttonTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTableLayoutPanel.ColumnCount = 2;
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.buttonTableLayoutPanel.Controls.Add(this.leftButtonsPanel, 0, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.rightButtonsPanel, 1, 0);
            this.buttonTableLayoutPanel.Location = new System.Drawing.Point(0, 3);
            this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            this.buttonTableLayoutPanel.RowCount = 1;
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonTableLayoutPanel.Size = new System.Drawing.Size(975, 93);
            this.buttonTableLayoutPanel.TabIndex = 20;
            // 
            // leftButtonsPanel
            // 
            this.leftButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.leftButtonsPanel.Controls.Add(this.copyToClipboardButton);
            this.leftButtonsPanel.Controls.Add(this.loadButton);
            this.leftButtonsPanel.Controls.Add(this.addButton);
            this.leftButtonsPanel.Location = new System.Drawing.Point(3, 3);
            this.leftButtonsPanel.Name = "leftButtonsPanel";
            this.leftButtonsPanel.Size = new System.Drawing.Size(676, 87);
            this.leftButtonsPanel.TabIndex = 0;
            // 
            // copyToClipboardButton
            // 
            this.copyToClipboardButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyToClipboardButton.Location = new System.Drawing.Point(0, 6);
            this.copyToClipboardButton.Name = "copyToClipboardButton";
            this.copyToClipboardButton.Size = new System.Drawing.Size(676, 23);
            this.copyToClipboardButton.TabIndex = 10;
            this.copyToClipboardButton.Text = "Copy Output to Clipboard";
            this.copyToClipboardButton.UseVisualStyleBackColor = true;
            this.copyToClipboardButton.Click += new System.EventHandler(this.copyToClipboardButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(0, 64);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(676, 23);
            this.loadButton.TabIndex = 21;
            this.loadButton.Text = "Load List";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(0, 35);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(676, 23);
            this.addButton.TabIndex = 18;
            this.addButton.Text = "Add to List";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // rightButtonsPanel
            // 
            this.rightButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightButtonsPanel.Controls.Add(this.UpDownButtonsTableLayoutPanel);
            this.rightButtonsPanel.Controls.Add(this.saveAllButton);
            this.rightButtonsPanel.Controls.Add(this.deleteSelectionButton);
            this.rightButtonsPanel.Location = new System.Drawing.Point(685, 3);
            this.rightButtonsPanel.Name = "rightButtonsPanel";
            this.rightButtonsPanel.Size = new System.Drawing.Size(287, 87);
            this.rightButtonsPanel.TabIndex = 1;
            // 
            // UpDownButtonsTableLayoutPanel
            // 
            this.UpDownButtonsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpDownButtonsTableLayoutPanel.ColumnCount = 2;
            this.UpDownButtonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UpDownButtonsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UpDownButtonsTableLayoutPanel.Controls.Add(this.downSelectionButton, 1, 0);
            this.UpDownButtonsTableLayoutPanel.Controls.Add(this.upSelectionButton, 0, 0);
            this.UpDownButtonsTableLayoutPanel.Location = new System.Drawing.Point(2, 3);
            this.UpDownButtonsTableLayoutPanel.Name = "UpDownButtonsTableLayoutPanel";
            this.UpDownButtonsTableLayoutPanel.RowCount = 1;
            this.UpDownButtonsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UpDownButtonsTableLayoutPanel.Size = new System.Drawing.Size(282, 29);
            this.UpDownButtonsTableLayoutPanel.TabIndex = 21;
            // 
            // downSelectionButton
            // 
            this.downSelectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.downSelectionButton.Location = new System.Drawing.Point(144, 3);
            this.downSelectionButton.Name = "downSelectionButton";
            this.downSelectionButton.Size = new System.Drawing.Size(135, 23);
            this.downSelectionButton.TabIndex = 23;
            this.downSelectionButton.Text = "↓";
            this.downSelectionButton.UseVisualStyleBackColor = true;
            this.downSelectionButton.Click += new System.EventHandler(this.downSelectionButton_Click);
            // 
            // upSelectionButton
            // 
            this.upSelectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.upSelectionButton.Location = new System.Drawing.Point(3, 3);
            this.upSelectionButton.Name = "upSelectionButton";
            this.upSelectionButton.Size = new System.Drawing.Size(135, 23);
            this.upSelectionButton.TabIndex = 22;
            this.upSelectionButton.Text = "↑";
            this.upSelectionButton.UseVisualStyleBackColor = true;
            this.upSelectionButton.Click += new System.EventHandler(this.upSelectionButton_Click);
            // 
            // saveAllButton
            // 
            this.saveAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAllButton.Location = new System.Drawing.Point(5, 64);
            this.saveAllButton.Name = "saveAllButton";
            this.saveAllButton.Size = new System.Drawing.Size(276, 23);
            this.saveAllButton.TabIndex = 17;
            this.saveAllButton.Text = "Save List";
            this.saveAllButton.UseVisualStyleBackColor = true;
            this.saveAllButton.Click += new System.EventHandler(this.saveAllButton_Click);
            // 
            // deleteSelectionButton
            // 
            this.deleteSelectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSelectionButton.Location = new System.Drawing.Point(5, 35);
            this.deleteSelectionButton.Name = "deleteSelectionButton";
            this.deleteSelectionButton.Size = new System.Drawing.Size(276, 23);
            this.deleteSelectionButton.TabIndex = 20;
            this.deleteSelectionButton.Text = "Delete Selection from List";
            this.deleteSelectionButton.UseVisualStyleBackColor = true;
            this.deleteSelectionButton.Click += new System.EventHandler(this.deleteSelectionButton_Click);
            // 
            // exportCsvJapToEngButton
            // 
            this.exportCsvJapToEngButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportCsvJapToEngButton.Location = new System.Drawing.Point(3, 99);
            this.exportCsvJapToEngButton.Name = "exportCsvJapToEngButton";
            this.exportCsvJapToEngButton.Size = new System.Drawing.Size(963, 23);
            this.exportCsvJapToEngButton.TabIndex = 24;
            this.exportCsvJapToEngButton.Text = "Export to .csv (JP → EN)";
            this.exportCsvJapToEngButton.UseVisualStyleBackColor = true;
            this.exportCsvJapToEngButton.Click += new System.EventHandler(this.exportCsvJapToEngButton_Click);
            // 
            // userIOTableLayoutPanel
            // 
            this.userIOTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userIOTableLayoutPanel.ColumnCount = 2;
            this.userIOTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.userIOTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.userIOTableLayoutPanel.Controls.Add(this.userInputPanel, 0, 0);
            this.userIOTableLayoutPanel.Controls.Add(this.vocabularyItemsPanel, 1, 0);
            this.userIOTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.userIOTableLayoutPanel.Name = "userIOTableLayoutPanel";
            this.userIOTableLayoutPanel.RowCount = 1;
            this.userIOTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.userIOTableLayoutPanel.Size = new System.Drawing.Size(978, 485);
            this.userIOTableLayoutPanel.TabIndex = 33;
            // 
            // userInputPanel
            // 
            this.userInputPanel.Controls.Add(this.inputPanel);
            this.userInputPanel.Controls.Add(this.parentWordPanel);
            this.userInputPanel.Controls.Add(this.outputPanel);
            this.userInputPanel.Controls.Add(this.parentEnglishDefinitionsPanel);
            this.userInputPanel.Controls.Add(this.writeInKanaPanel);
            this.userInputPanel.Controls.Add(this.additionalCommentsPanel);
            this.userInputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userInputPanel.Location = new System.Drawing.Point(3, 3);
            this.userInputPanel.Name = "userInputPanel";
            this.userInputPanel.Size = new System.Drawing.Size(678, 479);
            this.userInputPanel.TabIndex = 20;
            // 
            // inputPanel
            // 
            this.inputPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputPanel.Controls.Add(this.inputLabel);
            this.inputPanel.Controls.Add(this.inputTextBox);
            this.inputPanel.Controls.Add(this.enterButton);
            this.inputPanel.Location = new System.Drawing.Point(3, 3);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(672, 23);
            this.inputPanel.TabIndex = 25;
            // 
            // inputLabel
            // 
            this.inputLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(2, 3);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(35, 15);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "Input";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inputTextBox.Location = new System.Drawing.Point(41, 0);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(551, 23);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputTextBox_KeyDown);
            // 
            // enterButton
            // 
            this.enterButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.enterButton.Location = new System.Drawing.Point(598, 0);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(75, 23);
            this.enterButton.TabIndex = 16;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // parentWordPanel
            // 
            this.parentWordPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parentWordPanel.Controls.Add(this.wordTableLayoutPanel);
            this.parentWordPanel.Location = new System.Drawing.Point(3, 32);
            this.parentWordPanel.Name = "parentWordPanel";
            this.parentWordPanel.Size = new System.Drawing.Size(672, 49);
            this.parentWordPanel.TabIndex = 26;
            // 
            // wordTableLayoutPanel
            // 
            this.wordTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wordTableLayoutPanel.ColumnCount = 3;
            this.wordTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.wordTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.wordTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.wordTableLayoutPanel.Controls.Add(this.otherFormsPanel, 1, 0);
            this.wordTableLayoutPanel.Controls.Add(this.readingPanel, 2, 0);
            this.wordTableLayoutPanel.Controls.Add(this.wordPanel, 0, 0);
            this.wordTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.wordTableLayoutPanel.Name = "wordTableLayoutPanel";
            this.wordTableLayoutPanel.RowCount = 1;
            this.wordTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.wordTableLayoutPanel.Size = new System.Drawing.Size(672, 49);
            this.wordTableLayoutPanel.TabIndex = 0;
            // 
            // otherFormsPanel
            // 
            this.otherFormsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.otherFormsPanel.Controls.Add(this.otherFormsLabel);
            this.otherFormsPanel.Controls.Add(this.otherFormsComboBox);
            this.otherFormsPanel.Location = new System.Drawing.Point(224, 0);
            this.otherFormsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.otherFormsPanel.Name = "otherFormsPanel";
            this.otherFormsPanel.Size = new System.Drawing.Size(224, 49);
            this.otherFormsPanel.TabIndex = 1;
            // 
            // otherFormsLabel
            // 
            this.otherFormsLabel.AutoSize = true;
            this.otherFormsLabel.Location = new System.Drawing.Point(3, 17);
            this.otherFormsLabel.Name = "otherFormsLabel";
            this.otherFormsLabel.Size = new System.Drawing.Size(73, 15);
            this.otherFormsLabel.TabIndex = 15;
            this.otherFormsLabel.Text = "Other Forms";
            // 
            // otherFormsComboBox
            // 
            this.otherFormsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.otherFormsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.otherFormsComboBox.Enabled = false;
            this.otherFormsComboBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.otherFormsComboBox.FormattingEnabled = true;
            this.otherFormsComboBox.Location = new System.Drawing.Point(82, 5);
            this.otherFormsComboBox.Name = "otherFormsComboBox";
            this.otherFormsComboBox.Size = new System.Drawing.Size(75, 38);
            this.otherFormsComboBox.TabIndex = 14;
            this.otherFormsComboBox.SelectedIndexChanged += new System.EventHandler(this.otherFormsComboBox_SelectedIndexChanged);
            // 
            // readingPanel
            // 
            this.readingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readingPanel.Controls.Add(this.readingOutputLabel);
            this.readingPanel.Controls.Add(this.readingLabel);
            this.readingPanel.Location = new System.Drawing.Point(448, 0);
            this.readingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.readingPanel.Name = "readingPanel";
            this.readingPanel.Size = new System.Drawing.Size(224, 49);
            this.readingPanel.TabIndex = 2;
            // 
            // readingOutputLabel
            // 
            this.readingOutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.readingOutputLabel.AutoSize = true;
            this.readingOutputLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.readingOutputLabel.Location = new System.Drawing.Point(59, 8);
            this.readingOutputLabel.Name = "readingOutputLabel";
            this.readingOutputLabel.Size = new System.Drawing.Size(31, 30);
            this.readingOutputLabel.TabIndex = 8;
            this.readingOutputLabel.Text = "   ";
            // 
            // readingLabel
            // 
            this.readingLabel.AutoSize = true;
            this.readingLabel.Location = new System.Drawing.Point(3, 17);
            this.readingLabel.Name = "readingLabel";
            this.readingLabel.Size = new System.Drawing.Size(50, 15);
            this.readingLabel.TabIndex = 6;
            this.readingLabel.Text = "Reading";
            // 
            // wordPanel
            // 
            this.wordPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wordPanel.Controls.Add(this.wordLabel);
            this.wordPanel.Controls.Add(this.wordComboBox);
            this.wordPanel.Location = new System.Drawing.Point(0, 0);
            this.wordPanel.Margin = new System.Windows.Forms.Padding(0);
            this.wordPanel.Name = "wordPanel";
            this.wordPanel.Size = new System.Drawing.Size(224, 49);
            this.wordPanel.TabIndex = 0;
            // 
            // wordLabel
            // 
            this.wordLabel.AutoSize = true;
            this.wordLabel.Location = new System.Drawing.Point(3, 17);
            this.wordLabel.Name = "wordLabel";
            this.wordLabel.Size = new System.Drawing.Size(36, 15);
            this.wordLabel.TabIndex = 5;
            this.wordLabel.Text = "Word";
            // 
            // wordComboBox
            // 
            this.wordComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wordComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wordComboBox.Enabled = false;
            this.wordComboBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wordComboBox.FormattingEnabled = true;
            this.wordComboBox.Location = new System.Drawing.Point(45, 5);
            this.wordComboBox.Name = "wordComboBox";
            this.wordComboBox.Size = new System.Drawing.Size(69, 38);
            this.wordComboBox.TabIndex = 12;
            this.wordComboBox.SelectedIndexChanged += new System.EventHandler(this.wordComboBox_SelectedIndexChanged);
            // 
            // outputPanel
            // 
            this.outputPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPanel.Controls.Add(this.outputGroupBox);
            this.outputPanel.Location = new System.Drawing.Point(3, 337);
            this.outputPanel.Name = "outputPanel";
            this.outputPanel.Size = new System.Drawing.Size(675, 139);
            this.outputPanel.TabIndex = 30;
            // 
            // outputGroupBox
            // 
            this.outputGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputGroupBox.Controls.Add(this.outputTextBox);
            this.outputGroupBox.Location = new System.Drawing.Point(5, 3);
            this.outputGroupBox.Name = "outputGroupBox";
            this.outputGroupBox.Size = new System.Drawing.Size(667, 136);
            this.outputGroupBox.TabIndex = 20;
            this.outputGroupBox.TabStop = false;
            this.outputGroupBox.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTextBox.Location = new System.Drawing.Point(3, 19);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(658, 114);
            this.outputTextBox.TabIndex = 2;
            // 
            // parentEnglishDefinitionsPanel
            // 
            this.parentEnglishDefinitionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parentEnglishDefinitionsPanel.Controls.Add(this.englishDefinitionsGroupBox);
            this.parentEnglishDefinitionsPanel.Location = new System.Drawing.Point(3, 84);
            this.parentEnglishDefinitionsPanel.Name = "parentEnglishDefinitionsPanel";
            this.parentEnglishDefinitionsPanel.Size = new System.Drawing.Size(672, 158);
            this.parentEnglishDefinitionsPanel.TabIndex = 27;
            // 
            // englishDefinitionsGroupBox
            // 
            this.englishDefinitionsGroupBox.Controls.Add(this.englishDefinitionsPanel);
            this.englishDefinitionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.englishDefinitionsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.englishDefinitionsGroupBox.Name = "englishDefinitionsGroupBox";
            this.englishDefinitionsGroupBox.Size = new System.Drawing.Size(672, 158);
            this.englishDefinitionsGroupBox.TabIndex = 4;
            this.englishDefinitionsGroupBox.TabStop = false;
            this.englishDefinitionsGroupBox.Text = "English Definitions";
            // 
            // englishDefinitionsPanel
            // 
            this.englishDefinitionsPanel.AutoScroll = true;
            this.englishDefinitionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.englishDefinitionsPanel.Location = new System.Drawing.Point(3, 19);
            this.englishDefinitionsPanel.Name = "englishDefinitionsPanel";
            this.englishDefinitionsPanel.Size = new System.Drawing.Size(666, 136);
            this.englishDefinitionsPanel.TabIndex = 0;
            // 
            // writeInKanaPanel
            // 
            this.writeInKanaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.writeInKanaPanel.Controls.Add(this.writeInKanaCheckbox);
            this.writeInKanaPanel.Location = new System.Drawing.Point(3, 309);
            this.writeInKanaPanel.Name = "writeInKanaPanel";
            this.writeInKanaPanel.Size = new System.Drawing.Size(675, 25);
            this.writeInKanaPanel.TabIndex = 29;
            // 
            // writeInKanaCheckbox
            // 
            this.writeInKanaCheckbox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.writeInKanaCheckbox.AutoSize = true;
            this.writeInKanaCheckbox.Enabled = false;
            this.writeInKanaCheckbox.Location = new System.Drawing.Point(3, 3);
            this.writeInKanaCheckbox.Name = "writeInKanaCheckbox";
            this.writeInKanaCheckbox.Size = new System.Drawing.Size(96, 19);
            this.writeInKanaCheckbox.TabIndex = 9;
            this.writeInKanaCheckbox.Text = "Write in Kana";
            this.writeInKanaCheckbox.UseVisualStyleBackColor = true;
            this.writeInKanaCheckbox.CheckedChanged += new System.EventHandler(this.writeInKanaCheckbox_CheckedChanged);
            // 
            // additionalCommentsPanel
            // 
            this.additionalCommentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.additionalCommentsPanel.Controls.Add(this.additionalCommentsTextBox);
            this.additionalCommentsPanel.Location = new System.Drawing.Point(3, 248);
            this.additionalCommentsPanel.Name = "additionalCommentsPanel";
            this.additionalCommentsPanel.Size = new System.Drawing.Size(675, 58);
            this.additionalCommentsPanel.TabIndex = 28;
            // 
            // additionalCommentsTextBox
            // 
            this.additionalCommentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.additionalCommentsTextBox.Location = new System.Drawing.Point(0, 0);
            this.additionalCommentsTextBox.Multiline = true;
            this.additionalCommentsTextBox.Name = "additionalCommentsTextBox";
            this.additionalCommentsTextBox.PlaceholderText = "Additional Comments";
            this.additionalCommentsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.additionalCommentsTextBox.Size = new System.Drawing.Size(675, 58);
            this.additionalCommentsTextBox.TabIndex = 11;
            this.additionalCommentsTextBox.TextChanged += new System.EventHandler(this.additionalCommentsTextBox_TextChanged);
            // 
            // vocabularyItemsPanel
            // 
            this.vocabularyItemsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vocabularyItemsPanel.Controls.Add(this.vocabularyItemsGridView);
            this.vocabularyItemsPanel.Location = new System.Drawing.Point(687, 3);
            this.vocabularyItemsPanel.Name = "vocabularyItemsPanel";
            this.vocabularyItemsPanel.Size = new System.Drawing.Size(288, 479);
            this.vocabularyItemsPanel.TabIndex = 32;
            // 
            // vocabularyItemsGridView
            // 
            this.vocabularyItemsGridView.AllowUserToResizeRows = false;
            this.vocabularyItemsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vocabularyItemsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.vocabularyItemsGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.vocabularyItemsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.vocabularyItemsGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.vocabularyItemsGridView.Location = new System.Drawing.Point(3, 3);
            this.vocabularyItemsGridView.MultiSelect = false;
            this.vocabularyItemsGridView.Name = "vocabularyItemsGridView";
            this.vocabularyItemsGridView.ReadOnly = true;
            this.vocabularyItemsGridView.RowHeadersVisible = false;
            this.vocabularyItemsGridView.RowTemplate.Height = 25;
            this.vocabularyItemsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.vocabularyItemsGridView.Size = new System.Drawing.Size(282, 473);
            this.vocabularyItemsGridView.TabIndex = 19;
            // 
            // windowTableLayoutPanel
            // 
            this.windowTableLayoutPanel.ColumnCount = 1;
            this.windowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.windowTableLayoutPanel.Controls.Add(this.userIOTableLayoutPanel, 0, 0);
            this.windowTableLayoutPanel.Controls.Add(this.buttonPanel, 0, 1);
            this.windowTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.windowTableLayoutPanel.Name = "windowTableLayoutPanel";
            this.windowTableLayoutPanel.RowCount = 2;
            this.windowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.windowTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.windowTableLayoutPanel.Size = new System.Drawing.Size(984, 661);
            this.windowTableLayoutPanel.TabIndex = 34;
            // 
            // exportCsvEngToJapButton
            // 
            this.exportCsvEngToJapButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportCsvEngToJapButton.Location = new System.Drawing.Point(3, 128);
            this.exportCsvEngToJapButton.Name = "exportCsvEngToJapButton";
            this.exportCsvEngToJapButton.Size = new System.Drawing.Size(963, 23);
            this.exportCsvEngToJapButton.TabIndex = 25;
            this.exportCsvEngToJapButton.Text = "Export to .csv (EN → JP)";
            this.exportCsvEngToJapButton.UseVisualStyleBackColor = true;
            this.exportCsvEngToJapButton.Click += new System.EventHandler(this.exportCsvEngToJapButton_Click);
            // 
            // MJapVocabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.windowTableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MJapVocabForm";
            this.Text = "MJapVocab";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MJapVocabForm_FormClosing);
            this.Load += new System.EventHandler(this.MJapVocabForm_Load);
            this.buttonPanel.ResumeLayout(false);
            this.buttonTableLayoutPanel.ResumeLayout(false);
            this.leftButtonsPanel.ResumeLayout(false);
            this.rightButtonsPanel.ResumeLayout(false);
            this.UpDownButtonsTableLayoutPanel.ResumeLayout(false);
            this.userIOTableLayoutPanel.ResumeLayout(false);
            this.userInputPanel.ResumeLayout(false);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.parentWordPanel.ResumeLayout(false);
            this.wordTableLayoutPanel.ResumeLayout(false);
            this.otherFormsPanel.ResumeLayout(false);
            this.otherFormsPanel.PerformLayout();
            this.readingPanel.ResumeLayout(false);
            this.readingPanel.PerformLayout();
            this.wordPanel.ResumeLayout(false);
            this.wordPanel.PerformLayout();
            this.outputPanel.ResumeLayout(false);
            this.outputGroupBox.ResumeLayout(false);
            this.outputGroupBox.PerformLayout();
            this.parentEnglishDefinitionsPanel.ResumeLayout(false);
            this.englishDefinitionsGroupBox.ResumeLayout(false);
            this.writeInKanaPanel.ResumeLayout(false);
            this.writeInKanaPanel.PerformLayout();
            this.additionalCommentsPanel.ResumeLayout(false);
            this.additionalCommentsPanel.PerformLayout();
            this.vocabularyItemsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vocabularyItemsGridView)).EndInit();
            this.windowTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel buttonPanel;
        private TableLayoutPanel buttonTableLayoutPanel;
        private Panel leftButtonsPanel;
        private Button copyToClipboardButton;
        private Button loadButton;
        private Button addButton;
        private Panel rightButtonsPanel;
        private TableLayoutPanel UpDownButtonsTableLayoutPanel;
        private Button downSelectionButton;
        private Button upSelectionButton;
        private Button saveAllButton;
        private Button deleteSelectionButton;
        private Button exportCsvJapToEngButton;
        private TableLayoutPanel userIOTableLayoutPanel;
        private Panel vocabularyItemsPanel;
        private DataGridView vocabularyItemsGridView;
        private Panel userInputPanel;
        private Panel inputPanel;
        private Label inputLabel;
        private TextBox inputTextBox;
        private Button enterButton;
        private Panel parentWordPanel;
        private Label wordLabel;
        private Label readingLabel;
        private Label readingOutputLabel;
        private Label otherFormsLabel;
        private ComboBox wordComboBox;
        private ComboBox otherFormsComboBox;
        private Panel outputPanel;
        private GroupBox outputGroupBox;
        private TextBox outputTextBox;
        private Panel parentEnglishDefinitionsPanel;
        private GroupBox englishDefinitionsGroupBox;
        private Panel englishDefinitionsPanel;
        private Panel writeInKanaPanel;
        private CheckBox writeInKanaCheckbox;
        private Panel additionalCommentsPanel;
        private TextBox additionalCommentsTextBox;
        private TableLayoutPanel windowTableLayoutPanel;
        private TableLayoutPanel wordTableLayoutPanel;
        private Panel otherFormsPanel;
        private Panel readingPanel;
        private Panel wordPanel;
        private Button exportCsvEngToJapButton;
    }
}

