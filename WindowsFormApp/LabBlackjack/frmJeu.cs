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

namespace LabBlackjack
{
    public partial class frmJeu : Form
    {
        public frmJeu()
        {
            InitializeComponent();
        }

		public void launchNewGame(){
			Croupier blackJackSet = new Croupier ();
			GroupOfPlayer activePlayers = new GroupOfPlayer (2); //weird name...

			do {
				foreach (Player currentTurnPlayer in activePlayers.listOfPlayer) {
					Console.Write (currentTurnPlayer.name);
					if (currentTurnPlayer.isPlayerFolded) {
						Console.Write (" is folded!\n\n");
					} else {
						blackJackSet.playTurn (currentTurnPlayer);
						Console.WriteLine ("You have a hand of " + currentTurnPlayer.getNewTotalPointFromHandOfCard ());
						//currentTurnPlayer.isPlayerFolded = isPlayerFolding ();
					}
				}
			} while(activePlayers.isAnyPlayerLeftToPlay () && !activePlayers.isAnyPlayerBusted ());

		}



    }
}
