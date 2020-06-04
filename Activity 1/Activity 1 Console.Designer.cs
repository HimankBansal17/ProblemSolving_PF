namespace Assignment____Forms_
{
    partial class Activity1
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
            this.button2 = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.EndPoint = new System.Windows.Forms.CheckBox();
            this.WaterSquare = new System.Windows.Forms.CheckBox();
            this.GrassSquare = new System.Windows.Forms.CheckBox();
            this.RockySqaure = new System.Windows.Forms.CheckBox();
            this.Obstacle = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox90 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.Walkable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 27);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter the Size of The Matrix";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(83, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(158, 77);
            this.button2.TabIndex = 4;
            this.button2.Text = "Find Shortest Path";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Start
            // 
            this.Start.AutoSize = true;
            this.Start.Location = new System.Drawing.Point(34, 155);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(92, 21);
            this.Start.TabIndex = 5;
            this.Start.Text = "StartPoint";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button3.FlatAppearance.BorderSize = 10;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button3.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(95, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 51);
            this.button3.TabIndex = 6;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.UseWaitCursor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // EndPoint
            // 
            this.EndPoint.AutoSize = true;
            this.EndPoint.Location = new System.Drawing.Point(34, 182);
            this.EndPoint.Name = "EndPoint";
            this.EndPoint.Size = new System.Drawing.Size(87, 21);
            this.EndPoint.TabIndex = 7;
            this.EndPoint.Text = "EndPoint";
            this.EndPoint.UseVisualStyleBackColor = true;
            this.EndPoint.CheckedChanged += new System.EventHandler(this.EndPoint_CheckedChanged);
            // 
            // WaterSquare
            // 
            this.WaterSquare.AutoSize = true;
            this.WaterSquare.Location = new System.Drawing.Point(34, 209);
            this.WaterSquare.Name = "WaterSquare";
            this.WaterSquare.Size = new System.Drawing.Size(118, 21);
            this.WaterSquare.TabIndex = 8;
            this.WaterSquare.Text = "Water Sqaure";
            this.WaterSquare.UseVisualStyleBackColor = true;
            this.WaterSquare.CheckedChanged += new System.EventHandler(this.WaterSquare_CheckedChanged);
            // 
            // GrassSquare
            // 
            this.GrassSquare.AutoSize = true;
            this.GrassSquare.Location = new System.Drawing.Point(34, 236);
            this.GrassSquare.Name = "GrassSquare";
            this.GrassSquare.Size = new System.Drawing.Size(114, 21);
            this.GrassSquare.TabIndex = 9;
            this.GrassSquare.Text = "GrassSqaure";
            this.GrassSquare.UseVisualStyleBackColor = true;
            this.GrassSquare.CheckedChanged += new System.EventHandler(this.GrassSquare_CheckedChanged);
            // 
            // RockySqaure
            // 
            this.RockySqaure.AutoSize = true;
            this.RockySqaure.Location = new System.Drawing.Point(34, 263);
            this.RockySqaure.Name = "RockySqaure";
            this.RockySqaure.Size = new System.Drawing.Size(115, 21);
            this.RockySqaure.TabIndex = 10;
            this.RockySqaure.Text = "RockySqaure";
            this.RockySqaure.UseVisualStyleBackColor = true;
            this.RockySqaure.CheckedChanged += new System.EventHandler(this.RockySquare_CheckedChanged_1);
            // 
            // Obstacle
            // 
            this.Obstacle.AutoSize = true;
            this.Obstacle.Location = new System.Drawing.Point(184, 263);
            this.Obstacle.Name = "Obstacle";
            this.Obstacle.Size = new System.Drawing.Size(86, 21);
            this.Obstacle.TabIndex = 11;
            this.Obstacle.Text = "Obstacle";
            this.Obstacle.UseVisualStyleBackColor = true;
            this.Obstacle.CheckedChanged += new System.EventHandler(this.Obstacle_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(83, 373);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 37);
            this.button1.TabIndex = 18;
            this.button1.Text = "Find Easiest Path";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(83, 416);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(158, 37);
            this.button4.TabIndex = 19;
            this.button4.Text = "Find Hardest Path";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox90
            // 
            this.checkBox90.AutoSize = true;
            this.checkBox90.Location = new System.Drawing.Point(184, 209);
            this.checkBox90.Name = "checkBox90";
            this.checkBox90.Size = new System.Drawing.Size(111, 21);
            this.checkBox90.TabIndex = 20;
            this.checkBox90.Text = "Walkable 90\'";
            this.checkBox90.UseVisualStyleBackColor = true;
            this.checkBox90.CheckedChanged += new System.EventHandler(this.checkBox90_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(184, 182);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(119, 21);
            this.checkBox2.TabIndex = 21;
            this.checkBox2.Text = "Walkable 120\'";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(184, 236);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(111, 21);
            this.checkBox3.TabIndex = 22;
            this.checkBox3.Text = "Walkable 40\'";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // Walkable
            // 
            this.Walkable.AutoSize = true;
            this.Walkable.Location = new System.Drawing.Point(184, 155);
            this.Walkable.Name = "Walkable";
            this.Walkable.Size = new System.Drawing.Size(138, 21);
            this.Walkable.TabIndex = 15;
            this.Walkable.Text = "Walkable Sqaure";
            this.Walkable.UseVisualStyleBackColor = true;
            this.Walkable.CheckedChanged += new System.EventHandler(this.WalkableSquare);
            // 
            // Activity1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 512);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox90);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Walkable);
            this.Controls.Add(this.Obstacle);
            this.Controls.Add(this.RockySqaure);
            this.Controls.Add(this.GrassSquare);
            this.Controls.Add(this.WaterSquare);
            this.Controls.Add(this.EndPoint);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Activity1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox Start;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox EndPoint;
        private System.Windows.Forms.CheckBox WaterSquare;
        private System.Windows.Forms.CheckBox GrassSquare;
        private System.Windows.Forms.CheckBox RockySqaure;
        private System.Windows.Forms.CheckBox Obstacle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox90;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox Walkable;
    }
}