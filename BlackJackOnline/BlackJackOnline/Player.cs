using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	public class Player
	{
		const int numberOfCardPerTypes = 13;
		const int pointForfaceCard = 10;
		const int pointForAce = 11;

		public string name;

		public List<int> HandOfCard = new List<int>();
		public bool isPlayerFolded = false;
		private int totalPointInHand;
		/*need solid cleaning*/
		public int getTotalPointFromHandOfCard(){
			foreach (int card in HandOfCard) {
				if ((card / numberOfCardPerTypes) >= 10) { //is card a face
					totalPointInHand += pointForfaceCard;
				} 
				else if ((card / numberOfCardPerTypes) == 0) { //is card an ace
					totalPointInHand += pointForAce;
				} 
				else { // normal card
					totalPointInHand += (card / numberOfCardPerTypes);
				}
			}
			return totalPointInHand;
		}
	}
}

