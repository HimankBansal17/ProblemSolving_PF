using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heaph_PathFinding
{
    public partial class Board : Form
    {


        public int GridX;
        public int GridY;
        public Node[,] Grid;
        public Board()
        {
            InitializeComponent();
            board_panel.BackColor = Color.AntiqueWhite;
        }


        
        private void Board_Load(object sender, EventArgs e)
        {
            
        }

        private void board_panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Panel1
            Pen b = new Pen(Color.Black);
            
            
            for (int i = 1; i <=board_panel.Width / 20; i++)
            {
                g.DrawLine(b, i * 20, 0, i * 20, board_panel.Height);
            }

            for (int j=1;j<=board_panel.Height/20;j++)
            {
                g.DrawLine(b,0,j*20,board_panel.Width,j*20);
            }

            g.FillRectangle(new SolidBrush(Color.Cyan), 0, 0, 20, 20);
        }
        
        //make the start point 
    }
}
