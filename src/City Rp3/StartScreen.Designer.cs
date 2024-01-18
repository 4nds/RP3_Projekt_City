namespace City_Rp3
{
    partial class StartScreen
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
            this.label1 = new System.Windows.Forms.Label();
            this.new_game_button = new System.Windows.Forms.Button();
            this.load_game_button = new System.Windows.Forms.Button();
            this.quit_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(265, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "City RP3";
            // 
            // new_game_button
            // 
            this.new_game_button.BackColor = System.Drawing.SystemColors.ControlDark;
            this.new_game_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.new_game_button.Location = new System.Drawing.Point(213, 278);
            this.new_game_button.Name = "new_game_button";
            this.new_game_button.Size = new System.Drawing.Size(274, 50);
            this.new_game_button.TabIndex = 1;
            this.new_game_button.Text = "New Game";
            this.new_game_button.UseVisualStyleBackColor = false;
            this.new_game_button.Click += new System.EventHandler(this.new_game_button_Click);
            // 
            // load_game_button
            // 
            this.load_game_button.BackColor = System.Drawing.SystemColors.ControlDark;
            this.load_game_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.load_game_button.Location = new System.Drawing.Point(213, 380);
            this.load_game_button.Name = "load_game_button";
            this.load_game_button.Size = new System.Drawing.Size(274, 50);
            this.load_game_button.TabIndex = 2;
            this.load_game_button.Text = "Load Game";
            this.load_game_button.UseVisualStyleBackColor = false;
            this.load_game_button.Click += new System.EventHandler(this.load_game_button_Click);
            // 
            // quit_button
            // 
            this.quit_button.BackColor = System.Drawing.SystemColors.ControlDark;
            this.quit_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.quit_button.Location = new System.Drawing.Point(213, 480);
            this.quit_button.Name = "quit_button";
            this.quit_button.Size = new System.Drawing.Size(274, 50);
            this.quit_button.TabIndex = 3;
            this.quit_button.Text = "Quit";
            this.quit_button.UseVisualStyleBackColor = false;
            this.quit_button.Click += new System.EventHandler(this.quit_button_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(700, 700);
            this.Controls.Add(this.quit_button);
            this.Controls.Add(this.load_game_button);
            this.Controls.Add(this.new_game_button);
            this.Controls.Add(this.label1);
            this.Name = "StartScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Button new_game_button;
        private Button quit_button;
        public Button load_game_button;
    }
}