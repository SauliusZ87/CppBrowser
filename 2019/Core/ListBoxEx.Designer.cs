namespace CppBrowser.Core
{
    partial class ListBoxEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListBoxEx));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tiNamespace = new System.Windows.Forms.ToolStripButton();
            this.tiParameters = new System.Windows.Forms.ToolStripButton();
            this.tiSort = new System.Windows.Forms.ToolStripButton();
            this.tiFind = new System.Windows.Forms.ToolStripButton();
            this.tiSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripFind = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.findBox = new ToolStripSpringTextBox();
            this.btnCloseFind = new System.Windows.Forms.ToolStripButton();
            this.btnRememberLastSearch = new System.Windows.Forms.ToolStripButton();
            this.btnLockFindBar = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
            this.toolStripFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BackColor = System.Drawing.SystemColors.Control;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(0, 28);
            this.listBox1.Margin = new System.Windows.Forms.Padding(0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(393, 225);
            this.listBox1.TabIndex = 0;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox1_DrawItem);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // toolStripMain
            // 
            this.toolStripMain.AllowItemReorder = true;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiNamespace,
            this.tiParameters,
            this.tiSort,
            this.tiFind,
            this.tiSettings});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(393, 25);
            this.toolStripMain.Stretch = true;
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.LayoutCompleted += new System.EventHandler(this.toolStripMain_LayoutCompleted);
            this.toolStripMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseDown);
            this.toolStripMain.MouseLeave += new System.EventHandler(this.toolStripMain_MouseLeave);
            this.toolStripMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseMove);
            this.toolStripMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolStripMain_MouseUp);
            // 
            // tiNamespace
            // 
            this.tiNamespace.CheckOnClick = true;
            this.tiNamespace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tiNamespace.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.tiNamespace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiNamespace.Name = "tiNamespace";
            this.tiNamespace.Size = new System.Drawing.Size(23, 22);
            this.tiNamespace.Tag = "0";
            this.tiNamespace.Text = "c::";
            this.tiNamespace.ToolTipText = "Show qualified names";
            this.tiNamespace.Click += new System.EventHandler(this.tiNamespace_Click);
            // 
            // tiParameters
            // 
            this.tiParameters.CheckOnClick = true;
            this.tiParameters.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tiParameters.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.tiParameters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiParameters.Name = "tiParameters";
            this.tiParameters.Size = new System.Drawing.Size(25, 22);
            this.tiParameters.Tag = "1";
            this.tiParameters.Text = "(..)";
            this.tiParameters.ToolTipText = "Show parameters";
            this.tiParameters.Click += new System.EventHandler(this.tiParameters_Click);
            // 
            // tiSort
            // 
            this.tiSort.CheckOnClick = true;
            this.tiSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tiSort.Image = ((System.Drawing.Image)(resources.GetObject("tiSort.Image")));
            this.tiSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiSort.Name = "tiSort";
            this.tiSort.Size = new System.Drawing.Size(23, 22);
            this.tiSort.Tag = "2";
            this.tiSort.Text = "Sort";
            this.tiSort.ToolTipText = "Sort by name";
            this.tiSort.Click += new System.EventHandler(this.tiSort_Click);
            // 
            // tiFind
            // 
            this.tiFind.CheckOnClick = true;
            this.tiFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tiFind.Image = ((System.Drawing.Image)(resources.GetObject("tiFind.Image")));
            this.tiFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiFind.Name = "tiFind";
            this.tiFind.Size = new System.Drawing.Size(23, 22);
            this.tiFind.Tag = "3";
            this.tiFind.Text = "Find";
            this.tiFind.Click += new System.EventHandler(this.tiFind_Click);
            // 
            // tiSettings
            // 
            this.tiSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tiSettings.Image = ((System.Drawing.Image)(resources.GetObject("tiSettings.Image")));
            this.tiSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tiSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tiSettings.Name = "tiSettings";
            this.tiSettings.Size = new System.Drawing.Size(23, 22);
            this.tiSettings.Tag = "4";
            this.tiSettings.Text = "Settings";
            this.tiSettings.ToolTipText = "Show settings dialog";
            this.tiSettings.Click += new System.EventHandler(this.tiSettings_Click);
            // 
            // toolStripFind
            // 
            this.toolStripFind.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripFind.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripFind.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.findBox,
            this.btnCloseFind,
            this.btnRememberLastSearch,
            this.btnLockFindBar});
            this.toolStripFind.Location = new System.Drawing.Point(0, 232);
            this.toolStripFind.Name = "toolStripFind";
            this.toolStripFind.Size = new System.Drawing.Size(393, 25);
            this.toolStripFind.Stretch = true;
            this.toolStripFind.TabIndex = 2;
            this.toolStripFind.Text = "toolStrip2";
            this.toolStripFind.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Enter text to find";
            // 
            // findBox
            // 
            this.findBox.Name = "findBox";
            this.findBox.Size = new System.Drawing.Size(267, 25);
            this.findBox.AltNumberPressed += new ToolStripSpringTextBox.AltNumNotification(this.findBox_AltNumPressed);
            this.findBox.EscapeKeyPressed += new ToolStripSpringTextBox.KeyNotification(this.findBox_EscapePressed);
            this.findBox.EnterKeyPressed += new ToolStripSpringTextBox.KeyNotification(this.findBox_EnterPressed);
            this.findBox.ArrowKeyPressed += new ToolStripSpringTextBox.ArrowKeyNotification(this.findBox_ArrowPressed);
            this.findBox.TextChanged += new System.EventHandler(this.findBox_TextChanged);
            // 
            // btnCloseFind
            // 
            this.btnCloseFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseFind.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseFind.Image")));
            this.btnCloseFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseFind.Name = "btnCloseFind";
            this.btnCloseFind.Size = new System.Drawing.Size(23, 22);
            this.btnCloseFind.Text = "toolStripButton3";
            this.btnCloseFind.ToolTipText = "Close find bar";
            this.btnCloseFind.Click += new System.EventHandler(this.closeFind_Click);
            // 
            // btnRememberLastSearch
            // 
            this.btnRememberLastSearch.CheckOnClick = true;
            this.btnRememberLastSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRememberLastSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnRememberLastSearch.Image")));
            this.btnRememberLastSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRememberLastSearch.Name = "btnRememberLastSearch";
            this.btnRememberLastSearch.Size = new System.Drawing.Size(23, 22);
            this.btnRememberLastSearch.Text = "toolStripButton1";
            this.btnRememberLastSearch.ToolTipText = "Remember last search";
            this.btnRememberLastSearch.Click += new System.EventHandler(this.btnRememberLastSearch_Click);
            // 
            // btnLockFindBar
            // 
            this.btnLockFindBar.CheckOnClick = true;
            this.btnLockFindBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLockFindBar.Image = ((System.Drawing.Image)(resources.GetObject("btnLockFindBar.Image")));
            this.btnLockFindBar.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLockFindBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLockFindBar.Name = "btnLockFindBar";
            this.btnLockFindBar.Size = new System.Drawing.Size(23, 22);
            this.btnLockFindBar.Text = "toolStripButton1";
            this.btnLockFindBar.ToolTipText = "Lock findbar";
            this.btnLockFindBar.Click += new System.EventHandler(this.btnLockFindBar_Click);
            // 
            // ListBoxEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 257);
            this.ControlBox = false;
            this.Controls.Add(this.toolStripFind);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(880, 560);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 270);
            this.Name = "ListBoxEx";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.ListBox_Deactivate);
            this.Shown += new System.EventHandler(this.ListBox_Shown);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.toolStripFind.ResumeLayout(false);
            this.toolStripFind.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton tiNamespace;
        private System.Windows.Forms.ToolStripButton tiParameters;
        private System.Windows.Forms.ToolStripButton tiSort;
        private System.Windows.Forms.ToolStrip toolStripFind;
        private ToolStripSpringTextBox findBox;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton btnCloseFind;
        private System.Windows.Forms.ToolStripButton tiFind;
        private System.Windows.Forms.ToolStripButton btnLockFindBar;
        private System.Windows.Forms.ToolStripButton btnRememberLastSearch;
        private System.Windows.Forms.ToolStripButton tiSettings;
    }
}