using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;

namespace BlackJackOnline
{

	class MainClass
	{
		
		static Croupier blackJackSet = new Croupier ();
		static GroupOfPlayer activePlayers = new GroupOfPlayer (2); //weird name...

		public static void Main (string[] args)
		{
			sendTestDataToServer ();

			do{
				foreach (Player currentTurnPlayer in activePlayers.listOfPlayer) {
					Console.Write(currentTurnPlayer.name);
					if(currentTurnPlayer.isPlayerFolded){
						Console.Write (" is folded!\n\n");
					}
					else{
						blackJackSet.playTurn (currentTurnPlayer);
						Console.WriteLine ("You have a hand of " + currentTurnPlayer.getNewTotalPointFromHandOfCard());
						currentTurnPlayer.isPlayerFolded = isPlayerFolding();
					}
				}
			}while(activePlayers.isAnyPlayerLeftToPlay() && !activePlayers.isAnyPlayerBusted());
		}

		private static bool isPlayerFolding(){
			Console.WriteLine ("Fold? (Y/N)");
			if (Console.ReadLine () == "Y") {
				return true;
			}
			return false;
		}
		private static async void sendTestDataToServer(){
			const string urlTemplate = "http://localhost:8080/build";
			var userQuery = "test";

			var client = new HttpClient();
			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			client.Timeout = TimeSpan.FromMilliseconds(600000);
			var task = client.PostAsync(urlTemplate, urlTemplate);
			var result = task.Result.Content.ReadAsStringAsync().Result;

		}
	}
}
