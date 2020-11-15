namespace PathFinder_Interface
{
    partial class Board
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
            this.board_panel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // board_panel
            // 
            this.board_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board_panel.Location = new System.Drawing.Point(0, 0);
            this.board_panel.Name = "board_panel";
            this.board_panel.Size = new System.Drawing.Size(800, 450);
            this.board_panel.TabIndex = 0;
            this.board_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.board_panel_Paint);
            // 
            // Board
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.board_panel);
            this.Name = "Board";
            this.Text = "Board";
            this.Load += new System.EventHandler(this.Board_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel board_panel;
    }
}