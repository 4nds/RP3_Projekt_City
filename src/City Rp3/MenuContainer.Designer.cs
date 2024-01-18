namespace City_Rp3
{
    partial class MenuContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.title_bar_panel = new System.Windows.Forms.Panel();
            this.close_button = new System.Windows.Forms.Button();
            this.title_label = new System.Windows.Forms.Label();
            this.title_bar_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // title_bar_panel
            // 
            this.title_bar_panel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.title_bar_panel.Controls.Add(this.close_button);
            this.title_bar_panel.Controls.Add(this.title_label);
            this.title_bar_panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.title_bar_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.title_bar_panel.Location = new System.Drawing.Point(0, 0);
            this.title_bar_panel.Name = "title_bar_panel";
            this.title_bar_panel.Size = new System.Drawing.Size(200, 35);
            this.title_bar_panel.TabIndex = 0;
            this.title_bar_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.title_bar_panel_MouseDown);
            this.title_bar_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.title_bar_panel_MouseMove);
            this.title_bar_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.title_bar_panel_MouseUp);
            // 
            // close_button
            // 
            this.close_button.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.close_button.ForeColor = System.Drawing.SystemColors.ControlText;
            this.close_button.Location = new System.Drawing.Point(165, 0);
            this.close_button.Margin = new System.Windows.Forms.Padding(0);
            this.close_button.Name = "close_button";
            this.close_button.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.close_button.Size = new System.Drawing.Size(35, 35);
            this.close_button.TabIndex = 1;
            this.close_button.Text = "✕";
            this.close_button.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            this.close_button.MouseLeave += new System.EventHandler(this.close_button_MouseLeave);
            this.close_button.MouseHover += new System.EventHandler(this.close_button_MouseHover);
            // 
            // title_label
            // 
            this.title_label.AutoSize = true;
            this.title_label.Location = new System.Drawing.Point(6, 4);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(57, 25);
            this.title_label.TabIndex = 0;
            this.title_label.Text = "Menu";
            this.title_label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.title_label_MouseDown);
            this.title_label.MouseMove += new System.Windows.Forms.MouseEventHandler(this.title_label_MouseMove);
            this.title_label.MouseUp += new System.Windows.Forms.MouseEventHandler(this.title_label_MouseUp);
            // 
            // MenuContainer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.title_bar_panel);
            this.Name = "MenuContainer";
            this.Size = new System.Drawing.Size(200, 200);
            this.title_bar_panel.ResumeLayout(false);
            this.title_bar_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel title_bar_panel;
        private Button close_button;
        private Label title_label;
    }
}
