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
	public abstract class game
	{

		protected string onlineID = "";
		protected Croupier blackjackSet = new Croupier ();
		protected GroupOfPlayer inGamePlayer = new GroupOfPlayer (2);
		protected int cardImageIndex = 0;
		protected int cardImageIndexCroupier = 1;
		protected frmJeu currentGame;

		public game (frmJeu frm)
		{
			currentGame = frm;
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

		protected virtual void playTurn(){
		
		}

		public virtual void picDistribuerCarte_Click(){
			
		}

		public virtual void picFinCarte_Click(){
		
		}

		public virtual void picDemanderCarte_Click(){
			
		}
	}
}

