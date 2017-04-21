using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	class MainClass
	{
		
		static Croupier blackJackSet = new Croupier ();
		static GroupOfPlayer activePlayers = new GroupOfPlayer (2);

		public static void Main (string[] args)
		{
			blackJackSet.PlayNewCardOnTable ();
			Console.WriteLine ("The croupier draw " + blackJackSet.PlayedCardByCroupier[0]);
			do{
			foreach (Player currentTurnPlayer in activePlayers.listOfPlayer) {
					if(currentTurnPlayer.isPlayerFolded){
						Console.WriteLine ("\nplayer is folded!");
					}
					else{
						playTurnForCurrentPlayer (currentTurnPlayer);
						currentTurnPlayer.isPlayerFolded = isPlayerFolding();
					}
				}
			}while(true);
			
		}

		private static bool isPlayerFolding(){
			Console.Write ("Fold? (Y/N)");
			if (Console.ReadLine () == "Y") {
				return true;
			}
			return false;
		}

		private static void playTurnForCurrentPlayer(Player currentTurnPlayer){
				int newCard = blackJackSet.DrawNewCardFromCardPack ();
				currentTurnPlayer.HandOfCard.Add (newCard);
			Console.WriteLine ("Player draw a " + blackJackSet.ConvertCardDigitToStringValue(newCard));
		}
	}
}
