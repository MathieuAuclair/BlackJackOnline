using System;
using System.Collections.Generic;

namespace BlackJackOnline
{
	public class Croupier
	{
		public List<int> PlayedCardByCroupier = new List<int> ();
		public int CardPackIndex;
		CardPack cardPack = new CardPack ();

		public int DrawNewCardFromCardPack(){
			if (CardPackIndex >= 52) {
				cardPack.mixCardPack ();
				CardPackIndex = 0;	
			} 
			return cardPack.Card [CardPackIndex++];
		}

		public void PlayNewCardOnTable(){
			PlayedCardByCroupier.Add (DrawNewCardFromCardPack ());
		}
		/*need cleanning*/
		public string ConvertCardDigitToStringValue(int card){ //since card are digit we need to convert them
			return getCardName(card) + " of " + getCardType(card);
		}

		private string getCardName(int card){
			switch((card+1) % 12){
			case 0:
				return "ace";
			case 10:
				return "jack";
			case 11:
				return "queen";
			case 12:
				return "king";
			default:
				return ((card+1) % 12).ToString ();
			}	
		}

		private string getCardType(int card){
			Console.WriteLine ("\n\n" + card + "\n" + (card+1)/12);
			switch(card/12){
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
		/*need cleanning*/
	}
}

