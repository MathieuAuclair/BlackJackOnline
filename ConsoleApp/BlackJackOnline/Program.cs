using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace BlackJackOnline
{

	class MainClass
	{
		static Croupier blackJackSet = new Croupier ();
		static GroupOfPlayer activePlayers = new GroupOfPlayer (2); //weird name...

		public static void Main (string[] args)
		{
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


		public static void TestPOSTWebRequest(string request){

			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/" + request);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";

			using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				string json = "{\"user\":\"test\"," + "\"password\":\"bla\"}";

				streamWriter.Write(json);
				streamWriter.Flush();
				streamWriter.Close();
			}

			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();
				Console.WriteLine (result);
			}


		}
	}
}
