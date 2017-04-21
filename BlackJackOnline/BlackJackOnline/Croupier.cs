using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	public class Croupier
	{
		const int numberOfCardInPack = 52;

		public List<int> PlayedCardByCroupier = new List<int> ();
		public int CardPackIndex;
		CardPack cardPack = new CardPack ();

		public int DrawNewCardFromCardPack(){
			if (CardPackIndex > numberOfCardInPack) {
				cardPack.mixCardPack ();
				CardPackIndex = 0;	
			} 
			return cardPack.Card [CardPackIndex++];
		}

		public void PlayNewCardOnTable(){
			PlayedCardByCroupier.Add (DrawNewCardFromCardPack ());
		}

		public string GetCardFullName(int card){ 
			return cardPack.getCardFullName(card);
		}
	}
}

