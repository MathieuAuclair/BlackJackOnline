using System;
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

		public bool isNotEveryPlayerDone(){
			foreach (Player currentPlayer in listOfPlayer) {
				if (!currentPlayer.isPlayerFolded && !(currentPlayer.getTotalPointFromHandOfCard() >= 21)) {//not clean at all!
					return true;
				}
			}
			return false;
		}
	}
}

