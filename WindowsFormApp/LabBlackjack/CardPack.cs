using System;
using System.Collections.Generic;

namespace LabBlackjack
{
	public class CardPack
	{
		public List<int> Card = new List<int>();

		const int numberOfCardInPack = 52;
		const int numberOfCardPerTypes = 13;

		public CardPack ()
		{
			mixCardPack ();
		}

		public void mixCardPack(){
			int newCard;
			Card.Clear ();
			Random randomNewCard = new Random ();
			for(int cardPosition = 0; cardPosition < numberOfCardInPack; cardPosition++){
				do {
					newCard = randomNewCard.Next (0, numberOfCardInPack);
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

		public string getCardFullName(int card){
			return getCardName (card) + " of " + getCardType (card);
		}

		private string getCardName(int card){
			switch(card % numberOfCardPerTypes){
			case 0:
				return "ace";
			case 10:
				return "jack";
			case 11:
				return "queen";
			case 12:
				return "king";
			default:
				return (card % numberOfCardPerTypes + 1).ToString ();
			}	
		}

		private string getCardType(int card){
			switch(card / numberOfCardPerTypes){
			case 0:
				return "heart";
			case 1:
				return "clover";
			case 2:
				return "tile";
			case 3:
				return "spade";
			default:
				return "error";
			}
		}
	}
}

