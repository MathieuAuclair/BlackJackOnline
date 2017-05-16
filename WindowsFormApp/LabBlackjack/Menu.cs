using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabBlackjack
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
			MessageBox.Show ("To launch server open a command line and execute the server.js file with node\n\n node server.js"+
			"\n\nAlso don't forget to set server IP adress to your router IP adress and open port 8080 on your router and computer"+
			", If you get any problem make sure port 8080 is availible"+
			"\n\nFor any other problem https://github.com/MathieuAuclair/BlackJackOnline"
			);
        }

        private void button2_Click(object sender, EventArgs e)
        {
			Application.Run (new frmJeu ());
        }

        private void button3_Click(object sender, EventArgs e)
        {
			
		}
    }
}
