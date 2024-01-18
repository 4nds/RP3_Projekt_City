namespace City_Rp3
{
    partial class WorkersMenuContent
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
            this.workers_panel = new System.Windows.Forms.Panel();
            this.add_worker_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // workers_panel
            // 
            this.workers_panel.AutoScroll = true;
            this.workers_panel.Location = new System.Drawing.Point(0, 0);
            this.workers_panel.Name = "workers_panel";
            this.workers_panel.Size = new System.Drawing.Size(215, 250);
            this.workers_panel.TabIndex = 6;
            // 
            // add_worker_button
            // 
            this.add_worker_button.Location = new System.Drawing.Point(47, 260);
            this.add_worker_button.Name = "add_worker_button";
            this.add_worker_button.Size = new System.Drawing.Size(120, 35);
            this.add_worker_button.TabIndex = 7;
            this.add_worker_button.Text = "Add Worker";
            this.add_worker_button.UseVisualStyleBackColor = true;
            this.add_worker_button.Click += new System.EventHandler(this.add_worker_button_Click);
            // 
            // WorkersMenuContent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.workers_panel);
            this.Controls.Add(this.add_worker_button);
            this.Name = "WorkersMenuContent";
            this.Size = new System.Drawing.Size(215, 305);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel workers_panel;
        private Button add_worker_button;
    }
}
