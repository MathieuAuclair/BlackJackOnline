using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

/*
	* TODO
	* OnlineMode handling (!!!!)
	* Refactoring (!!!!!!!!!!!!!!!!!)
	* Documentation (!)
	* Use GroupOfPlayer in a better way
*/
using System.Xml;
using System.Net;
using System.Threading;

namespace LabBlackjack
{
    public partial class frmJeu : Form
    {

		game newGame;

		public frmJeu(bool online)
        {
            InitializeComponent();
			if (online) {
				newGame = new onlineGame (this);
			} 
			else {
				newGame = new offlineGame (this);
			}
		}

		private void picDistribuerCarte_Click(object sender, EventArgs e){
			newGame.picDistribuerCarte_Click ();
		}
		private void picFinCarte_Click(object sender, EventArgs e){
			newGame.picFinCarte_Click ();
		}

		private void picDemanderCarte_Click(object sender, EventArgs e){
			newGame.picDemanderCarte_Click ();
		}
    }
}