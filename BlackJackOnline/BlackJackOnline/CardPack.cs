using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	public class CardPack
	{
		public List<int> Card = new List<int>();

		public CardPack ()
		{
			mixCardPack ();
		}

		public void mixCardPack(){
			int newCard;
			Random randomNewCard = new Random ();
			for(int cardPosition = 0; cardPosition < 52; cardPosition++){
				do {
					newCard = randomNewCard.Next (0, 52);
				} while(isCardNotAvailible (newCard));

				Card.Add (newCard); //add a card to the cardpack
			}
		}

		private bool isCardNotAvailible(int newCard){
			foreach (int usedCard in Card) {
				if (usedCard == newCard) {
					return true;
				}
			}
			return false;
		}
		
	}
}

