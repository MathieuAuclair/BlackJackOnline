namespace LabBlackjack
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.showHelp = new System.Windows.Forms.Button();
            this.playOnline = new System.Windows.Forms.Button();
            this.playOffline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Brush Script MT", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(120, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 79);
            this.label1.TabIndex = 0;
            this.label1.Text = "BLACK JACK";
            // 
            // showHelp
            // 
            this.showHelp.Location = new System.Drawing.Point(255, 265);
            this.showHelp.Name = "showHelp";
            this.showHelp.Size = new System.Drawing.Size(144, 48);
            this.showHelp.TabIndex = 1;
            this.showHelp.Text = "Server Help";
            this.showHelp.UseVisualStyleBackColor = true;
            this.showHelp.Click += new System.EventHandler(this.button1_Click);
            // 
            // playOnline
            // 
            this.playOnline.Location = new System.Drawing.Point(255, 211);
            this.playOnline.Name = "playOnline";
            this.playOnline.Size = new System.Drawing.Size(144, 48);
            this.playOnline.TabIndex = 2;
            this.playOnline.Text = "Play Online";
            this.playOnline.UseVisualStyleBackColor = true;
            this.playOnline.Click += new System.EventHandler(this.button2_Click);
            // 
            // playOffline
            // 
            this.playOffline.Location = new System.Drawing.Point(255, 157);
            this.playOffline.Name = "playOffline";
            this.playOffline.Size = new System.Drawing.Size(144, 48);
            this.playOffline.TabIndex = 3;
            this.playOffline.Text = "Play Offline";
            this.playOffline.UseVisualStyleBackColor = true;
            this.playOffline.Click += new System.EventHandler(this.button3_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(654, 467);
            this.Controls.Add(this.playOffline);
            this.Controls.Add(this.playOnline);
            this.Controls.Add(this.showHelp);
            this.Controls.Add(this.label1);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button showHelp;
        private System.Windows.Forms.Button playOnline;
        private System.Windows.Forms.Button playOffline;
    }
}