namespace City_Rp3
{
    partial class Game {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.mapuc1 = new City_Rp3.mapUC();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mapuc1
            // 
            this.mapuc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapuc1.Location = new System.Drawing.Point(0, 0);
            this.mapuc1.Map = null;
            this.mapuc1.Margin = new System.Windows.Forms.Padding(0);
            this.mapuc1.Name = "mapuc1";
            this.mapuc1.Size = new System.Drawing.Size(700, 700);
            this.mapuc1.Soldiers = null;
            this.mapuc1.TabIndex = 0;
            this.mapuc1.Wolves = null;
            this.mapuc1.Workers = null;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Game
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(700, 700);
            this.Controls.Add(this.mapuc1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Game";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private mapUC mapuc1;
        public System.Windows.Forms.Timer timer1;
    }
}