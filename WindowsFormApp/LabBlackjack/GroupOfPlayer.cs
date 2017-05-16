using System;
using System.Collections.Generic;

namespace LabBlackjack
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

		public string findWinningPlayer(){
			Player winner = new Player();
			for(int index = 0; index < listOfPlayer.Count; index++){
				if (listOfPlayer [index].totalPointInHand > winner.totalPointInHand && listOfPlayer[index].totalPointInHand <= 21) {
					listOfPlayer [index].gameWon++;
					winner = listOfPlayer [index];
				} 
				listOfPlayer [index].gamePlayed++;
			}

			return winner.name + " with a score of " + winner.totalPointInHand;
		}
	}
}

