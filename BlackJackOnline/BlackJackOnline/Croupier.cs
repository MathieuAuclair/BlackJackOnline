using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	public class Croupier
	{
		public List<int> PlayedCard = new List<int> ();
		public int CardPackIndex;
		CardPack cardPack = new CardPack ();

		public int drawNewCardFromCardPack(){
			if (CardPackIndex >= 52) {
				cardPack.mixCardPack ();
				CardPackIndex = 0;	
			} 
			return cardPack.Card [CardPackIndex++];
		}

		public void playNewCardOnTable(){
			PlayedCard.Add (drawNewCardFromCardPack ());
		}
	}
}

