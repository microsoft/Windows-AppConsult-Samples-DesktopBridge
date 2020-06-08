namespace DynamicWindowsForms
{
    partial class FormDynamic
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.buttonAddUWP = new System.Windows.Forms.Button();
            this.comboBoxUWP = new System.Windows.Forms.ComboBox();
            this.buttonAddStandard = new System.Windows.Forms.Button();
            this.comboBoxStandard = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonClear);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 598);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 77);
            this.panel1.TabIndex = 0;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(11, 12);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(139, 53);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "C&lear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(757, 12);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(139, 53);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.buttonAddUWP);
            this.panelTop.Controls.Add(this.comboBoxUWP);
            this.panelTop.Controls.Add(this.buttonAddStandard);
            this.panelTop.Controls.Add(this.comboBoxStandard);
            this.panelTop.Controls.Add(this.menuStrip1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(930, 122);
            this.panelTop.TabIndex = 1;
            // 
            // buttonAddUWP
            // 
            this.buttonAddUWP.Location = new System.Drawing.Point(712, 68);
            this.buttonAddUWP.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddUWP.Name = "buttonAddUWP";
            this.buttonAddUWP.Size = new System.Drawing.Size(109, 33);
            this.buttonAddUWP.TabIndex = 4;
            this.buttonAddUWP.Text = "A&dd UWP";
            this.buttonAddUWP.UseVisualStyleBackColor = true;
            this.buttonAddUWP.Click += new System.EventHandler(this.buttonAddUWP_Click);
            // 
            // comboBoxUWP
            // 
            this.comboBoxUWP.FormattingEnabled = true;
            this.comboBoxUWP.Items.AddRange(new object[] {
            "Textbox",
            "Checkbox",
            "Radio Group",
            "Combobox",
            "Button",
            "Slider",
            "Notes",
            "Calendar",
            "Ink",
            "Map",
            "WebView",
            "MediaPlayer"});
            this.comboBoxUWP.Location = new System.Drawing.Point(471, 68);
            this.comboBoxUWP.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxUWP.Name = "comboBoxUWP";
            this.comboBoxUWP.Size = new System.Drawing.Size(169, 33);
            this.comboBoxUWP.TabIndex = 3;
            // 
            // buttonAddStandard
            // 
            this.buttonAddStandard.Location = new System.Drawing.Point(278, 62);
            this.buttonAddStandard.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonAddStandard.Name = "buttonAddStandard";
            this.buttonAddStandard.Size = new System.Drawing.Size(128, 42);
            this.buttonAddStandard.TabIndex = 2;
            this.buttonAddStandard.Text = "&Add";
            this.buttonAddStandard.UseVisualStyleBackColor = true;
            this.buttonAddStandard.Click += new System.EventHandler(this.buttonAddStandard_Click);
            // 
            // comboBoxStandard
            // 
            this.comboBoxStandard.FormattingEnabled = true;
            this.comboBoxStandard.Items.AddRange(new object[] {
            "Textbox",
            "Checkbox",
            "Radio Group",
            "Combobox",
            "Button",
            "Notes",
            "Calendar",
            "Browser"});
            this.comboBoxStandard.Location = new System.Drawing.Point(33, 68);
            this.comboBoxStandard.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.comboBoxStandard.Name = "comboBoxStandard";
            this.comboBoxStandard.Size = new System.Drawing.Size(224, 33);
            this.comboBoxStandard.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(930, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemSave});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 29);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(223, 34);
            this.toolStripMenuItemOpen.Text = "&Open";
            // 
            // toolStripMenuItemSave
            // 
            this.toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            this.toolStripMenuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItemSave.Size = new System.Drawing.Size(223, 34);
            this.toolStripMenuItemSave.Text = "&Save";
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.flowLayoutPanel1);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(0, 122);
            this.panelMiddle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(930, 476);
            this.panelMiddle.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(930, 476);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormDynamic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 675);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FormDynamic";
            this.Text = "Form Dynamic";
            this.panel1.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelMiddle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAddStandard;
        private System.Windows.Forms.ComboBox comboBoxStandard;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ComboBox comboBoxUWP;
        private System.Windows.Forms.Button buttonAddUWP;
    }
}

