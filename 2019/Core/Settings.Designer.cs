namespace CppBrowser.Core
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backColor2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backColor1 = new System.Windows.Forms.TextBox();
            this.selectionColor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.resetColors = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFont = new System.Windows.Forms.Button();
            this.fontBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.findColor = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkFindOnlyName = new System.Windows.Forms.CheckBox();
            this.checkCurrentItem = new System.Windows.Forms.CheckBox();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.comboPosition = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkCaseSensitiveFind = new System.Windows.Forms.CheckBox();
            this.editY = new NumericEditBox();
            this.editX = new NumericEditBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.backColor2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.backColor1);
            this.groupBox1.Location = new System.Drawing.Point(6, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 76);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Background colors";
            // 
            // backColor2
            // 
            this.backColor2.Cursor = System.Windows.Forms.Cursors.Default;
            this.backColor2.Location = new System.Drawing.Point(90, 45);
            this.backColor2.Name = "backColor2";
            this.backColor2.ReadOnly = true;
            this.backColor2.Size = new System.Drawing.Size(45, 20);
            this.backColor2.TabIndex = 3;
            this.backColor2.Click += new System.EventHandler(this.backColor2_Click);
            this.backColor2.Enter += new System.EventHandler(this.backColor2_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Color2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Color1";
            // 
            // backColor1
            // 
            this.backColor1.Cursor = System.Windows.Forms.Cursors.Default;
            this.backColor1.Location = new System.Drawing.Point(90, 19);
            this.backColor1.Name = "backColor1";
            this.backColor1.ReadOnly = true;
            this.backColor1.Size = new System.Drawing.Size(45, 20);
            this.backColor1.TabIndex = 0;
            this.backColor1.Click += new System.EventHandler(this.backColor1_Click);
            this.backColor1.Enter += new System.EventHandler(this.backColor1_Enter);
            // 
            // selectionColor
            // 
            this.selectionColor.Cursor = System.Windows.Forms.Cursors.Default;
            this.selectionColor.Location = new System.Drawing.Point(90, 19);
            this.selectionColor.Name = "selectionColor";
            this.selectionColor.ReadOnly = true;
            this.selectionColor.Size = new System.Drawing.Size(45, 20);
            this.selectionColor.TabIndex = 5;
            this.selectionColor.Click += new System.EventHandler(this.selectedColor_Click);
            this.selectionColor.Enter += new System.EventHandler(this.selectedColor_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Selection color";
            // 
            // resetColors
            // 
            this.resetColors.Location = new System.Drawing.Point(6, 221);
            this.resetColors.Name = "resetColors";
            this.resetColors.Size = new System.Drawing.Size(150, 23);
            this.resetColors.TabIndex = 4;
            this.resetColors.Text = "Reset To Defaults";
            this.resetColors.UseVisualStyleBackColor = true;
            this.resetColors.Click += new System.EventHandler(this.resetColors_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkBox,
            this.type,
            this.color});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(287, 294);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // checkBox
            // 
            this.checkBox.HeaderText = "Show";
            this.checkBox.Name = "checkBox";
            this.checkBox.Width = 50;
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.type.HeaderText = "Type";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Width = 56;
            // 
            // color
            // 
            this.color.HeaderText = "Color";
            this.color.Name = "color";
            this.color.ReadOnly = true;
            this.color.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.color.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.color.Width = 50;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(301, 326);
            this.tabControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(293, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Displayed items";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.resetColors);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(293, 300);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Font & Colors";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnFont);
            this.groupBox3.Controls.Add(this.fontBox);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(6, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(281, 43);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Font";
            // 
            // btnFont
            // 
            this.btnFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFont.Location = new System.Drawing.Point(250, 14);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(25, 19);
            this.btnFont.TabIndex = 2;
            this.btnFont.Text = "...";
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // fontBox
            // 
            this.fontBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fontBox.Location = new System.Drawing.Point(76, 12);
            this.fontBox.Name = "fontBox";
            this.fontBox.ReadOnly = true;
            this.fontBox.Size = new System.Drawing.Size(168, 20);
            this.fontBox.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Listbox font";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.findColor);
            this.groupBox2.Controls.Add(this.selectionColor);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 77);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Highlight colors";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Find color";
            // 
            // findColor
            // 
            this.findColor.Cursor = System.Windows.Forms.Cursors.Default;
            this.findColor.Location = new System.Drawing.Point(90, 46);
            this.findColor.Name = "findColor";
            this.findColor.ReadOnly = true;
            this.findColor.Size = new System.Drawing.Size(45, 20);
            this.findColor.TabIndex = 6;
            this.findColor.Click += new System.EventHandler(this.findColor_Click);
            this.findColor.Enter += new System.EventHandler(this.findColor_Enter);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkFindOnlyName);
            this.tabPage3.Controls.Add(this.checkCurrentItem);
            this.tabPage3.Controls.Add(this.labelY);
            this.tabPage3.Controls.Add(this.labelX);
            this.tabPage3.Controls.Add(this.comboPosition);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.checkCaseSensitiveFind);
            this.tabPage3.Controls.Add(this.editY);
            this.tabPage3.Controls.Add(this.editX);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(293, 300);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Misc";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkFindOnlyName
            // 
            this.checkFindOnlyName.AutoSize = true;
            this.checkFindOnlyName.Location = new System.Drawing.Point(9, 57);
            this.checkFindOnlyName.Name = "checkFindOnlyName";
            this.checkFindOnlyName.Size = new System.Drawing.Size(254, 17);
            this.checkFindOnlyName.TabIndex = 10;
            this.checkFindOnlyName.Text = "Find only in name (only if qualified names shown)";
            this.checkFindOnlyName.UseVisualStyleBackColor = true;
            // 
            // checkCurrentItem
            // 
            this.checkCurrentItem.AutoSize = true;
            this.checkCurrentItem.Location = new System.Drawing.Point(9, 33);
            this.checkCurrentItem.Name = "checkCurrentItem";
            this.checkCurrentItem.Size = new System.Drawing.Size(114, 17);
            this.checkCurrentItem.TabIndex = 9;
            this.checkCurrentItem.Text = "Select current item";
            this.checkCurrentItem.UseVisualStyleBackColor = true;
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Enabled = false;
            this.labelY.Location = new System.Drawing.Point(85, 114);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 13);
            this.labelY.TabIndex = 6;
            this.labelY.Text = "Y:";
            this.labelY.Visible = false;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Enabled = false;
            this.labelX.Location = new System.Drawing.Point(6, 114);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 13);
            this.labelX.TabIndex = 5;
            this.labelX.Text = "X:";
            this.labelX.Visible = false;
            // 
            // comboPosition
            // 
            this.comboPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPosition.FormattingEnabled = true;
            this.comboPosition.Location = new System.Drawing.Point(102, 83);
            this.comboPosition.Name = "comboPosition";
            this.comboPosition.Size = new System.Drawing.Size(121, 21);
            this.comboPosition.TabIndex = 3;
            this.comboPosition.SelectedValueChanged += new System.EventHandler(this.comboPosition_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Window position";
            // 
            // checkCaseSensitiveFind
            // 
            this.checkCaseSensitiveFind.AutoSize = true;
            this.checkCaseSensitiveFind.Location = new System.Drawing.Point(9, 9);
            this.checkCaseSensitiveFind.Name = "checkCaseSensitiveFind";
            this.checkCaseSensitiveFind.Size = new System.Drawing.Size(114, 17);
            this.checkCaseSensitiveFind.TabIndex = 1;
            this.checkCaseSensitiveFind.Text = "Case sensitive find";
            this.checkCaseSensitiveFind.UseVisualStyleBackColor = true;
            // 
            // editY
            // 
            this.editY.Enabled = false;
            this.editY.Location = new System.Drawing.Point(108, 111);
            this.editY.Name = "editY";
            this.editY.Size = new System.Drawing.Size(50, 20);
            this.editY.TabIndex = 7;
            this.editY.Visible = false;
            // 
            // editX
            // 
            this.editX.Enabled = false;
            this.editX.Location = new System.Drawing.Point(29, 111);
            this.editX.Name = "editX";
            this.editX.Size = new System.Drawing.Size(50, 20);
            this.editX.TabIndex = 4;
            this.editX.Visible = false;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 381);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox backColor2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox backColor1;
        private System.Windows.Forms.Button resetColors;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox selectionColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox findColor;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.TextBox fontBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkCaseSensitiveFind;
        private System.Windows.Forms.ComboBox comboPosition;
        private System.Windows.Forms.Label label6;
        private NumericEditBox editY;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private NumericEditBox editX;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn color;
        private System.Windows.Forms.CheckBox checkCurrentItem;
        private System.Windows.Forms.CheckBox checkFindOnlyName;
    }
}