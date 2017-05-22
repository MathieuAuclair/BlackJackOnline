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

		private void picDistribuerCarte_Click(object sender, EventArgs e){
			if (sendCustomPOSTWebRequest ("turn", onlineID) == "false") {
				MessageBox.Show ("Please wait for your turn");
			} 
			else {
				playTurn ();// play turn as normal and then send last card played
			}
		}

		private void picFinCarte_Click(object sender, EventArgs e){
			if (sendCustomPOSTWebRequest ("turn", onlineID) == "false") {
				MessageBox.Show ("Please wait for your turn");
			} 
			else {
				sendCustomPOSTWebRequest ("play", "");// need some checkup
			}
		}

		private void picDemanderCarte_Click(object sender, EventArgs e){
			if (sendCustomPOSTWebRequest ("turn", onlineID) == "false") {
				MessageBox.Show ("Please wait for your turn");
			} 
			else {

			}
		}

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
				frm.Close ();
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
				}
				int newCard = blackjackSet.DrawNewCardFromCardPack ();
				string score = sendCustomPOSTWebRequest ("play", newCard.ToString());
				frm.lblCptJ.Text = score;
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

		private void askForNewSet(){
			DialogResult newGame = MessageBox.Show ("Do you want to play a new game?", "new game", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
			if (newGame == DialogResult.Yes) {
				frm.lblCptJ.Text = "0";
				resetCardPictureBox ();
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

