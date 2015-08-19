namespace DrawingApp
{
    partial class DrawingApp
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
            this.redo_button = new System.Windows.Forms.Button();
            this.undo_button = new System.Windows.Forms.Button();
            this.load_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.modebox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupButton = new System.Windows.Forms.Button();
            this.UngroupButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Controls.Add(this.UngroupButton);
            this.panel1.Controls.Add(this.GroupButton);
            this.panel1.Controls.Add(this.redo_button);
            this.panel1.Controls.Add(this.undo_button);
            this.panel1.Controls.Add(this.load_button);
            this.panel1.Controls.Add(this.save_button);
            this.panel1.Controls.Add(this.modebox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(822, 30);
            this.panel1.TabIndex = 0;
            // 
            // redo_button
            // 
            this.redo_button.Location = new System.Drawing.Point(408, 3);
            this.redo_button.Name = "redo_button";
            this.redo_button.Size = new System.Drawing.Size(54, 23);
            this.redo_button.TabIndex = 5;
            this.redo_button.Text = "Redo";
            this.redo_button.UseVisualStyleBackColor = true;
            this.redo_button.Click += new System.EventHandler(this.redo_button_Click);
            // 
            // undo_button
            // 
            this.undo_button.Location = new System.Drawing.Point(346, 3);
            this.undo_button.Name = "undo_button";
            this.undo_button.Size = new System.Drawing.Size(56, 23);
            this.undo_button.TabIndex = 4;
            this.undo_button.Text = "Undo";
            this.undo_button.UseVisualStyleBackColor = true;
            this.undo_button.Click += new System.EventHandler(this.undo_button_Click);
            // 
            // load_button
            // 
            this.load_button.Location = new System.Drawing.Point(578, 3);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(75, 23);
            this.load_button.TabIndex = 3;
            this.load_button.Text = "Load";
            this.load_button.UseVisualStyleBackColor = true;
            this.load_button.Click += new System.EventHandler(this.load_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(497, 3);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 2;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // modebox
            // 
            this.modebox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modebox.FormattingEnabled = true;
            this.modebox.Items.AddRange(new object[] {
            "Create Rectangle",
            "Create Ellipse",
            "Move",
            "Resize"});
            this.modebox.Location = new System.Drawing.Point(144, 5);
            this.modebox.Name = "modebox";
            this.modebox.Size = new System.Drawing.Size(170, 21);
            this.modebox.TabIndex = 1;
            this.modebox.SelectedIndexChanged += new System.EventHandler(this.modebox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Coordinates: 0x0";
            // 
            // GroupButton
            // 
            this.GroupButton.Location = new System.Drawing.Point(659, 3);
            this.GroupButton.Name = "GroupButton";
            this.GroupButton.Size = new System.Drawing.Size(75, 23);
            this.GroupButton.TabIndex = 6;
            this.GroupButton.Text = "Group";
            this.GroupButton.UseVisualStyleBackColor = true;
            this.GroupButton.Click += new System.EventHandler(this.GroupButton_Click);
            // 
            // UngroupButton
            // 
            this.UngroupButton.Location = new System.Drawing.Point(740, 3);
            this.UngroupButton.Name = "UngroupButton";
            this.UngroupButton.Size = new System.Drawing.Size(75, 23);
            this.UngroupButton.TabIndex = 7;
            this.UngroupButton.Text = "UnGroup";
            this.UngroupButton.UseVisualStyleBackColor = true;
            this.UngroupButton.Click += new System.EventHandler(this.UngroupButton_Click);
            // 
            // DrawingApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 532);
            this.Controls.Add(this.panel1);
            this.Name = "DrawingApp";
            this.Text = "DrawingApplication";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingApp_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DrawingApp_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingApp_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingApp_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingApp_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox modebox;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Button load_button;
        private System.Windows.Forms.Button redo_button;
        private System.Windows.Forms.Button undo_button;
        private System.Windows.Forms.Button UngroupButton;
        private System.Windows.Forms.Button GroupButton;
    }
}

