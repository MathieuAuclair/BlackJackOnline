using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	class MainClass
	{
		static List<Player> listOfPlayer = new List<Player> ();
		static Croupier blackJackSet = new Croupier ();

		public static void Main (string[] args)
		{
			createPlayer (2); //using a placeHolder value

			blackJackSet.playNewCardOnTable ();
			Console.WriteLine ("The croupier draw " + blackJackSet.PlayedCard[0]);

			foreach (Player currentTurnPlayer in listOfPlayer) {
				currentTurnPlayer.isPlayerFolded = isPlayerFolding ();
				playTurnCurrentPlayer (currentTurnPlayer);
			}
		}

		private static void createPlayer(int playerCount){
			for (int i = 0; i < playerCount; i++) {
				listOfPlayer.Add (new Player());
			}
		}

		private static bool isPlayerFolding(){
			Console.Write ("Fold? (Y/N)");
			if (Console.ReadLine () == "Y") {
				return true;
			}
			return false;
		}

		private static void playTurnCurrentPlayer(Player currentTurnPlayer){
			if (currentTurnPlayer.isPlayerFolded) {
				Console.WriteLine ("player is folded!");
			} 
			else {
				int newCard = blackJackSet.drawNewCardFromCardPack ();
				currentTurnPlayer.HandOfCard.Add (newCard);
				Console.WriteLine ("Player draw a " + newCard);
			}
		}
	}
}
