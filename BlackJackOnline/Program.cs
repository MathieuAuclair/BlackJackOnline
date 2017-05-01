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
			TestPOSTWebRequest(); 

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


		public static void TestPOSTWebRequest(){
			// Create a request using a URL that can receive a post.   
			WebRequest request = WebRequest.Create ("http://127.0.0.1:8080/test");  
			// Set the Method property of the request to POST.  
			request.Method = "POST";  
			// Create POST data and convert it to a byte array.  
			string postData = "This is a test that posts this string to a Web server.";  
			byte[] byteArray = Encoding.UTF8.GetBytes (postData);  
			// Set the ContentType property of the WebRequest.  
			request.ContentType = "application/x-www-form-urlencoded";  
			// Set the ContentLength property of the WebRequest.  
			request.ContentLength = byteArray.Length;  
			// Get the request stream.  
			Stream dataStream = request.GetRequestStream ();  
			// Write the data to the request stream.  
			dataStream.Write (byteArray, 0, byteArray.Length);  
			// Close the Stream object.  
			dataStream.Close ();  
			// Get the response.  
			WebResponse response = request.GetResponse ();  
			// Display the status.  
			Console.WriteLine (((HttpWebResponse)response).StatusDescription);  
			// Get the stream containing content returned by the server.  
			dataStream = response.GetResponseStream ();  
			// Open the stream using a StreamReader for easy access.  
			StreamReader reader = new StreamReader (dataStream);  
			// Read the content.  
			string responseFromServer = reader.ReadToEnd ();  
			// Display the content.  
			Console.WriteLine (responseFromServer);  
			// Clean up the streams.  
			reader.Close ();  
			dataStream.Close ();  
			response.Close (); 
		}
	}
}
