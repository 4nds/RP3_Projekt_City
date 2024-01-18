namespace City_Rp3
{
    partial class GameMenuContent
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
            this.resume_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.quit_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resume_button
            // 
            this.resume_button.Location = new System.Drawing.Point(43, 15);
            this.resume_button.Name = "resume_button";
            this.resume_button.Size = new System.Drawing.Size(112, 34);
            this.resume_button.TabIndex = 0;
            this.resume_button.Text = "Resume";
            this.resume_button.UseVisualStyleBackColor = true;
            this.resume_button.Click += new System.EventHandler(this.resume_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(43, 64);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(112, 34);
            this.save_button.TabIndex = 1;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // quit_button
            // 
            this.quit_button.Location = new System.Drawing.Point(43, 114);
            this.quit_button.Name = "quit_button";
            this.quit_button.Size = new System.Drawing.Size(112, 34);
            this.quit_button.TabIndex = 2;
            this.quit_button.Text = "Exit";
            this.quit_button.UseVisualStyleBackColor = true;
            this.quit_button.Click += new System.EventHandler(this.quit_button_Click);
            // 
            // GameMenuContent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.quit_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.resume_button);
            this.Name = "GameMenuContent";
            this.Size = new System.Drawing.Size(200, 165);
            this.ResumeLayout(false);

        }

        #endregion

        private Button resume_button;
        private Button save_button;
        private Button quit_button;
    }
}
