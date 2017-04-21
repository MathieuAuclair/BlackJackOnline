using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	class MainClass
	{
		
		static Croupier blackJackSet = new Croupier ();
		static GroupOfPlayer activePlayers = new GroupOfPlayer (2); //weird name...

		public static void Main (string[] args)
		{
			blackJackSet.PlayNewCardOnTable ();
			Console.WriteLine ("The croupier draw " + blackJackSet.GetCardFullName(blackJackSet.PlayedCardByCroupier[0]) + "\n\n");
			do{
				foreach (Player currentTurnPlayer in activePlayers.listOfPlayer) {
					Console.Write(currentTurnPlayer.name);
					if(currentTurnPlayer.isPlayerFolded){
						Console.WriteLine ("player is folded!\n\n");
					}
					else{
						blackJackSet.playTurn (currentTurnPlayer);
						currentTurnPlayer.isPlayerFolded = isPlayerFolding();
					}
				}
			}while(activePlayers.isNotEveryPlayerDone());
		}

		private static bool isPlayerFolding(){
			Console.WriteLine ("Fold? (Y/N)");
			if (Console.ReadLine () == "Y") {
				return true;
			}
			return false;
		}
	}
}
