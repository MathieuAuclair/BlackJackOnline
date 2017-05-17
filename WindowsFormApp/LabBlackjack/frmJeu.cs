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
		Croupier blackjackSet = new Croupier ();
		GroupOfPlayer inGamePlayer = new GroupOfPlayer (2);
		int cardImageIndex = 0;
		int cardImageIndexCroupier = 1;
		bool onlineMode;
		string onlineID = "";

		public frmJeu(bool online)
        {
            InitializeComponent();
			onlineMode = online;
			if (online) {
				checkForPartner ();
			} 
			else {
				DialogResult loadGameSave = MessageBox.Show ("Do you want to load save?", "load game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				if (loadGameSave == DialogResult.Yes) {
					loadGame ();
				}
				playTurnForCroupier ();
				inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard();
				lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString ();
			}
		}

		/*----------BTN----------------------------------------------*/
        private void picFinCarte_Click(object sender, EventArgs e){
			if (onlineMode) {
				if (sendCustomPOSTWebRequest ("turn", onlineID) == "false") {
					MessageBox.Show ("Please wait for your turn");
				} 
				else {
					sendCustomPOSTWebRequest ("play", "");
				}
			} 
			else {
				if (inGamePlayer.listOfPlayer [0].isPlayerFolded) {
					playTurn ();
				} 
				else {
					inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
					MessageBox.Show ("you fold!");
					findSetWinner ();
				}
			}
		}

		/*----------BTN----------------------------------------------*/
        private void picDemanderCarte_Click(object sender, EventArgs e){
			if (onlineMode) {
				if (sendCustomPOSTWebRequest ("turn", onlineID) == "false") {
					MessageBox.Show ("Please wait for your turn");
				} 
				else {
					
				}
			} 
			else {
				playTurn ();
				if (inGamePlayer.listOfPlayer [0].isPlayerFolded == false) {
					inGamePlayer.listOfPlayer [0].isPlayerFolded = true;
					findSetWinner ();
				}
			}
		}

		/*----------BTN----------------------------------------------*/
        private void picDistribuerCarte_Click(object sender, EventArgs e){/*Isshhhh need a huge refactoring...*/
			if (onlineMode) {
				if (sendCustomPOSTWebRequest ("turn", onlineID) == "false") {
					MessageBox.Show ("Please wait for your turn");
				} 
				else {
					playTurn ();// play turn as normal and then send last card played
				}
			} 
			else {
				playTurn ();
			}
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
					if (!onlineMode) {
						findSetWinner ();
					}
				}
				int newCard = blackjackSet.DrawNewCardFromCardPack ();
				if(onlineMode){
					string score = sendCustomPOSTWebRequest ("play", newCard.ToString());
					lblCptJ.Text = score;
				}
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
			lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString();
		}

		public void playTurnForCroupier(){
			int newCard = blackjackSet.DrawNewCardFromCardPack ();
			PictureBox cardPictureBox = (PictureBox)Controls ["picC" + cardImageIndexCroupier];
			cardPictureBox.Image = Image.FromFile (Directory.GetCurrentDirectory () + "/images/" + blackjackSet.GetCardFullName (newCard) + ".gif");
			inGamePlayer.listOfPlayer [1].HandOfCard.Add (newCard);
			inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard ();
		}

		private void askForNewSet(){
			DialogResult newGame = MessageBox.Show ("Do you want to play a new game?", "new game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (newGame == DialogResult.Yes) {
				lblCptJ.Text = "0";
				resetCardPictureBox ();
				loadGame ();
				cardImageIndexCroupier = 1;
				playTurnForCroupier ();
				inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard();
				lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString ();
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
            lblCptPlayed.Text = inGamePlayer.listOfPlayer[0].gamePlayed.ToString();
            lblCptWon.Text = inGamePlayer.listOfPlayer[0].gameWon.ToString();
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

		/*online mode*/

		public void checkForPartner(){
			string gameStatus = sendCustomPOSTWebRequest ("login", onlineID);
			if (gameStatus == "false") {
				MessageBox.Show ("sorry the game is full try later!");
			} else if (gameStatus == "true") {
				MessageBox.Show ("waiting for a player!");
				Thread.Sleep (4000);
				checkForPartner ();
			} else if (gameStatus == "error") {
				MessageBox.Show ("there was an error");
				this.Close ();
			} else if (gameStatus == "start") {
				MessageBox.Show ("the game is starting!");
			}
			else {
				MessageBox.Show ("looking for a game!");
				onlineID = gameStatus;
				Thread.Sleep(2000);
				checkForPartner ();
			}
		}

		public string sendCustomPOSTWebRequest(string request, string dataToServer){
			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + request);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				//string json = "{\"user\":\"test\"," + "\"password\":\"bla\"}";
				string json = "{\"data\":\"" + dataToServer + "\"}";

				streamWriter.Write(json);
				streamWriter.Flush();
				streamWriter.Close();
			}
				
			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				return streamReader.ReadToEnd();
			}
		}
    }
}