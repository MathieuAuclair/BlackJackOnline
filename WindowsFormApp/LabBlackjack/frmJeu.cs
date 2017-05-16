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
	* OnlineMode handling (!!!!!)
	* Refactoring (!!!!)
	* Documentation (!)
	* Use GroupOfPlayer in a better way
*/
using System.Xml;

namespace LabBlackjack
{
    public partial class frmJeu : Form
    {
		Croupier blackjackSet = new Croupier ();
		GroupOfPlayer inGamePlayer = new GroupOfPlayer (2);
		int cardImageIndex = 0;

        public frmJeu()
        {
            InitializeComponent();
		}

        private void picFinCarte_Click(object sender, EventArgs e){
			if (inGamePlayer.listOfPlayer [0].isPlayerFolded) {
				playTurn ();
			} 
			else {
				inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
				MessageBox.Show ("you fold!");
				findSetWinner ();
			}
		}

        private void picDemanderCarte_Click(object sender, EventArgs e){
			playTurn ();
			if (inGamePlayer.listOfPlayer [0].isPlayerFolded == false) {
				inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
				findSetWinner ();
			}
		}

        private void picDistribuerCarte_Click(object sender, EventArgs e){
			playTurn ();
        }

		private void playTurn(){
			if (inGamePlayer.listOfPlayer [0].isPlayerFolded) {
				MessageBox.Show ("can't play while being fold!");
			} 
			else if(isPlayerNotBusted()){
				if (cardImageIndex < 5) {
					cardImageIndex++;
				} 
				else {
					MessageBox.Show ("max card reached!");
					findSetWinner ();
				}
				int newCard = blackjackSet.DrawNewCardFromCardPack ();
				PictureBox cardPictureBox = (PictureBox)Controls ["picJ" + cardImageIndex];
				cardPictureBox.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/" + blackjackSet.GetCardFullName(newCard) + ".gif");
				inGamePlayer.listOfPlayer [0].HandOfCard.Add (newCard);
				inGamePlayer.listOfPlayer [0].getNewTotalPointFromHandOfCard ();
				if(isPlayerNotBusted()) {
					lblCptJ.Text = inGamePlayer.listOfPlayer [0].totalPointInHand.ToString();
				}
			}
		}

		private bool isPlayerNotBusted(){
			if (inGamePlayer.listOfPlayer [0].totalPointInHand > 21) {
				MessageBox.Show ("busted!");
				inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
				inGamePlayer.listOfPlayer [0].gamePlayed++;
				askForNewSet ();
				return false;
			}
			else {
				return true;
			} 
		}

		private void findSetWinner(){
			setCroupierHandScore (inGamePlayer.listOfPlayer[0].totalPointInHand);
			MessageBox.Show ("the winner is the " + inGamePlayer.findWinningPlayer());
			askForNewSet ();
		}

		private void setCroupierHandScore(int scoreToBeat){
			int cardImageIndexCroupier = 0;
			inGamePlayer.listOfPlayer[1].name = "croupier";
			while (inGamePlayer.listOfPlayer[1].totalPointInHand < scoreToBeat) {
				cardImageIndexCroupier++;
				if (cardImageIndex < 6) {
					int newCard = blackjackSet.DrawNewCardFromCardPack ();
					PictureBox cardPictureBox = (PictureBox)Controls ["picC" + cardImageIndexCroupier];
					cardPictureBox.Image = Image.FromFile (Directory.GetCurrentDirectory () + "/images/" + blackjackSet.GetCardFullName (newCard) + ".gif");
					inGamePlayer.listOfPlayer [1].HandOfCard.Add (newCard);
					inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard ();
				} 
				else {
					break;
				}
			}
			lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString();
		}

		private void askForNewSet(){
			DialogResult newGame = MessageBox.Show ("Do you want to play a new game?", "new game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (newGame == DialogResult.Yes) {
				lblCptC.Text = "0";
				lblCptJ.Text = "0";
				resetCardPictureBox ();
				inGamePlayer = new GroupOfPlayer (2);
			} 
			else {
				saveGame ();
				this.Close ();
			}
		}

		private void saveGame(){
			XmlDocument xmlSave = new XmlDocument();
			XmlNode xmlSaveNode = xmlSave.CreateNode(XmlNodeType.Element, "SAVE", "");
			foreach(Player save in inGamePlayer.listOfPlayer){
				XmlNode newSave = save.getCreateXmlNode (xmlSave);
				xmlSaveNode.AppendChild (newSave);
			}
			xmlSave.RemoveAll();
			xmlSave.AppendChild (xmlSaveNode);
			xmlSave.Save ("save.xml");
		}

		private void resetCardPictureBox(){
			cardImageIndex = 0;
			for (int i = 1; i < 6; i++) {
				PictureBox card = (PictureBox)Controls ["picJ" + i];
				card.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/back.gif");
				card = (PictureBox)Controls ["picC" + i];
				card.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/back.gif");
			}

		}
    }
}