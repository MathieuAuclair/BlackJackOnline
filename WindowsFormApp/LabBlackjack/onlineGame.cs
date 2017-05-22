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
using System.Threading;
using System.Net;

namespace LabBlackjack
{
	public class onlineGame : game
	{
		public static frmJeu frm;


		public onlineGame (frmJeu online) : base(frm)
		{
			checkForPartner ();
			frm = online;
		}

		public override void picDistribuerCarte_Click(){
			if (isItPlayerTurn()) {
				playTurn ();// play turn as normal and then send last card played
			} 
			else {
				MessageBox.Show ("Please wait for your turn");
			}
		}

		public override void picFinCarte_Click(){
			if (isItPlayerTurn()) {
				sendCustomPOSTWebRequest ("play", "");// need some checkup
			} 
			else {
				MessageBox.Show ("Please wait for your turn");
			}
		}

		public override void picDemanderCarte_Click(){
			if (isItPlayerTurn()) {
				//
			} 
			else {
				MessageBox.Show ("Please wait for your turn");
			}
		}

		private bool isItPlayerTurn (){
			return !(sendCustomPOSTWebRequest ("turn", onlineID) == "false");
		}

		public void checkForPartner(){
			string gameStatus = sendCustomPOSTWebRequest ("login", onlineID);

			switch (gameStatus) {
				case "false":
					MessageBox.Show ("sorry the game is full try later!");
					break;
				case "true":
					MessageBox.Show ("waiting for a player!");
					Thread.Sleep (3000); // really need a socket connection
					checkForPartner ();
					break;
				case "error":
					MessageBox.Show ("there was an error");
					break;
				case "start":
					MessageBox.Show ("the game is starting!");
					break;
			default:
				MessageBox.Show ("looking for a game!");
				onlineID = gameStatus;
				Thread.Sleep (3000);
				checkForPartner ();
				break;
			}
		}

		public string sendCustomPOSTWebRequest(string request, string dataToServer){
			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + request);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
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
				string score = sendCustomPOSTWebRequest ("play", newCard.ToString());
				frm.lblCptC.Text = score;
				displayCard (newCard);
				inGamePlayer.listOfPlayer [0].HandOfCard.Add (newCard);
				inGamePlayer.listOfPlayer [0].getNewTotalPointFromHandOfCard ();
				if(isPlayerNotBusted()) {
				frm.lblCptJ.Text = inGamePlayer.listOfPlayer [0].totalPointInHand.ToString();
				}
			}
		}

		private void displayCard(int newCard){
			PictureBox cardPictureBox = (PictureBox)frm.Controls ["picJ" + cardImageIndex];
			cardPictureBox.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/" + blackjackSet.GetCardFullName(newCard) + ".gif");
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

		private void askForNewSet(){
			DialogResult newGame = MessageBox.Show ("Do you want to play a new game?", "new game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (newGame == DialogResult.Yes) {
				frm.lblCptJ.Text = "0";
				resetCardPictureBox (frm);
				cardImageIndexCroupier = 1;
				inGamePlayer.listOfPlayer [1].getNewTotalPointFromHandOfCard();
				frm.lblCptC.Text = inGamePlayer.listOfPlayer [1].totalPointInHand.ToString ();
			} 
			else {
				frm.Close ();
			}
		}
	}
}

