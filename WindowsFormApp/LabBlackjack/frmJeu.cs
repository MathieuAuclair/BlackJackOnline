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

namespace LabBlackjack
{
    public partial class frmJeu : Form
    {
		Croupier blackjackSet = new Croupier ();
		GroupOfPlayer inGamePlayer = new GroupOfPlayer (2);

        public frmJeu(bool onlineMode)
        {
            InitializeComponent();
			if (onlineMode) {
				launchOnlineGame ();
			}
			else{
				launchOfflineGame ();
			}
		}

		public void launchOnlineGame (){
		
		}

		public void launchOfflineGame(){
			
		}

        private void picFinCarte_Click(object sender, EventArgs e){
			foldCard ();
        }

        private void picDemanderCarte_Click(object sender, EventArgs e){
			drawNewCard ();
        }



        private void picDistribuerCarte_Click(object sender, EventArgs e){
			drawNewCard ();
			foldCard ();
        }

		private void drawNewCard(){
			int newCard = blackjackSet.DrawNewCardFromCardPack ();
			inGamePlayer.listOfPlayer[0].HandOfCard.Add(newCard);
			string imageName = Regex.Replace (blackjackSet.GetCardFullName (newCard), @"\s+", "");
			MessageBox.Show (imageName);
			picJ1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/" + imageName + ".gif");
		}

		private void foldCard(){
			inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
		}
    }
}
