using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFinder_Interface
{
    public partial class Board : Form
    {
        public Board()
        {
            
            InitializeComponent();
        }

        private void Board_Load(object sender, EventArgs e)
        {
            board_panel.BackColor = Color.Black;
            

        }

        private void board_panel_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            //Panel1
            Pen b = new Pen(Color.Red);
            
            
            for (int i=0;i<board_panel.Width/20;i++)
            {
                g.DrawLine(b, i *20, 0, i * 20,board_panel.Height) ;   
            }
        }
    }
}   
