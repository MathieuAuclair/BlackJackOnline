﻿using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	public class GroupOfPlayer
	{
		public List<Player> listOfPlayer = new List<Player> ();

		public GroupOfPlayer (int countOfPlayer)
		{
			createPlayer (countOfPlayer);
		}

		private void createPlayer(int countOfPlayer){
			for (int i = 0; i < countOfPlayer; i++) {
				listOfPlayer.Add (new Player());
				listOfPlayer [i].name = "Player" + i;
			}
		}

		public bool isAnyPlayerLeftToPlay(){
			foreach (Player currentPlayer in listOfPlayer) {
				if (!currentPlayer.isPlayerFolded) {
					return true;
				}
			}
			return false;
		}
		/*need to check for optimisation*/
		public bool isAnyPlayerBusted(){
			foreach (Player currentPlayer in listOfPlayer) {
				if (currentPlayer.totalPointInHand > 21) {
					Console.WriteLine (currentPlayer.name + " is busted!");
					return true;
				}
			}
			return false;
		}
	}
}
