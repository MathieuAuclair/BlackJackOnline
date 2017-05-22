using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;


namespace LabBlackjack
{
	/*
	 	* this class is abstract because it's not a valid type to decalre
	 	* we don't want anyone to declare a new game, the only thing we accept
	 	* is to store a onlineGame or offLine game in a game variable
	*/
	public abstract class game
	{
		/*
		 	* protected field are field that are accessible from inherit class
		 	* it's really useful to avoid repetition
		*/
		protected string onlineID = "";
		protected Croupier blackjackSet = new Croupier ();
		protected GroupOfPlayer inGamePlayer = new GroupOfPlayer (2);
		protected int cardImageIndex = 0;
		protected int cardImageIndexCroupier = 1;

		public game (frmJeu frm)
		{
			
		}
			
		protected void resetCardPictureBox(frmJeu frm){
			cardImageIndex = 0;
			for (int i = 1; i < 6; i++) {
				PictureBox card = (PictureBox)frm.Controls ["picJ" + i];
				card.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/back.gif");
				card = (PictureBox)frm.Controls ["picC" + i];
				card.Image = Image.FromFile(Directory.GetCurrentDirectory() + "/images/back.gif");
			}
			frm.lblCptJ.Text = "0";
		}

		/*
			* this is declared because both children class are using this method
		*/

		protected virtual void playTurn(){
		
		}

		/*
		 	* this is declared so a variable of the type game have these method accessible
		 	* or else even if online game has these method public, once onlineGame would have been
		 	* stored in a game variable, these method would not be availible.
		*/

		public virtual void picDistribuerCarte_Click(){
			
		}

		public virtual void picFinCarte_Click(){
		
		}

		public virtual void picDemanderCarte_Click(){
			
		}
	}
}

