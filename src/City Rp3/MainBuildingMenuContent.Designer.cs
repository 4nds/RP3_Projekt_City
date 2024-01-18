namespace City_Rp3
{
    partial class MainBuildingMenuContent
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
            this.buildings_button = new System.Windows.Forms.Button();
            this.workers_button = new System.Windows.Forms.Button();
            this.resources_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buildings_button
            // 
            this.buildings_button.Location = new System.Drawing.Point(81, 17);
            this.buildings_button.Name = "buildings_button";
            this.buildings_button.Size = new System.Drawing.Size(112, 34);
            this.buildings_button.TabIndex = 0;
            this.buildings_button.Text = "Buildings";
            this.buildings_button.UseVisualStyleBackColor = true;
            this.buildings_button.Click += new System.EventHandler(this.buildings_button_Click);
            // 
            // workers_button
            // 
            this.workers_button.Location = new System.Drawing.Point(81, 69);
            this.workers_button.Name = "workers_button";
            this.workers_button.Size = new System.Drawing.Size(112, 34);
            this.workers_button.TabIndex = 1;
            this.workers_button.Text = "Workers";
            this.workers_button.UseVisualStyleBackColor = true;
            this.workers_button.Click += new System.EventHandler(this.workers_button_Click);
            // 
            // resources_label
            // 
            this.resources_label.AutoSize = true;
            this.resources_label.Location = new System.Drawing.Point(15, 122);
            this.resources_label.Name = "resources_label";
            this.resources_label.Size = new System.Drawing.Size(95, 25);
            this.resources_label.TabIndex = 2;
            this.resources_label.Text = "Resources:";
            // 
            // MainBuildingMenuContent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.buildings_button);
            this.Controls.Add(this.workers_button);
            this.Controls.Add(this.resources_label);
            this.Name = "MainBuildingMenuContent";
            this.Size = new System.Drawing.Size(275, 225);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buildings_button;
        private Button workers_button;
        private Label resources_label;
    }
}
