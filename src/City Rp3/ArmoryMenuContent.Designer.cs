namespace City_Rp3
{
    partial class ArmoryMenuContent
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
            this.attack_panel = new System.Windows.Forms.Panel();
            this.attack_label = new System.Windows.Forms.Label();
            this.attack_button = new System.Windows.Forms.Button();
            this.defence_panel = new System.Windows.Forms.Panel();
            this.defense_label = new System.Windows.Forms.Label();
            this.defense_button = new System.Windows.Forms.Button();
            this.add_soldier_button = new System.Windows.Forms.Button();
            this.attack_panel.SuspendLayout();
            this.defence_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // attack_panel
            // 
            this.attack_panel.Controls.Add(this.attack_label);
            this.attack_panel.Controls.Add(this.attack_button);
            this.attack_panel.Location = new System.Drawing.Point(0, 0);
            this.attack_panel.Name = "attack_panel";
            this.attack_panel.Size = new System.Drawing.Size(270, 170);
            this.attack_panel.TabIndex = 3;
            // 
            // attack_label
            // 
            this.attack_label.AutoSize = true;
            this.attack_label.Location = new System.Drawing.Point(5, 15);
            this.attack_label.Name = "attack_label";
            this.attack_label.Size = new System.Drawing.Size(66, 25);
            this.attack_label.TabIndex = 0;
            this.attack_label.Text = "Attack:";
            // 
            // attack_button
            // 
            this.attack_button.Location = new System.Drawing.Point(79, 120);
            this.attack_button.Name = "attack_button";
            this.attack_button.Size = new System.Drawing.Size(112, 34);
            this.attack_button.TabIndex = 1;
            this.attack_button.Text = "Upgrade";
            this.attack_button.UseVisualStyleBackColor = true;
            this.attack_button.Click += new System.EventHandler(this.attack_button_Click);
            // 
            // defence_panel
            // 
            this.defence_panel.Controls.Add(this.defense_label);
            this.defence_panel.Controls.Add(this.defense_button);
            this.defence_panel.Location = new System.Drawing.Point(0, 170);
            this.defence_panel.Name = "defence_panel";
            this.defence_panel.Size = new System.Drawing.Size(270, 170);
            this.defence_panel.TabIndex = 4;
            // 
            // defense_label
            // 
            this.defense_label.AutoSize = true;
            this.defense_label.Location = new System.Drawing.Point(5, 15);
            this.defense_label.Name = "defense_label";
            this.defense_label.Size = new System.Drawing.Size(80, 25);
            this.defense_label.TabIndex = 1;
            this.defense_label.Text = "Defense:";
            // 
            // defense_button
            // 
            this.defense_button.Location = new System.Drawing.Point(79, 120);
            this.defense_button.Name = "defense_button";
            this.defense_button.Size = new System.Drawing.Size(112, 34);
            this.defense_button.TabIndex = 2;
            this.defense_button.Text = "Upgrade";
            this.defense_button.UseVisualStyleBackColor = true;
            this.defense_button.Click += new System.EventHandler(this.defense_button_Click);
            // 
            // add_soldier_button
            // 
            this.add_soldier_button.Location = new System.Drawing.Point(75, 352);
            this.add_soldier_button.Name = "add_soldier_button";
            this.add_soldier_button.Size = new System.Drawing.Size(120, 35);
            this.add_soldier_button.TabIndex = 5;
            this.add_soldier_button.Text = "Add Soldier";
            this.add_soldier_button.UseVisualStyleBackColor = true;
            this.add_soldier_button.Click += new System.EventHandler(this.add_soldier_button_Click);
            // 
            // ArmoryMenuContent
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.attack_panel);
            this.Controls.Add(this.defence_panel);
            this.Controls.Add(this.add_soldier_button);
            this.Name = "ArmoryMenuContent";
            this.Size = new System.Drawing.Size(270, 400);
            this.attack_panel.ResumeLayout(false);
            this.attack_panel.PerformLayout();
            this.defence_panel.ResumeLayout(false);
            this.defence_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel attack_panel;
        private Label attack_label;
        private Button attack_button;
        private Panel defence_panel;
        private Label defense_label;
        private Button defense_button;
        private Button add_soldier_button;
    }
}
