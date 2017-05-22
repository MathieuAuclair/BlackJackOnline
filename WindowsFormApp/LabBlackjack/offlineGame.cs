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
using System.Xml;

namespace LabBlackjack
{
	public class offlineGame : game
	{
		public static frmJeu frm;

		public offlineGame (frmJeu online) : base(frm)
		{
			DialogResult loadGameSave = MessageBox.Show ("Do you want to load save?", "load game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (loadGameSave == DialogResult.Yes) {
				loadGame ();
				frm = online;
			}
			playTurnForCroupier ();
			inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard();
			frm.lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString ();
		}

	    public override void picFinCarte_Click(){
			if (inGamePlayer.listOfPlayer [0].isPlayerFolded) {
				playTurn ();
			} 
			else {
				inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
				MessageBox.Show ("you fold!");
				findSetWinner ();
			}
		}

		public override void picDemanderCarte_Click(){
			playTurn ();
			if (inGamePlayer.listOfPlayer [0].isPlayerFolded == false) {
				inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
				findSetWinner ();
			}
		}

		public override void picDistribuerCarte_Click(){
			playTurn ();
		}

		protected override void playTurn(){
			if (inGamePlayer.listOfPlayer [0].isPlayerFolded) {
				MessageBox.Show ("can't play while being fold!");
			} 
			else if(isPlayerNotBusted()){
				if (cardImageIndex < 5) {
					cardImageIndex++;
				} 
				else {
					MessageBox.Show ("max card reached!");
				}
				int newCard = blackjackSet.DrawNewCardFromCardPack ();
				PictureBox cardPictureBox = (PictureBox)frm.Controls ["picJ" + cardImageIndex];
				cardPictureBox.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/" + blackjackSet.GetCardFullName(newCard) + ".gif");
				inGamePlayer.listOfPlayer [0].HandOfCard.Add (newCard);
				inGamePlayer.listOfPlayer [0].getNewTotalPointFromHandOfCard ();
				if(isPlayerNotBusted()) {
				frm.lblCptJ.Text = inGamePlayer.listOfPlayer [0].totalPointInHand.ToString();
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

		private void loadGame(){
			XmlDocument xmlSave = new XmlDocument();
			xmlSave.Load ("save.xml");
			inGamePlayer.listOfPlayer.Clear ();
			XmlNode xmlSaveNode = xmlSave.SelectSingleNode("SAVE");
			XmlNodeList node = xmlSaveNode.SelectNodes("PLAYER");
			foreach(XmlNode loadedPlayer in node){
				Player player = new Player ();
				player.name = loadedPlayer ["NAME"].InnerText;
				player.gamePlayed = Convert.ToInt32(loadedPlayer ["PLAYED"].InnerText);
				player.gameWon = Convert.ToInt32(loadedPlayer ["WIN"].InnerText);
				inGamePlayer.listOfPlayer.Add (player);
			}
			frm.lblCptPlayed.Text = inGamePlayer.listOfPlayer[0].gamePlayed.ToString();
			frm.lblCptWon.Text = inGamePlayer.listOfPlayer[0].gameWon.ToString();
		}

		private void setCroupierHandScore(int scoreToBeat){
			inGamePlayer.listOfPlayer[1].name = "croupier";
			while (inGamePlayer.listOfPlayer[1].totalPointInHand < scoreToBeat) {
				cardImageIndexCroupier++;
				if (cardImageIndex < 6) {
					playTurnForCroupier ();
				} 
				else {
					break;
				}
			}
			frm.lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString();
		}

		public void playTurnForCroupier(){
			int newCard = blackjackSet.DrawNewCardFromCardPack ();
            PictureBox cardPictureBox = (PictureBox)frm.Controls ["picC" + cardImageIndexCroupier];
            cardPictureBox.Image = Image.FromFile (Directory.GetCurrentDirectory () + "/images/" + blackjackSet.GetCardFullName (newCard) + ".gif");
			inGamePlayer.listOfPlayer [1].HandOfCard.Add (newCard);
			inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard ();
		}

		private void askForNewSet(){
			DialogResult newGame = MessageBox.Show ("Do you want to play a new game?", "new game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (newGame == DialogResult.Yes) {
				resetCardPictureBox ();
				loadGame ();
				cardImageIndexCroupier = 1;
				playTurnForCroupier ();
				inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard();
				frm.lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString ();
			} 
			else {
				saveGame ();
				frm.Close ();
			}
		}
	}
}

