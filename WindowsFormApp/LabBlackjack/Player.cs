using System;
using System.Collections.Generic;
using System.Xml;

namespace LabBlackjack
{
	public class Player
	{
		const int numberOfCardPerTypes = 13;
		const int pointForFaceCard = 10;
		const int pointForAce = 11;

		public string name;

		public List<int> HandOfCard = new List<int>();
		public bool isPlayerFolded = false;
		public int totalPointInHand{ get; private set;}
		public int gamePlayed = 0;
		public int gameWon = 0;
		/*need solid cleaning*/
		public int getNewTotalPointFromHandOfCard(){
			totalPointInHand = 0; 
			foreach (int card in HandOfCard) {
				if ((card % numberOfCardPerTypes) >= 10) { //is card a face
					totalPointInHand += pointForFaceCard;
				} 
				else if ((card % numberOfCardPerTypes) == 0) { //is card an ace
					totalPointInHand += pointForAce;
				} 
				else { // normal card
					totalPointInHand += ((card % numberOfCardPerTypes)+1);
				}
			}
			return totalPointInHand;
		}

		public XmlNode getCreateXmlNode(XmlDocument Player){
			XmlNode xmlName = Player.CreateNode (XmlNodeType.Element, "NAME", "");
			xmlName.InnerText = name;
			XmlNode xmlPlayed = Player.CreateNode (XmlNodeType.Element, "PLAYED", "");
			xmlPlayed.InnerText = gamePlayed.ToString();
			XmlNode xmlWin = Player.CreateNode (XmlNodeType.Element, "WIN", "");
			xmlWin.InnerText = gameWon.ToString();
			XmlNode xmlPlayer = Player.CreateNode (XmlNodeType.Element, "PLAYER", "");
			xmlPlayer.AppendChild (xmlName);
			xmlPlayer.AppendChild (xmlPlayed);
			xmlPlayer.AppendChild (xmlWin);
			return xmlPlayer;
		}
	}
}